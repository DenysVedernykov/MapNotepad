using MapNotepad.Helpers;
using MapNotepad.Helpers.ProcessHelpers;
using MapNotepad.Models;
using MapNotepad.Services.Pins;
using MapNotepad.Views;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

namespace MapNotepad.ViewModels
{
    class PinsPageViewModel : BaseViewModel
    {
        private IPinService _pinService;

        public PinsPageViewModel(
            INavigationService navigationService,
            IPinService pinService)
            : base(navigationService)
        {
            _pinService = pinService;

            _pins = new ObservableCollection<UserPin>();
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

        private bool _isShowList;
        public bool IsShowList
        {
            get => _isShowList;
            set => SetProperty(ref _isShowList, value);
        }

        private ObservableCollection<UserPin> _pins;
        public ObservableCollection<UserPin> Pins
        {
            get => _pins;
            set => SetProperty(ref _pins, value);
        }

        private ICommand _itemTappedCommand;
        public ICommand ItemTappedCommand => _itemTappedCommand ??= SingleExecutionCommand.FromFunc(OnItemTappedCommandAsync);

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
            MessagingCenter.Subscribe<AddPinsPageViewModel, UserPin>(
                this,
                "AddPin",
                (sender, pin) => {
                    Pins.Add(pin);
                });

            var allPins = await _pinService.AllPinsAsync();

            if (allPins.IsSuccess)
            {
                foreach (var pin in allPins.Result)
                {
                    Pins.Add(pin);
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
                            Pins.Add(pin);
                        }
                    }

                    IsEmpty = Pins.Count == 0;

                    break;
            }
        }

        #endregion

        #region -- Private methods --

        private Task OnItemTappedCommandAsync()
        {
            IsShowList = false;
            Text = "";

            MessagingCenter.Send<PinsPageViewModel, int>(this, "SwitchTab", 0);

            return Task.CompletedTask;
        }

        private Task OnExitButtonCommandAsync()
        {

            return Task.CompletedTask;
        }

        private Task OnGoSettingsButtonCommandAsync()
        {
            return _navigationService.NavigateAsync(nameof(SettingsPage));
        }

        private Task OnGoAddPinsPageCommandAsync()
        {
            return _navigationService.NavigateAsync(nameof(AddPinsPage));
        }

        #endregion

    }
}
