using Microsoft.Extensions.DependencyInjection;
using Shiny;


namespace Sample
{
    public class Startup : ShinyStartup
    {
        public override void ConfigureServices(IServiceCollection services, IPlatform platform)
        {
            services.UseBleClient();
            services.UseXfMaterialDialogs();
            services.UseGlobalCommandExceptionHandler(x => x.AlertType = ErrorAlertType.FullError);
        }
    }
}
