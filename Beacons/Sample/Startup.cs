using Microsoft.Extensions.DependencyInjection;
using Shiny;
using Shiny.Beacons;


namespace Sample
{
    public class Startup : ShinyStartup
    {
        public override void ConfigureServices(IServiceCollection services, IPlatform platform)
        {
            // we inject our db so we can use it in our shiny background events to store them for display later
            services.AddSingleton<SampleSqliteConnection>();
            services.UseBeaconMonitoring<MyBeaconMonitorDelegate>(new BeaconMonitorConfig().UseEstimote());
            services.UseBeaconRanging();
        }
    }
}
