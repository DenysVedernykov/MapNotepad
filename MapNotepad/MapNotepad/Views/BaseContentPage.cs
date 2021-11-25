using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace MapNotepad.Views
{
    public class BaseContentPage : ContentPage
    {
        public BaseContentPage()
        {
            On<iOS>().SetUseSafeArea(true);
        }
    }
}
