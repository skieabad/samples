using System;
using Android.App;
using Android.Runtime;
using Microsoft.Extensions.DependencyInjection;
using Shiny;

namespace Sample.Droid
{
    public class MainApplication : Application
    {
        public MainApplication(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }


        public override void OnCreate()
        {
            base.OnCreate();
            this.ShinyOnCreate(new Startup
            {
                RegisterPlatformServices = (services) =>
                {
                    services.AddSingleton<IMyService, AndroidMyService>();
                }
            });
        }
    }
}

