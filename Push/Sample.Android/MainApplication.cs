using Android.App;
using Android.Runtime;
using Microsoft.Extensions.Configuration;
using Shiny;
using Shiny.Push;
using System;


namespace Sample.Droid
{
    [Application]
    public class MainApplication : Application
    {
        public MainApplication(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer) { }


        public override void OnCreate()
        {
            base.OnCreate();
            this.ShinyOnCreate(new Startup
            {
#if NATIVE || FIREBASE
                // if you want to try initialization with straight config vars instead of a google-services.json - set the Firebase node in config and switch some comments in Startup.cs
                RegisterPlatform = (services, config) =>
                {

                    var cfg = config.GetSection("Firebase").Get<FirebaseConfig>() ?? throw new ArgumentException("Missing Firebase configuration");
                    services.UsePush<MyPushDelegate>(cfg);
                }
#endif
            });

        }
    }
}