using System;
using Android.App;
using Android.Content.PM;
using Xamarin.Forms.Platform.Android;

[assembly: Shiny.ShinyApplication(
    ShinyStartupTypeName = "Sample.Startup",
    XamarinFormsAppTypeName = "Sample.App"
)]

namespace Sample.Droid
{
    [Activity(
        Label = "Push Firebase",
        Icon = "@mipmap/icon",
        Theme = "@style/MainTheme",
        MainLauncher = true,
        ConfigurationChanges =
            ConfigChanges.ScreenSize |
            ConfigChanges.Orientation |
            ConfigChanges.UiMode |
            ConfigChanges.ScreenLayout |
            ConfigChanges.SmallestScreenSize
    )]
    public partial class MainActivity : FormsAppCompatActivity
    {
    }
}