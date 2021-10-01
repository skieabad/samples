using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Prism.Ioc;
using Prism.Navigation;
using Shiny;
using Shiny.Beacons;
using System.Threading.Tasks;
using Xamarin.Forms;


namespace Sample
{
    public class Startup : FrameworkStartup
    {
        public override void ConfigureApp(Application app, IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainViewModel>();
            containerRegistry.RegisterForNavigation<LogsPage, LogsViewModel>();
            containerRegistry.RegisterForNavigation<CreatePage, CreateViewModel>();
            containerRegistry.RegisterForNavigation<RangingPage, RangingViewModel>();
            containerRegistry.RegisterForNavigation<MonitoringPage, MonitoringViewModel>();
            containerRegistry.RegisterForNavigation<ManagedBeaconPage, ManagedBeaconViewModel>();
        }


        public override Task RunApp(INavigationService navigator)
            => navigator.Navigate("MainPage/NavigationPage/RangingPage");


        protected override void Configure(ILoggingBuilder builder, IServiceCollection services)
        {
            // we inject our db so we can use it in our shiny background events to store them for display later
            services.UseXfMaterialDialogs();
            services.UseGlobalCommandExceptionHandler();

            services.AddSingleton<SampleSqliteConnection>();
            services.UseBeaconMonitoring<MyBeaconMonitorDelegate>(new BeaconMonitorConfig().UseEstimote());
            services.UseBeaconRanging();
        }
    }
}
