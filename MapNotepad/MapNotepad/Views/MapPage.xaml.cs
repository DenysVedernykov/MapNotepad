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
            InitializeComponent();

            var pinTokyo = new Pin()
            {
                Type = PinType.Generic,
                Label = "Tokyo SKYTREE",
                Address = "Sumida-ku, Tokyo, Japan",
                Position = new Position(35.71d, 139.81d),
                Tag = "id_tokyo",
                IsVisible = true
            };

            map.Pins.Add(pinTokyo);
            map.MoveToRegion(MapSpan.FromCenterAndRadius(pinTokyo.Position, Distance.FromMeters(5000)));
        }
    }
}