using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms.GoogleMaps;

namespace MapNotepad.ViewModels
{
    class AddPinsPageViewModel : BaseViewModel
    {
        public AddPinsPageViewModel(INavigationService navigationService) 
            : base(navigationService)
        {
            InitialCameraUpdate = CameraUpdateFactory.NewPositionZoom(new Position(35.71d, 139.81d), 12d);
        }

        public CameraUpdate InitialCameraUpdate { get; set; }


    }
}
