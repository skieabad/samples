using Prism.DryIoc;
using Prism.Ioc;

namespace Sample
{
    public partial class App : PrismApplication
    {
        protected override async void OnInitialized()
        {
            this.InitializeComponent();
            await this.NavigationService.NavigateAsync("NavigationPage/MainPage");
        }


        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainViewModel>();
        }
    }
}
