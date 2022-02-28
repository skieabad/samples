using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Prism.Ioc;
using Prism.Navigation;
using Shiny;
using System.Threading.Tasks;
using Xamarin.Forms;


namespace Sample
{
    public class Startup : FrameworkStartup
    {
        public override Task RunApp(INavigationService navigator)
            => navigator.Navigate("//NavigationPage/MainPage");


        public override void ConfigureApp(Application app, IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainViewModel>();
        }


        protected override void Configure(ILoggingBuilder builder, IServiceCollection services)
        {
            services.UseBleClient();
            services.UseXfMaterialDialogs();
            services.UseGlobalCommandExceptionHandler(x => x.AlertType = ErrorAlertType.FullError);
        }
    }
}
