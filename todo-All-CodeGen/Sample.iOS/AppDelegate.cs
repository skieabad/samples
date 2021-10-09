using Foundation;
using Xamarin.Forms.Platform.iOS;

[assembly: Shiny.ShinyApplication(
    XamarinFormsAppTypeName = "Sample.App"
)]

namespace Sample.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : FormsApplicationDelegate
    {
	}
}
