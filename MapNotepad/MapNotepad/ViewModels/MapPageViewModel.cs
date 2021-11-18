using Acr.UserDialogs;
using MapNotepad.Helpers;
using MapNotepad.Helpers.ProcessHelpers;
using MapNotepad.Models;
using MapNotepad.Services.Authorization;
using MapNotepad.Services.Pins;
using MapNotepad.Services.SettingsManager;
using MapNotepad.Views;
using Prism.Navigation;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

namespace MapNotepad.ViewModels
{
    class MapPageViewModel : BaseViewModel
    {
        private IPinService _pinService; 
        
        private IAuthorizationService _authorizationService;

        private ISettingsManagerService _settingsManagerService;

        public MapPageViewModel(
            INavigationService navigationService,
            IPinService pinService,
            IAuthorizationService authorizationService,
            ISettingsManagerService settingsManagerService)
            : base(navigationService)
        {
            _pinService = pinService; 
            _authorizationService = authorizationService;
            _settingsManagerService = settingsManagerService;

            _pins = new ObservableCollection<Pin>();
            _searchResult = new ObservableCollection<UserPin>();

            IsShowList = false;
        }

        #region -- Public properties --

        private string _labelPinDescription;
        public string LabelPinDescription
        {
            get => _labelPinDescription;
            set => SetProperty(ref _labelPinDescription, value);
        }

        private string _latitudePinDescription;
        public string LatitudePinDescription
        {
            get => _latitudePinDescription;
            set => SetProperty(ref _latitudePinDescription, value);
        }

        private string _longitudePinDescription;
        public string LongitudePinDescription
        {
            get => _longitudePinDescription;
            set => SetProperty(ref _longitudePinDescription, value);
        }

        private string _pinDescription;
        public string PinDescription
        {
            get => _pinDescription;
            set => SetProperty(ref _pinDescription, value);
        }

        private string _text;
        public string Text
        {
            get => _text;
            set => SetProperty(ref _text, value);
        }

        private bool _isShowList;
        public bool IsShowList
        {
            get => _isShowList;
            set => SetProperty(ref _isShowList, value);
        }

        private bool _isPinDescriptionVisible;
        public bool IsPinDescriptionVisible
        {
            get => _isPinDescriptionVisible;
            set => SetProperty(ref _isPinDescriptionVisible, value);
        }

        private UserPin _selectedItem;
        public UserPin SelectedItem
        {
            get => _selectedItem;
            set => SetProperty(ref _selectedItem, value);
        }

        private ObservableCollection<Pin> _pins;
        public ObservableCollection<Pin> Pins
        {
            get => _pins;
            set => SetProperty(ref _pins, value);
        }

        private ObservableCollection<UserPin> _searchResult;
        public ObservableCollection<UserPin> SearchResult
        {
            get => _searchResult;
            set => SetProperty(ref _searchResult, value);
        }

        private ICommand _itemTappedCommand;
        public ICommand ItemTappedCommand => _itemTappedCommand ??= SingleExecutionCommand.FromFunc(OnItemTappedCommandAsync);

        private ICommand _mapClickedCommand;
        public ICommand MapClickedCommand => _mapClickedCommand ??= SingleExecutionCommand.FromFunc<Position>(OnMapClickedCommandAsync);

        private ICommand _exitButtonCommand;
        public ICommand ExitButtonCommand => _exitButtonCommand ??= SingleExecutionCommand.FromFunc(OnExitButtonCommandAsync);
        
        private ICommand _GoSettingsButtonCommand;
        public ICommand GoSettingsButtonCommand => _GoSettingsButtonCommand ??= SingleExecutionCommand.FromFunc(OnGoSettingsButtonCommandAsync);

        #endregion

        #region -- IInitializeAsync implementation --

        public async override Task InitializeAsync(INavigationParameters parameters)
        {
            MessagingCenter.Subscribe<AddPinsPageViewModel, UserPin>(
                this,
                "AddPin",
                (sender, userPin) => {
                    var pin = new Pin()
                    {
                        Label = userPin.Label,
                        Position = new Position(userPin.Latitude, userPin.Longitude),
                        IsVisible = userPin.Favorites,
                        Tag = userPin.Id
                    };
                    pin.Clicked += Pin_Clicked;

                    Pins.Add(pin);
                });
            
            MessagingCenter.Subscribe<PinsPageViewModel, UserPinWithCommand>(
                this,
                "DeletePin",
                (sender, userPin) => {
                    var pin = new Pin()
                    {
                        Label = userPin.Label,
                        Position = new Position(userPin.Latitude, userPin.Longitude),
                        IsVisible = userPin.Favorites,
                        Tag = userPin.Id
                    };

                    Pins.Remove(pin);
                });

            var allPins = await _pinService.AllPinsAsync();

            if (allPins.IsSuccess)
            {
                foreach (var userPin in allPins.Result)
                {
                    var pin = new Pin()
                    {
                        Label = userPin.Label,
                        Position = new Position(userPin.Latitude, userPin.Longitude),
                        IsVisible = userPin.Favorites,
                        Tag = userPin.Id
                    };
                    pin.Clicked += Pin_Clicked;

                    Pins.Add(pin);
                }
            }
        }

        #endregion

        #region -- Overrides --

        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);

            switch (args.PropertyName)
            {
                case nameof(Text):
                    if (!string.IsNullOrWhiteSpace(Text))
                    {
                        var allPins = _pinService.SearchPinsAsync(Text.Trim());

                        if (allPins.Result.IsSuccess)
                        {
                            SearchResult.Clear();

                            foreach (var pin in allPins.Result.Result)
                            {
                                SearchResult.Add(pin);
                            }
                        }

                        IsShowList = SearchResult.Count > 0;
                    }
                    
                    break;
                case nameof(IsShowList):
                    if (!IsShowList)
                    {
                        SearchResult.Clear();
                    }
                    break;
            }
        }

        #endregion

        #region -- Private methods --

        private void Pin_Clicked(object sender, System.EventArgs e)
        {
            var pin = sender as Pin;

            var userPin = _pinService.GetByIdAsync((int)pin.Tag);

            if (userPin != null)
            {
                if (userPin.Result.IsSuccess)
                {
                    LabelPinDescription = userPin.Result.Result.Label;
                    LatitudePinDescription = userPin.Result.Result.Latitude.ToString();
                    LongitudePinDescription = userPin.Result.Result.Longitude.ToString();
                    PinDescription = userPin.Result.Result.Description;

                    IsPinDescriptionVisible = true;
                }
            }
        }

        private Task OnMapClickedCommandAsync(Position position)
        {
            IsPinDescriptionVisible = false;

            return Task.CompletedTask;
        }

        private Task OnItemTappedCommandAsync()
        {
            IsShowList = false;
            Text = "";

            MessagingCenter.Send<MapPageViewModel, Position>(this, "MoveToPosition", new Position(SelectedItem.Latitude, SelectedItem.Longitude));

            return Task.CompletedTask;
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

        private Task OnGoSettingsButtonCommandAsync()
        {
            return _navigationService.NavigateAsync(nameof(SettingsPage));
        }

        #endregion

    }
}