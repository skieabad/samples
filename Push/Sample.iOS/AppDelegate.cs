using Foundation;
using Shiny;
using System;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;


namespace Sample.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : FormsApplicationDelegate
    {
		public override bool FinishedLaunching(UIApplication app, NSDictionary options)
		{
			this.ShinyFinishedLaunching(new Startup());
			Forms.Init();
			this.LoadApplication(new App());
			return base.FinishedLaunching(app, options);
		}


        public override void RegisteredForRemoteNotifications(UIApplication application, NSData deviceToken)
            => this.ShinyRegisteredForRemoteNotifications(deviceToken);


        public override void FailedToRegisterForRemoteNotifications(UIApplication application, NSError error)
            => this.ShinyFailedToRegisterForRemoteNotifications(error);


        public override void DidReceiveRemoteNotification(UIApplication application, NSDictionary userInfo, Action<UIBackgroundFetchResult> completionHandler)
            => this.ShinyDidReceiveRemoteNotification(userInfo, completionHandler);
    }
}
