using System;
using Foundation;
using UIKit;
using Xamarin.Forms.Platform.iOS;
using Shiny;
using ObjCRuntime;

namespace Sample.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : FormsApplicationDelegate
    {
		public override bool FinishedLaunching(UIApplication app, NSDictionary options)
		{
			this.ShinyFinishedLaunching(new Startup());
			global::Xamarin.Forms.Forms.Init();
			this.LoadApplication(new App());
			return base.FinishedLaunching(app, options);
		}


        public override void HandleEventsForBackgroundUrl(UIApplication application, string sessionIdentifier, Action completionHandler)
            => this.ShinyHandleEventsForBackgroundUrl(sessionIdentifier, completionHandler);
    }
}
