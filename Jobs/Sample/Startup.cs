using Microsoft.Extensions.DependencyInjection;
using Shiny;
using Shiny.Jobs;

namespace Sample
{
    public class Startup : ShinyStartup
    {
        public override void ConfigureServices(IServiceCollection services, IPlatform platform)
        {
            // we inject our db so we can use it in our shiny background events to store them for display later
            services.AddSingleton<SampleSqliteConnection>();

            services.AddSingleton<JobLoggerTask>();
            services.UseJobs(true);
            services.UseNotifications(); // adding notifications for some job fun

            // OPTION 1 - register with the type
            //services.RegisterJob(typeof(SampleJob));

            // OPTION 2 - register with some options
            services.RegisterJob(new JobInfo(typeof(SampleJob))
            {
                RequiredInternetAccess = InternetAccess.Any,
                RunOnForeground = true, // this job will run roughly every 30 seconds that your app is in the foreground
                DeviceCharging = false,
                BatteryNotLow = true,
                Repeat = true // this is the default
            });
        }
    }
}
