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
    public partial class AddPinsPage : BaseContentPage
    {
        public AddPinsPage()
        {
            InitializeComponent();
        }

        private void Map_MapClicked(object sender, Xamarin.Forms.GoogleMaps.MapClickedEventArgs e)
        {
            var map = sender as Xamarin.Forms.GoogleMaps.Map;
            var position = e.Point;

            var pin = new Pin()
            {
                Label = "Point",
                Type = PinType.Place,
                Position = position
            };

            map.Pins.Add(pin);
            map.MoveToRegion(MapSpan.FromCenterAndRadius(position, Distance.FromMeters(8000)));

        }

        private void Map_CameraMoveStarted(object sender, CameraMoveStartedEventArgs e)
        {
            var map = sender as Xamarin.Forms.GoogleMaps.Map;
            var pin = new Pin()
            {
                Label = "Point 2",
                Type = PinType.Place,
                Position = new Position(35.71d, 139.83d)
            };

            map.Pins.Add(pin);
        }
    }
}