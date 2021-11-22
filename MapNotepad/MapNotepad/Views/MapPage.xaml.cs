using MapNotepad.ViewModels;
using Plugin.Geolocator;
using Prism.Unity;
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
    public partial class MapPage : BaseContentPage
    {
        public MapPage()
        {
            MessagingCenter.Subscribe<PinsPageViewModel, Position>(
                this,
                "MoveToPosition",
                (sender, position) => {
                    map.MoveCamera(CameraUpdateFactory.NewPosition(position));
                });

            MessagingCenter.Subscribe<MapPageViewModel, Position>(
                this,
                "MoveToPosition",
                (sender, position) => {
                    map.MoveCamera(CameraUpdateFactory.NewPosition(position));
                });

            MessagingCenter.Subscribe<MapPageViewModel>(
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