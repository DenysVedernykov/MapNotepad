using Android.Content;
using Android.Graphics;
using Android.Support.V4.Content;
using Android.Support.V4.View;
using Android.Views;
using Google.Android.Material.Tabs;
using MapNotepad.Controls;
using MapNotepad.Droid.Renderers.Controls;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms.Platform.Android.AppCompat;


[assembly: ExportRenderer(typeof(CustomTabbedPage), typeof(CustomTabbedPageRenderer))]

namespace MapNotepad.Droid.Renderers.Controls
{
    public class CustomTabbedPageRenderer : TabbedPageRenderer
    {
        public CustomTabbedPageRenderer(Context context) 
            : base(context) 
        { 
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

        }
    }
}