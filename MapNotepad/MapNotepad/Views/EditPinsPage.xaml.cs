using MapNotepad.ViewModels;
using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

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

            MessagingCenter.Subscribe<EditPinsPageViewModel, Position>(
                this,
                "MoveToLocation",
                async (sender, position) => {
                    await map.MoveCamera(CameraUpdateFactory.NewPosition(position));
                });

            MessagingCenter.Subscribe<EditPinsPageViewModel, string>(
                this,
                "ThemeChange",
                async (sender, path) => {
                    try
                    {
                        var assembly = typeof(MapPage).Assembly;

                        Stream stream = assembly.GetManifestResourceStream(path);
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
                });

            InitializeComponent();
        }
    }
}