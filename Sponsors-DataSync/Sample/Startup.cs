using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using System.Threading.Tasks;
using Prism.Ioc;
using Prism.Navigation;
using Shiny;
using Xamarin.Forms;


namespace Sample
{
    public class Startup : FrameworkStartup
    {
        public override void ConfigureApp(Application app, IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage>();
        }


        public override Task RunApp(INavigationService navigator)
            => navigator.Navigate("NavigationPage/MainPage");


        protected override void Configure(ILoggingBuilder builder, IServiceCollection services)
        {
            services.UseGlobalCommandExceptionHandler();
            services.UseXfMaterialDialogs();

            services.AddSingleton<SampleSqliteConnection>();
            services.UseDataSync<SampleDataSyncDelegate>();
        }
    }
}
