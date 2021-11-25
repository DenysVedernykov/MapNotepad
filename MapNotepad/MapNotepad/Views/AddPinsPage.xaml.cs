using MapNotepad.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

namespace MapNotepad.Views
{
    public partial class AddPinsPage : BaseContentPage
    {
        public AddPinsPage()
        {
            MessagingCenter.Subscribe<AddPinsPageViewModel, Pin>(
                this,
                "DeletePin",
                (sender, pin) => {
                    map.Pins.Remove(pin);
                });

            MessagingCenter.Subscribe<AddPinsPageViewModel, Pin>(
                this,
                "AddPin",
                (sender, pin) => {
                    map.Pins.Add(pin);
                });

            MessagingCenter.Subscribe<AddPinsPageViewModel, Position>(
                this,
                "MoveToLocation",
                async (sender, position) => {
                    await map.MoveCamera(CameraUpdateFactory.NewPosition(position));
                });

            InitializeComponent();
        }
    }
}