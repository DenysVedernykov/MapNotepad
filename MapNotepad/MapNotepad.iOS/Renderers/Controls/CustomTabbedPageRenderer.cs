using CoreGraphics;
using MapNotepad.Controls;
using MapNotepad.iOS.Renderers.Controls;
using System.Runtime.Remoting.Contexts;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomTabbedPage), typeof(CustomTabbedPageRenderer))]
namespace MapNotepad.iOS.Renderers.Controls
{
    public class CustomTabbedPageRenderer : TabbedRenderer
    {
        UITabBarController tabbedController;

        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                tabbedController = (UITabBarController)ViewController;
            }
        }

        public override void ViewWillAppear(bool animated)
        {
            AddSelectedTabIndicator();
            base.ViewWillAppear(animated);
        }

        void AddSelectedTabIndicator()
        {
            if (base.ViewControllers != null)
            {
                var color = ((CustomTabbedPage)Element).BackgroundSelectedTab;

                UITabBar.Appearance.SelectionIndicatorImage = GetImageWithColorPosition(color.ToUIColor(), new CGSize(UIScreen.MainScreen.Bounds.Width / base.ViewControllers.Length, tabbedController.TabBar.Bounds.Size.Height+2));
            }

        }

        UIImage GetImageWithColorPosition(UIColor color, CGSize size)
        {
            var rect = new CGRect(0, 0, size.Width, size.Height);

            UIGraphics.BeginImageContextWithOptions(size, false, 0);

            color.SetFill();
            UIGraphics.RectFill(rect);

            var img = UIGraphics.GetImageFromCurrentImageContext();
            UIGraphics.EndImageContext();

            return img;

        }

    }
}