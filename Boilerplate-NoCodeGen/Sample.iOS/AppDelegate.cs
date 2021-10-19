using System;
using Foundation;
using UIKit;
using Xamarin.Forms.Platform.iOS;
using Shiny;


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


		// this is the "old" attach method for background jobs on iOS, it is still needed though
		public override void PerformFetch(UIApplication application, Action<UIBackgroundFetchResult> completionHandler)
		{
			this.ShinyPerformFetch(completionHandler);
		}


		// this is used by Shiny.Net.Http
		public override void HandleEventsForBackgroundUrl(UIApplication application, string sessionIdentifier, Action completionHandler)
        {
            base.HandleEventsForBackgroundUrl(application, sessionIdentifier, completionHandler);
        }


		// these 3 methods are used by all of the push providers
		public override void RegisteredForRemoteNotifications(UIApplication application, NSData deviceToken)
		{
			this.ShinyRegisteredForRemoteNotifications(deviceToken);
		}

		public override void FailedToRegisterForRemoteNotifications(UIApplication application, NSError error)
		{
			this.ShinyFailedToRegisterForRemoteNotifications(error);
		}


		public override void DidReceiveRemoteNotification(UIApplication application, NSDictionary userInfo, Action<UIBackgroundFetchResult> completionHandler)
		{
			this.ShinyDidReceiveRemoteNotification(userInfo, completionHandler);
		}

	}
}
