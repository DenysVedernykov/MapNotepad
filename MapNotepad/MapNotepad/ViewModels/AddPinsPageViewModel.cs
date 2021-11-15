using MapNotepad.Helpers;
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

        public AddPinsPageViewModel(INavigationService navigationService) 
            : base(navigationService)
        {
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

        private Color _orderColorLatitude;
        public Color BorderColorLatitude
        {
            get => _orderColorLatitude;
            set => SetProperty(ref _orderColorLatitude, value);
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
            double simple;

            base.OnPropertyChanged(args);

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
                    if (double.TryParse(Longitude, out simple))
                    {
                        BorderColorLongitude = (Color)_resourceDictionary["LightGray"];
                    }
                    else
                    {
                        BorderColorLongitude = (Color)_resourceDictionary["Error"];
                    }

                    break;
                case nameof(Latitude):
                    if (double.TryParse(Latitude, out simple))
                    {
                        BorderColorLatitude = (Color)_resourceDictionary["LightGray"];
                    }
                    else
                    {
                        BorderColorLatitude = (Color)_resourceDictionary["Error"];
                    }

                    break;
            }

            IsEnableSaveButton =
                !string.IsNullOrEmpty(Label)
                && !string.IsNullOrEmpty(Description)
                && double.TryParse(Longitude, out simple)
                && double.TryParse(Latitude, out simple);
        }

        #endregion

        #region -- Private methods --

        private Task OnGoBackCommandAsync()
        {
            _navigationService.GoBackAsync();

            return Task.CompletedTask;
        }

        private Task OnSaveCommandAsync()
        {
            
            return Task.CompletedTask;
        }

        private Task OnMapClickedCommandAsync(Position position)
        {
            MessagingCenter.Send<AddPinsPageViewModel, Pin>(this, "DeletePin", _currentPin);

            _currentPin.Position = position;
            Longitude = position.Longitude.ToString();
            Latitude = position.Latitude.ToString();

            MessagingCenter.Send<AddPinsPageViewModel, Pin>(this, "AddPin", _currentPin);

            return Task.CompletedTask;
        }

        #endregion

    }
}
