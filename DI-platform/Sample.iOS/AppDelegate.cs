using Foundation;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using Shiny;
using Microsoft.Extensions.DependencyInjection;

[assembly: Shiny.ShinyApplication(
    ShinyStartupTypeName = "Sample.Startup",
    XamarinFormsAppTypeName = "Sample.App"
)]

namespace Sample.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : FormsApplicationDelegate
    {
        public bool FinishedLaunching(UIKit.UIApplication application, NSDictionary launchOptions)
        {
            this.ShinyFinishedLaunching(new Startup
            {
                RegisterPlatformServices = (services) =>
                {
                    services.AddSingleton<IMyService, iOSMyService>();
                }
            });
            this.LoadApplication(new App());
            Forms.Init();
            return true;
        }
    }
}
