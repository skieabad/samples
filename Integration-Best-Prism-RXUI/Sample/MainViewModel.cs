using Prism.Navigation;

using Shiny;

namespace Sample
{
    public class MainViewModel : ViewModel
    {
        // note that this Prism viewmodel has access to Prism & Shiny services
        // also note that ReactiveUI is also very available within this viewmodel
        public MainViewModel(INavigationService navigator)
        {
        }


        public override async void OnAppearing()
        {
            base.OnAppearing();
            await this.Dialogs.Alert(this["Strings:Test"]!);
        }
    }
}
