using Foundation;
using MapNotepad.Controls;
using MapNotepad.iOS.Renderers.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomTabbedPage), typeof(CustomTabbedPageRenderer))]
namespace MapNotepad.iOS.Renderers.Controls
{
    public class CustomTabbedPageRenderer : TabbedRenderer
    {
    }
}