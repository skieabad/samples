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
        protected override void Configure(ILoggingBuilder builder, IServiceCollection services)
        {
            // configure all your services that are needed for Shiny - Prism will get access to all of these as well
            services.AddSingleton<SampleSqliteConnection>();
            services.UseXfMaterialDialogs();
            services.UseResxLocalization();
        }


        public override void ConfigureApp(Application app, IContainerRegistry containerRegistry)
        {
            // register your viewmodels and any services that are specific only to your UI
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainViewModel>();
        }


        public override Task RunApp(INavigationService navigator)
        {
            // perform your inital navigation here
            return navigator.Navigate("/NavigationPage/MainPage");
        }
    }
}
