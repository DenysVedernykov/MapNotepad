using Acr.UserDialogs;
using MapNotepad.Helpers;
using MapNotepad.Models;
using MapNotepad.Services.Authorization;
using MapNotepad.Services.PermissionsService;
using MapNotepad.Services.Pins;
using MapNotepad.Services.SettingsManager;
using Plugin.Geolocator;
using Prism.Navigation;
using Prism.Unity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

namespace MapNotepad.ViewModels
{
    class EditPinsPageViewModel : BaseViewModel
    {
        private Pin _currentPin;

        private UserPinWithCommand userPin;
        private UserPinWithCommand userOldPin;

        private ResourceDictionary _resourceDictionary;

        private IPinService _pinService;

        private IAuthorizationService _authorizationService;

        private IPermissionsService _permissionsService;

        private ISettingsManagerService _settingsManagerService;

        public EditPinsPageViewModel(
            INavigationService navigationService,
            IPinService pinService,
            IAuthorizationService authorizationService,
            IPermissionsService permissionsService,
            ISettingsManagerService settingsManagerService)
            : base(navigationService)
        {
            _pinService = pinService;
            _authorizationService = authorizationService;
            _permissionsService = permissionsService;
            _settingsManagerService = settingsManagerService;

            _currentPin = new Pin();
            _currentPin.Label = "Point";

            ICollection<ResourceDictionary> mergedDictionaries = PrismApplication.Current.Resources.MergedDictionaries;
            _resourceDictionary = mergedDictionaries.FirstOrDefault();

            BorderColorLabel = (Color)_resourceDictionary["LightGray"];
            BorderColorLongitude = (Color)_resourceDictionary["LightGray"];
            BorderColorLatitude = (Color)_resourceDictionary["LightGray"];
        }

        #region -- Public properties --

        private Color _borderColorLabel;
        public Color BorderColorLabel
        {
            get => _borderColorLabel;
            set => SetProperty(ref _borderColorLabel, value);
        }

        private Color _borderColorLongitude;
        public Color BorderColorLongitude
        {
            get => _borderColorLongitude;
            set => SetProperty(ref _borderColorLongitude, value);
        }

        private Color _borderColorLatitude;
        public Color BorderColorLatitude
        {
            get => _borderColorLatitude;
            set => SetProperty(ref _borderColorLatitude, value);
        }

        private string _label;
        public string Label
        {
            get => _label;
            set => SetProperty(ref _label, value);
        }

        private string _description;
        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        private string _latitude;
        public string Latitude
        {
            get => _latitude;
            set => SetProperty(ref _latitude, value);
        }

        private string _longitude;
        public string Longitude
        {
            get => _longitude;
            set => SetProperty(ref _longitude, value);
        }

        private bool _isEnableSaveButton;
        public bool IsEnableSaveButton
        {
            get => _isEnableSaveButton;
            set => SetProperty(ref _isEnableSaveButton, value);
        }

        private bool _isShowingUser;
        public bool IsShowingUser
        {
            get => _isShowingUser;
            set => SetProperty(ref _isShowingUser, value);
        }

        private CameraUpdate _initialCameraUpdate;
        public CameraUpdate InitialCameraUpdate
        {
            get => _initialCameraUpdate;
            set => SetProperty(ref _initialCameraUpdate, value);
        }

        private ICommand _goBackCommand;
        public ICommand GoBackCommand => _goBackCommand ??= SingleExecutionCommand.FromFunc(OnGoBackCommandAsync);

        private ICommand _saveCommand;
        public ICommand SaveCommand => _saveCommand ??= SingleExecutionCommand.FromFunc(OnSaveCommandAsync);

        private ICommand _moveToMyLocationCommand;
        public ICommand MoveToMyLocationCommand => _moveToMyLocationCommand ??= SingleExecutionCommand.FromFunc(OnMoveToMyLocationCommandAsync);


