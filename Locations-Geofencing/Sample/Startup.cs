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

            services.UseGeofencing<GeofenceDelegate>();

            // let's send some notifications from our geofence
            services.UseNotifications();

            // we use this in the example, it isn't needed for geofencing in general
            services.UseGps();
        }
    }
}
