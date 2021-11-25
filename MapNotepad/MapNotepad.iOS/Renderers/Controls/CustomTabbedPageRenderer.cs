using MapNotepad.Controls;
using MapNotepad.iOS.Renderers.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomTabbedPage), typeof(CustomTabbedPageRenderer))]
namespace MapNotepad.iOS.Renderers.Controls
{
    public class CustomTabbedPageRenderer : TabbedRenderer
    {
    }
}