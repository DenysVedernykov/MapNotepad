using MapNotepad.ViewModels;
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

            InitializeComponent();
        }
    }
}