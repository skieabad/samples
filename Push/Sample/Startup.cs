using Microsoft.Extensions.DependencyInjection;
using Shiny;


namespace Sample
{
    public class Startup : ShinyStartup
    {
        public override void ConfigureServices(IServiceCollection services, IPlatform platform)
        {
            // we inject our db so we can use it in our shiny background events to store them for display later
            services.AddSingleton<SampleSqliteConnection>();
#if AZURE
            services.UsePushAzureNotificationHubs<MyPushDelegate>("LISTENER CONFIG TODO", "HUB NAME TODO");
#elif ONESIGNAL
            services.UseOneSignalPush<MyPushDelegate>("TODO");
#elif FIREBASE
            services.UseFirebaseMessaging<MyPushDelegate>();
#else
            services.UsePush<MyPushDelegate>();
#endif
        }
    }
}
