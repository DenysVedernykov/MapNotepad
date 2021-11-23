using Acr.UserDialogs;
using MapNotepad.Helpers;
using MapNotepad.Helpers.ProcessHelpers;
using MapNotepad.Models;
using MapNotepad.Services.Authorization;
using MapNotepad.Services.PermissionsService;
using MapNotepad.Services.Pins;
using MapNotepad.Services.SettingsManager;
using MapNotepad.Views;
using Prism.Navigation;
using Prism.Services.Dialogs;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

namespace MapNotepad.ViewModels
{
    class MapPageViewModel : BaseViewModel
    {
        private IPinService _pinService;

        private IDialogService _dialogService;

        private IAuthorizationService _authorizationService;

        private ISettingsManagerService _settingsManagerService;

        private IPermissionsService _permissionsService;

        public MapPageViewModel(
            IDialogService dialogService, 
            INavigationService navigationService,
            IPinService pinService,
            IAuthorizationService authorizationService,
            ISettingsManagerService settingsManagerService,
            IPermissionsService permissionsService
            )
            : base(navigationService)
        {
            _dialogService = dialogService; 
            _pinService = pinService; 
            _authorizationService = authorizationService;
            _settingsManagerService = settingsManagerService;
            _permissionsService = permissionsService;

            _pins = new ObservableCollection<Pin>();
            _searchResult = new ObservableCollection<UserPin>();
        }

        #region -- Public properties --

        private string _labelPinDescription;
        public string LabelPinDescription
        {
            get => _labelPinDescription;
            set => SetProperty(ref _labelPinDescription, value);
        }

        private double _latitudePinDescription;
        public double LatitudePinDescription
        {
            get => _latitudePinDescription;
            set => SetProperty(ref _latitudePinDescription, value);
        }

        private double _longitudePinDescription;
        public double LongitudePinDescription
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

        private bool _isPinDescriptionVisible;
        public bool IsPinDescriptionVisible
        {
            get => _isPinDescriptionVisible;
            set => SetProperty(ref _isPinDescriptionVisible, value);
        }

        private string _text;
        public string Text
        {
            get => _text;
            set => SetProperty(ref _text, value);
        }

        private bool _isEmptySearchResult;
        public bool IsEmptySearchResult
        {
            get => _isEmptySearchResult;
            set => SetProperty(ref _isEmptySearchResult, value);
        }

        private bool _shouldShowList;
        public bool ShouldShowList
        {
            get => _shouldShowList;
            set => SetProperty(ref _shouldShowList, value);
        }

        private bool _isShowingUser;
        public bool IsShowingUser
        {
            get => _isShowingUser;
            set => SetProperty(ref _isShowingUser, value);
        }

        private UserPin _selectedItem;
        public UserPin SelectedItem
        {
            get => _selectedItem;
            set => SetProperty(ref _selectedItem, value);
        }

