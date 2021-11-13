using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Shiny;


namespace Sample
{
    public class Startup : ShinyStartup
    {
        public override void ConfigureLogging(ILoggingBuilder builder, IPlatform platform)
        {
            base.ConfigureLogging(builder, platform);
            builder.SetMinimumLevel(LogLevel.Debug);
        }


        public override void ConfigureServices(IServiceCollection services, IPlatform platform)
        {
            // we inject our db so we can use it in our shiny background events to store them for display later
            services.AddSingleton<SampleSqliteConnection>();
            services.UseNotifications();
            services.UseHttpTransfers<HttpTransferDelegate>();
        }
    }
}
