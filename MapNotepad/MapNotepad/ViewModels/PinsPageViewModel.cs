using Acr.UserDialogs;
using MapNotepad.Helpers;
using MapNotepad.Helpers.ProcessHelpers;
using MapNotepad.Models;
using MapNotepad.Services.Authorization;
using MapNotepad.Services.Pins;
using MapNotepad.Services.SettingsManager;
using MapNotepad.Views;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

namespace MapNotepad.ViewModels
{
    class PinsPageViewModel : BaseViewModel
    {
        private IPinService _pinService;

        private IAuthorizationService _authorizationService;

        private ISettingsManagerService _settingsManagerService;

        public PinsPageViewModel(
            INavigationService navigationService,
            IPinService pinService,
            IAuthorizationService authorizationService,
            ISettingsManagerService settingsManagerService)
            : base(navigationService)
        {
            _pinService = pinService;
            _authorizationService = authorizationService;
            _settingsManagerService = settingsManagerService;

            _pins = new ObservableCollection<UserPinWithCommand>();
        }

        #region -- Public properties --

        private string _text;
        public string Text
        {
            get => _text;
            set => SetProperty(ref _text, value);
        }

        private bool _isEmpty;
        public bool IsEmpty
        {
            get => _isEmpty;
            set => SetProperty(ref _isEmpty, value);
        }

        private bool _shouldShowList;
        public bool ShouldShowList
        {
            get => _shouldShowList;
            set => SetProperty(ref _shouldShowList, value);
        }

        private ObservableCollection<UserPinWithCommand> _pins;
        public ObservableCollection<UserPinWithCommand> Pins
        {
            get => _pins;
            set => SetProperty(ref _pins, value);
        }

        private ICommand _itemTappedCommand;
        public ICommand ItemTappedCommand => _itemTappedCommand ??= SingleExecutionCommand.FromFunc(OnItemTappedCommandAsync);

        private ICommand _itemDeleteCommand;
        public ICommand ItemDeleteCommand => _itemDeleteCommand ??= SingleExecutionCommand.FromFunc(OnItemDeleteCommandAsync);

        private ICommand _switchFavoritesCommand;
        public ICommand SwitchFavoritesCommand => _switchFavoritesCommand ??= SingleExecutionCommand.FromFunc(OnSwitchFavoritesCommandAsync);

        private ICommand _itemEditCommand;
        public ICommand ItemEditCommand => _itemEditCommand ??= SingleExecutionCommand.FromFunc(OnItemEditCommandAsync);

        private ICommand _exitButtonCommand;
        public ICommand ExitButtonCommand => _exitButtonCommand ??= SingleExecutionCommand.FromFunc(OnExitButtonCommandAsync);

        private ICommand _GoSettingsButtonCommand;
        public ICommand GoSettingsButtonCommand => _GoSettingsButtonCommand ??= SingleExecutionCommand.FromFunc(OnGoSettingsButtonCommandAsync);

        private ICommand _goAddPinsPageCommand;
        public ICommand GoAddPinsPageCommand => _goAddPinsPageCommand ??= SingleExecutionCommand.FromFunc(OnGoAddPinsPageCommandAsync);

        #endregion

        #region -- IInitializeAsync implementation --

        public async override Task InitializeAsync(INavigationParameters parameters)
        {
            MessagingCenter.Subscribe<ConfirmAddPinQrViewModel, UserPin>(
                this,
                "AddPin",
                (sender, pin) => {
                    AddPin(pin.ToUserPinWithCommand());

                    IsEmpty = Pins.Count == 0;
                });

            MessagingCenter.Subscribe<AddPinsPageViewModel, UserPin>(
                this,
                "AddPin",
                (sender, pin) => {
                    AddPin(pin.ToUserPinWithCommand());

                    IsEmpty = Pins.Count == 0;
                });

            MessagingCenter.Subscribe<EditPinsPageViewModel, UserPinWithCommand>(
                this,
                "AddPin",
                (sender, pin) => {
                    AddPin(pin);

                    IsEmpty = Pins.Count == 0;
                });

            MessagingCenter.Subscribe<EditPinsPageViewModel, UserPinWithCommand>(
                this,
                "DeletePin",
                (sender, pin) => {
                    Pins.Remove(pin);

                    IsEmpty = Pins.Count == 0;
                });

            var allPins = await _pinService.AllPinsAsync();

            if (allPins.IsSuccess)
            {
                foreach (var pin in allPins.Result)
                {
                    AddPin(pin.ToUserPinWithCommand());
                }
            }

            IsEmpty = Pins.Count == 0;
        }

        #endregion

        #region -- Overrides --

        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);

