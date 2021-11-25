using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using Acr.UserDialogs;
using Xamarin.Forms.GoogleMaps.Android;
using MapNotepad.Services.PermissionsService;
using Prism;
using Prism.Ioc;
using Prism.Unity;
using MapNotepad.Droid.Services.PermissionsService;
using ContextMenu.Droid;
using Plugin.CurrentActivity;
using Plugin.Permissions;

namespace MapNotepad.Droid
{
    [Activity(Label = "MapNotepad", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize, ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        private static PermissionsService _permissionsService = new PermissionsService();
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            UserDialogs.Init(this);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            ZXing.Net.Mobile.Forms.Android.Platform.Init();

            Xamarin.FormsGoogleMaps.Init(this, savedInstanceState);

            ContextMenuViewRenderer.Preserve();

            CrossCurrentActivity.Current.Init(this, savedInstanceState);

            LoadApplication(new App(new AndroidInitializer()));
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public class AndroidInitializer : IPlatformInitializer
        {
            public void RegisterTypes(IContainerRegistry containerRegistry)
            {
                containerRegistry.RegisterInstance<IPermissionsService>(_permissionsService);
            }
        }
    }
}