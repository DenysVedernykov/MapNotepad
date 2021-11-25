using System;
using System.Collections.Generic;
using System.Linq;
using ContextMenu.iOS;
using Foundation;
using MapNotepad.iOS.Services.PermissionsService;
using MapNotepad.Services.PermissionsService;
using Prism;
using Prism.Ioc;
using Prism.Unity;
using UIKit;

namespace MapNotepad.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();

            Xamarin.FormsGoogleMaps.Init("AIzaSyAcBsMCF3pIsxYwe7ebpG7FiYXPpfozXFc");

            ZXing.Net.Mobile.Forms.iOS.Platform.Init();

            ContextMenuViewRenderer.Preserve();

            LoadApplication(new App(new IOSInitializer()));

            return base.FinishedLaunching(app, options);
        }

        public class IOSInitializer : IPlatformInitializer
        {
            public void RegisterTypes(IContainerRegistry containerRegistry)
            {
                containerRegistry.RegisterInstance<IPermissionsService>(PrismApplication.Current.Container.Resolve<PermissionsService>());
            }
        }

        public override bool ContinueUserActivity(UIApplication application, NSUserActivity userActivity, UIApplicationRestorationHandler completionHandler)
        {
            if (Xamarin.Essentials.Platform.ContinueUserActivity(application, userActivity, completionHandler))
                return true;

            return base.ContinueUserActivity(application, userActivity, completionHandler);
        }
    }
}