        private UserPin _clickedItem;
        public UserPin ClickedItem
        {
            get => _clickedItem;
            set => SetProperty(ref _clickedItem, value);
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

        private ICommand _showQrCodeCommand;
        public ICommand ShowQrCodeCommand => _showQrCodeCommand ??= SingleExecutionCommand.FromFunc(OnShowQrCodeCommandAsync);
        

        private ICommand _itemTappedCommand;
        public ICommand ItemTappedCommand => _itemTappedCommand ??= SingleExecutionCommand.FromFunc(OnItemTappedCommandAsync);

        private ICommand _moveToMyLocationCommand;
        public ICommand MoveToMyLocationCommand => _moveToMyLocationCommand ??= SingleExecutionCommand.FromFunc(OnMoveToMyLocationCommandAsync);

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
            CheckPermissions().Await();

            MessagingCenter.Subscribe<AddPinsPageViewModel, UserPin>(
                this,
                "AddPin",
                (sender, userPin) => {
                    AddPin(sender, userPin.ToUserPinWithCommand());
                });
            
            MessagingCenter.Subscribe<PinsPageViewModel, UserPinWithCommand>(
                this,
                "DeletePin",
                (sender, userPin) => {
                    DeletePin(sender, userPin);
                });
            
            
            MessagingCenter.Subscribe<PinsPageViewModel, UserPinWithCommand>(
                this,
                "AddPin",
                (sender, userPin) => {
                    AddPin(sender, userPin);
                });

            MessagingCenter.Subscribe<EditPinsPageViewModel, UserPinWithCommand>(
                this,
                "AddPin",
                (sender, userPin) => {
                    AddPin(sender, userPin);
                });
            
            MessagingCenter.Subscribe<EditPinsPageViewModel, UserPinWithCommand>(
                this,
                "DeletePin",
                (sender, userPin) => {
                    DeletePin(sender, userPin);
                });

            var allPins = await _pinService.AllPinsAsync();

            if (allPins.IsSuccess)
            {
                var favoritesPins = allPins.Result.Where(row => row.Favorites);

                foreach (var userPin in favoritesPins)
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
                    if (string.IsNullOrWhiteSpace(Text))
                    {
                        IsEmptySearchResult = false;
                    }
                    else
                    {
                        var allPins = _pinService.SearchPinsAsync(Text.Trim());

                        if (allPins.Result.IsSuccess)
                        {
                            var favoritesPins = allPins.Result.Result.Where(row => row.Favorites);

                            SearchResult.Clear();

                            foreach (var pin in favoritesPins)
                            {
                                SearchResult.Add(pin);
                            }
                        }

                        IsEmptySearchResult = SearchResult.Count == 0;
                    }
                    
                    break;
                case nameof(ShouldShowList):
                    if (!ShouldShowList)
                    {
                        SearchResult.Clear();
                        IsEmptySearchResult = false;
                    }
                    break;
            }
        }

        #endregion

        #region -- Private helpers --

        private async Task CheckPermissions()
        {
            IsShowingUser = await _permissionsService.RequestAsync<Permissions.LocationWhenInUse>() == PermissionStatus.Granted;
        }

        #endregion

        #region -- Private methods --

        private void DeletePin(object sender, UserPinWithCommand userPin)
        {
            var pin = new Pin()
            {
                Label = userPin.Label,
                Position = new Position(userPin.Latitude, userPin.Longitude),
                IsVisible = userPin.Favorites,
                Tag = userPin.Id
            };

            Pins.Remove(pin);
        }

        private void AddPin(object sender, UserPinWithCommand userPin)
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

        private void Pin_Clicked(object sender, System.EventArgs e)
        {
            var pin = sender as Pin;

            var userPin = _pinService.GetByIdAsync((int)pin.Tag);

            if (userPin != null)
            {
                if (userPin.Result.IsSuccess)
                {
                    ClickedItem = userPin.Result.Result;

                    LabelPinDescription = userPin.Result.Result.Label;
                    LatitudePinDescription = userPin.Result.Result.Latitude;
                    LongitudePinDescription = userPin.Result.Result.Longitude;
                    PinDescription = userPin.Result.Result.Description;

                    IsPinDescriptionVisible = true;
                }
            }
        }

        private Task OnShowQrCodeCommandAsync()
        {
            var param = new DialogParameters();
            param.Add("UserPin", ClickedItem);

            _dialogService.ShowDialog("QrCodePage", param);

            return Task.CompletedTask;
        }

        private Task OnMoveToMyLocationCommandAsync()
        {
            CheckPermissions().Await();

            if (IsShowingUser)
            {
                MessagingCenter.Send<MapPageViewModel>(this, "MoveToMyLocation");
            }

            return Task.CompletedTask;
        }

        private Task OnMapClickedCommandAsync(Position position)
        {
            IsPinDescriptionVisible = false;

            return Task.CompletedTask;
        }

        private Task OnItemTappedCommandAsync()
        {
            ShouldShowList = false;
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