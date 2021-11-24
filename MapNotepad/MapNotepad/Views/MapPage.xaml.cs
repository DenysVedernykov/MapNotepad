using MapNotepad.ViewModels;
using Plugin.Geolocator;
using Prism.Unity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
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

            MessagingCenter.Subscribe<MapPageViewModel, Location>(
                this,
                "MoveToMyLocation",
                async (sender, location) => {
                    await map.MoveCamera(CameraUpdateFactory.NewPosition(new Position(location.Latitude, location.Longitude)));
                });

            InitializeComponent();

            try
            {
                var assembly = typeof(MapPage).Assembly;

                Stream stream = assembly.GetManifestResourceStream("MapNotepad.Themes.DarkMapTheme.txt");
                string Json = "";
                using (var reader = new StreamReader(stream))
                {
                    Json = reader.ReadToEnd();
                }

                map.MapStyle = MapStyle.FromJson(Json);
            }
            catch (Exception e)
            {

            }

        }
    }
}