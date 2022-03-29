using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shiny;


namespace Sample
{
    public class Startup : ShinyStartup
    {
        public override void ConfigureServices(IServiceCollection services, IPlatform platform)
        {
            var config = new ConfigurationBuilder()
                .AddJsonPlatformBundle("appsettings.json", false)
                .Build();
            // we inject our db so we can use it in our shiny background events to store them for display later
            services.AddSingleton<SampleSqliteConnection>();
#if AZURE
            
            services.UsePushAzureNotificationHubs<MyPushDelegate>(
                config["AzureNotificationHubsListenerConnectionString"],
                config["AzureNotificationHubsHubName"]
            );
#elif ONESIGNAL
            
            services.UseOneSignalPush<MyPushDelegate>(config["OneSignalAppId"]);
#elif FIREBASE
            services.UseFirebaseMessaging<MyPushDelegate>();
#else
            services.UsePush<MyPushDelegate>();
#endif
        }
    }
}
