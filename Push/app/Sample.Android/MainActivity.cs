using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Shiny;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;


namespace Sample.Droid
{
    [Activity(
        Label = "Push",
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
    [IntentFilter(
        new [] {  Shiny.Push.Constants.ShinyIntentClickAction }, 
        Categories = new[] { Intent.CategoryDefault }
    )]
    public class MainActivity : FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            base.OnCreate(savedInstanceState);
            Forms.Init(this, savedInstanceState);

            this.LoadApplication(new App());
        }


        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            this.ShinyOnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}