            switch (args.PropertyName)
            {
                case nameof(Text):

                    Task<AOResult<List<UserPin>>> allPins;

                    if (string.IsNullOrWhiteSpace(Text))
                    {
                        allPins = _pinService.AllPinsAsync();
                    }
                    else
                    {
                        allPins = _pinService.SearchPinsAsync(Text);
                    }

                    if (allPins.Result.IsSuccess)
                    {
                        Pins.Clear();

                        foreach (var pin in allPins.Result.Result)
                        {
                            AddPin(pin.ToUserPinWithCommand());
                        }
                    }

                    IsEmpty = Pins.Count == 0;

                    break;
            }
        }

        #endregion

        #region -- Private methods --

        private void AddPin(UserPinWithCommand pin)
        {
            pin.TabCommand = ItemTappedCommand;
            pin.DeleteCommand = ItemDeleteCommand;
            pin.EditCommand = ItemEditCommand;
            pin.SwitchFavoritesCommand = SwitchFavoritesCommand;

            Pins.Add(pin);
        }

        private Task OnSwitchFavoritesCommandAsync(object item)
        {
            var pin = item as UserPinWithCommand;

            var idx = Pins.IndexOf(pin);

            Pins[idx].Favorites = !pin.Favorites;

            _pinService.UpdatePinAsync(Pins[idx].ToUserPin());

            if (Pins[idx].Favorites)
            {
                MessagingCenter.Send<PinsPageViewModel, UserPinWithCommand>(this, "AddPin", pin);
            }
            else
            {
                MessagingCenter.Send<PinsPageViewModel, UserPinWithCommand>(this, "DeletePin", pin);
            }

            return Task.CompletedTask;
        }

        private Task OnItemTappedCommandAsync(object item)
        {
            var pin = item as UserPinWithCommand;

            ShouldShowList = false;
            Text = "";

            if (pin.Favorites)
            {
                MessagingCenter.Send<PinsPageViewModel, Position>(this, "MoveToPosition", new Position(pin.Latitude, pin.Longitude));

                MessagingCenter.Send<PinsPageViewModel, UserPin>(this, "ShowDescriptionPin", pin.ToUserPin());

                MessagingCenter.Send<PinsPageViewModel, int>(this, "SwitchTab", 0);
            }
            else
            {
                var toastConfig = new ToastConfig("Pin not favorites")
                {
                    Duration = TimeSpan.FromSeconds(2),
                    Position = ToastPosition.Bottom,
                };

                UserDialogs.Instance.Toast(toastConfig);
            }

            return Task.CompletedTask;
        }

        private async Task OnItemDeleteCommandAsync(object item)
        {
            var pin = item as UserPinWithCommand;

            var confirm = await UserDialogs.Instance.ConfirmAsync(new ConfirmConfig()
            {
                OkText = Resource.ResourceManager.GetString("Ok", Resource.Culture),
                Message = Resource.ResourceManager.GetString("ConfirmDelete", Resource.Culture),
                CancelText = Resource.ResourceManager.GetString("Cancel", Resource.Culture)
            });

            if (confirm)
            {
                var userPin = pin.ToUserPin();

                var result = _pinService.DeletePinAsync(userPin);

                if (result is not null)
                {
                    if (result.Result.IsSuccess)
                    {
                        Pins.Remove(pin);

                        MessagingCenter.Send<PinsPageViewModel, UserPinWithCommand>(this, "DeletePin", pin);
                    }
                }
            }
        }

        private async Task OnItemEditCommandAsync(object item)
        {
            var pin = item as UserPinWithCommand;

            INavigationParameters param = new NavigationParameters();
            param.Add("Pin", pin);

            await _navigationService.NavigateAsync($"{nameof(EditPinsPage)}", param);
        }

        private async Task OnExitButtonCommandAsync()
        {
            var confirm = await UserDialogs.Instance.ConfirmAsync(new ConfirmConfig()
            {
                OkText = Resource.ResourceManager.GetString("Ok", Resource.Culture),
                Message = Resource.ResourceManager.GetString("ConfirmExit", Resource.Culture),
                CancelText = Resource.ResourceManager.GetString("Cancel", Resource.Culture)
            });

            if (confirm)
            {
                _authorizationService.LogOut();

                _settingsManagerService.Email = string.Empty;
                _settingsManagerService.Password = string.Empty;
                _settingsManagerService.Session = string.Empty;

                await _navigationService.NavigateAsync($"/{nameof(StartPage)}");
            }
        }

        private async Task OnGoSettingsButtonCommandAsync()
        {
            await _navigationService.NavigateAsync(nameof(SettingsPage));
        }

        private async Task OnGoAddPinsPageCommandAsync()
        {
            await _navigationService.NavigateAsync(nameof(AddPinsPage));
        }

        #endregion

    }
}