        private ICommand _mapClickedCommand;
        public ICommand MapClickedCommand => _mapClickedCommand ??= SingleExecutionCommand.FromFunc<Position>(OnMapClickedCommandAsync);

        #endregion

        #region -- IInitializeAsync implementation --

        public async override Task InitializeAsync(INavigationParameters parameters)
        {
            if (_settingsManagerService.IsNightThemeEnabled)
            {
                MessagingCenter.Send<EditPinsPageViewModel, string>(this, "ThemeChange", "MapNotepad.Themes.DarkMapTheme.txt");
            }
            else
            {
                MessagingCenter.Send<EditPinsPageViewModel, string>(this, "ThemeChange", "MapNotepad.Themes.LightMapTheme.txt");
            }

            await CheckPermissions();

            if (parameters.Count > 0)
            {
                if (parameters["Pin"] is not null)
                {
                    userOldPin = parameters["Pin"] as UserPinWithCommand;

                    userPin = new UserPinWithCommand();

                    userPin.Id = userOldPin.Id;
                    userPin.Address = userOldPin.Address;
                    userPin.Autor = userOldPin.Autor;
                    userPin.CreationDate = userOldPin.CreationDate;
                    userPin.Description = userOldPin.Description;
                    userPin.Favorites = userOldPin.Favorites;
                    userPin.Label = userOldPin.Label;
                    userPin.Latitude = userOldPin.Latitude;
                    userPin.Longitude = userOldPin.Longitude;
                    userPin.TabCommand = userOldPin.TabCommand;
                    userPin.EditCommand = userOldPin.EditCommand;
                    userPin.DeleteCommand = userOldPin.DeleteCommand;
                    userPin.SwitchFavoritesCommand = userOldPin.SwitchFavoritesCommand;

                    Label = userPin.Label;
                    Description = userPin.Description;
                    Latitude = userPin.Latitude.ToString();
                    Longitude = userPin.Longitude.ToString();

                    _currentPin.Position = new Position(userOldPin.Latitude, userOldPin.Longitude);

                    InitialCameraUpdate = CameraUpdateFactory.NewPositionZoom(_currentPin.Position, 12d);
                }
            }
        }

        #endregion

        #region -- Overrides --

        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            double sample;

            base.OnPropertyChanged(args);

            bool updatePin = false;
            bool truePosition = double.TryParse(Longitude, out sample)
                && double.TryParse(Latitude, out sample);

            if (truePosition)
            {
                var lng = double.Parse(Longitude);
                var lat = double.Parse(Latitude);

                truePosition &= (-180 <= lng) && (lng <= 180) && (-90 <= lat) && (lat <= 90);
            }

            IsEnableSaveButton = !string.IsNullOrEmpty(Label) && truePosition;

            switch (args.PropertyName)
            {
                case nameof(Label):

                    if (string.IsNullOrEmpty(Label))
                    {
                        BorderColorLabel = (Color)_resourceDictionary["Error"];
                    }
                    else
                    {
                        BorderColorLabel = (Color)_resourceDictionary["LightGray"];
                    }

                    break;
                case nameof(Longitude):

                    if (double.TryParse(Longitude, out sample))
                    {
                        var lng = double.Parse(Longitude);

                        if (-180 <= lng && lng <= 180)
                        {
                            BorderColorLongitude = (Color)_resourceDictionary["LightGray"];
                        }
                        else
                        {
                            BorderColorLongitude = (Color)_resourceDictionary["Error"];
                        }
                    }
                    else
                    {
                        BorderColorLongitude = (Color)_resourceDictionary["Error"];
                    }

                    updatePin = true;

                    break;
                case nameof(Latitude):

                    if (double.TryParse(Latitude, out sample))
                    {
                        var lat = double.Parse(Latitude);

                        if (-90 <= lat && lat <= 90)
                        {
                            BorderColorLatitude = (Color)_resourceDictionary["LightGray"];
                        }
                        else
                        {
                            BorderColorLatitude = (Color)_resourceDictionary["Error"];
                        }
                    }
                    else
                    {
                        BorderColorLatitude = (Color)_resourceDictionary["Error"];
                    }

                    updatePin = true;

                    break;
            }

