using Android.App;
using Android.Content;
using Android.Content.Res;
using MapNotepad.Controls;
using MapNotepad.Droid.Renderers.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomNoBorderEntry), typeof(CustomNoBorderEntryRenderer))]
namespace MapNotepad.Droid.Renderers.Controls
{
    class CustomNoBorderEntryRenderer : EntryRenderer
    {
        public CustomNoBorderEntryRenderer(Context context)
            : base(context)
        {
        }

        #region --- Ovverides ---

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.SetBackgroundColor(((Color)CustomNoBorderEntry.BackgroundColorProperty.DefaultValue).ToAndroid());
                Control.SetPadding(0, 0, 0, 0);
            }
        }

        #endregion

    }
}