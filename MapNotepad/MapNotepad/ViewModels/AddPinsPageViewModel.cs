using Acr.UserDialogs;
using MapNotepad.Helpers;
using MapNotepad.Models;
using MapNotepad.Services.Authorization;
using MapNotepad.Services.Pins;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Unity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

namespace MapNotepad.ViewModels
{
    class AddPinsPageViewModel : BaseViewModel
    {
        private Pin _currentPin;

        private ResourceDictionary _resourceDictionary;

        private IPinsService _pinsService;

        private IAuthorizationService _authorizationService;

        public AddPinsPageViewModel(
            INavigationService navigationService,
            IPinsService pinsService,
            IAuthorizationService authorizationService) 
            : base(navigationService)
        {
            _pinsService = pinsService;
            _authorizationService = authorizationService;

            _currentPin = new Pin();
            _currentPin.Label = "Point";

            InitialCameraUpdate = CameraUpdateFactory.NewPositionZoom(new Position(35.71d, 139.81d), 12d);

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

        private ICommand _mapClickedCommand;
        public ICommand MapClickedCommand => _mapClickedCommand ??= SingleExecutionCommand.FromFunc<Position>(OnMapClickedCommandAsync);

        #endregion

        #region -- Overrides --

        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            double sample;

            base.OnPropertyChanged(args);

            IsEnableSaveButton = !string.IsNullOrEmpty(Label);

            bool updatePin = false;
            bool truePosition = double.TryParse(Longitude, out sample)
                && double.TryParse(Latitude, out sample);

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
                        BorderColorLongitude = (Color)_resourceDictionary["LightGray"];
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
                        BorderColorLatitude = (Color)_resourceDictionary["LightGray"];
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
                MessagingCenter.Send<AddPinsPageViewModel, Pin>(this, "DeletePin", _currentPin);

                _currentPin.Position = new Position(double.Parse(Latitude), double.Parse(Longitude));

                MessagingCenter.Send<AddPinsPageViewModel, Pin>(this, "AddPin", _currentPin);
            }
        }

        #endregion

        #region -- Private methods --

        private Task OnGoBackCommandAsync()
        {
            return _navigationService.GoBackAsync();
        }

        private Task OnSaveCommandAsync()
        {
            var result = _pinsService.AddPin(new UserPin() 
            { 
                Autor = _authorizationService.Profile.Id, 
                Label = Label,
                Description = Description,
                Latitude = double.Parse(Latitude),
                Longitude = double.Parse(Longitude),
                Favorites = false,
                CreationDate = DateTime.Now
            });

            if (result.Result.IsSuccess)
            {
                _navigationService.GoBackAsync();
            }
            else
            {
                UserDialogs.Instance.Alert(new AlertConfig()
                {
                    OkText = Resource.ResourceManager.GetString("Ok", Resource.Culture),
                    Message = Resource.ResourceManager.GetString("ErrorAddPin", Resource.Culture)
                });
            }
            return Task.CompletedTask;
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