            if (truePosition && updatePin)
            {
                MessagingCenter.Send<EditPinsPageViewModel, Pin>(this, "DeletePin", _currentPin);

                _currentPin.Position = new Position(double.Parse(Latitude), double.Parse(Longitude));

                MessagingCenter.Send<EditPinsPageViewModel, Pin>(this, "AddPin", _currentPin);

                MessagingCenter.Send<EditPinsPageViewModel, Position>(
                    this,
                    "MoveToLocation",
                    _currentPin.Position);
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

        private async Task OnGoBackCommandAsync()
        {
            var confirm = await UserDialogs.Instance.ConfirmAsync(new ConfirmConfig()
            {
                OkText = Resource.ResourceManager.GetString("Ok", Resource.Culture),
                Message = Resource.ResourceManager.GetString("AlertNotSaved", Resource.Culture),
                CancelText = Resource.ResourceManager.GetString("Cancel", Resource.Culture)
            });

            if (confirm)
            {
                await _navigationService.GoBackAsync();
            }
        }

        private async Task OnSaveCommandAsync()
        {
            if (IsEnableSaveButton)
            {
                var geoCoder = new Geocoder();

                var position = new Position(double.Parse(Latitude), double.Parse(Longitude));
                var possibleAddresses = await geoCoder.GetAddressesForPositionAsync(position);

                if (possibleAddresses is not null)
                {
                    userPin.Label = Label;
                    userPin.Address = possibleAddresses.FirstOrDefault();
                    userPin.Description = Description;
                    userPin.Latitude = double.Parse(Latitude);
                    userPin.Longitude = double.Parse(Longitude);

                    var result = _pinService.UpdatePinAsync(userPin.ToUserPin());

                    if (result.Result.IsSuccess)
                    {

                        MessagingCenter.Send<EditPinsPageViewModel, UserPinWithCommand>(this, "DeletePin", userOldPin);
                        MessagingCenter.Send<EditPinsPageViewModel, UserPinWithCommand>(this, "AddPin", userPin);

                        await _navigationService.GoBackAsync();
                    }
                    else
                    {
                        await UserDialogs.Instance.AlertAsync(new AlertConfig()
                        {
                            OkText = Resource.ResourceManager.GetString("Ok", Resource.Culture),
                            Message = Resource.ResourceManager.GetString("ErrorEditPin", Resource.Culture)
                        });
                    }
                }
                else
                {
                    await UserDialogs.Instance.AlertAsync(new AlertConfig()
                    {
                        OkText = Resource.ResourceManager.GetString("Ok", Resource.Culture),
                        Message = Resource.ResourceManager.GetString("ErrorEditPin", Resource.Culture)
                    });
                }
            }
            else
            {
                await UserDialogs.Instance.AlertAsync(new AlertConfig()
                {
                    Message = Resource.ResourceManager.GetString("EnterCorrectData", Resource.Culture),
                    OkText = Resource.ResourceManager.GetString("Ok", Resource.Culture)
                });
            }
        }

        private async Task OnMoveToMyLocationCommandAsync()
        {
            await CheckPermissions();

            if (IsShowingUser)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    try
                    {
                        var locator = CrossGeolocator.Current;

                        if (locator.IsGeolocationEnabled && locator.IsGeolocationAvailable)
                        {
                            var position = await locator.GetPositionAsync();

                            MessagingCenter.Send<EditPinsPageViewModel, Position>(
                            this,
                            "MoveToLocation",
                            new Position(position.Latitude, position.Longitude));
                        }
                    }
                    catch(Exception e)
                    {
                    }
                    
                });
            }
        }

        private Task OnMapClickedCommandAsync(Position position)
        {
            Longitude = position.Longitude.ToString();
            Latitude = position.Latitude.ToString();

            return Task.CompletedTask;
        }

        #endregion

    }
}
