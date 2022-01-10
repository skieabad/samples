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
#if DEBUG
            builder.AddAppCenter("YourAppCenterKey", LogLevel.Warning);
#else
            builder.AddAppCenter("YourAppCenterKey", LogLevel.Error);
#endif
        }


        public override void ConfigureServices(IServiceCollection services, IPlatform platform)
        {
            // we inject our db so we can use it in our shiny background events to store them for display later
        }
    }
}
