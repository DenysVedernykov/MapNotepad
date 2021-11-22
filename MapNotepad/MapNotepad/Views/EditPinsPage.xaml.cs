using MapNotepad.ViewModels;
using Plugin.Geolocator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Forms.Xaml;

namespace MapNotepad.Views
{
    public partial class EditPinsPage : BaseContentPage
    {
        public EditPinsPage()
        {
            MessagingCenter.Subscribe<EditPinsPageViewModel, Pin>(
                this,
                "DeletePin",
                (sender, pin) => {
                    map.Pins.Remove(pin);
                });

            MessagingCenter.Subscribe<EditPinsPageViewModel, Pin>(
                this,
                "AddPin",
                (sender, pin) => {
                    map.Pins.Add(pin);
                });

            MessagingCenter.Subscribe<EditPinsPageViewModel>(
                this,
                "MoveToMyLocation",
                async (sender) => {
                    var locator = CrossGeolocator.Current;
                    var position = await locator.GetPositionAsync();

                    await map.MoveCamera(CameraUpdateFactory.NewPosition(new Position(position.Latitude, position.Longitude)));
                });

            InitializeComponent();
        }
    }
}