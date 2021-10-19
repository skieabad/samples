using System.Threading.Tasks;
using Shiny;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Prism.Ioc;
using Prism.Navigation;
using Xamarin.Forms;


namespace Sample
{
    public class Startup : FrameworkStartup
    {
        public override void ConfigureApp(Application app, IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<ListPage, ListViewModel>();
            containerRegistry.RegisterForNavigation<EntryPage, EntryPage>();
        }


        public override Task RunApp(INavigationService navigator)
            => navigator.Navigate("NavigationPage/ListPage");


        protected override void Configure(ILoggingBuilder builder, IServiceCollection services)
        {
            services.UseGlobalCommandExceptionHandler();
            services.UseXfMaterialDialogs();
            services.UseCallDirectoryServices();
        }
    }
}
