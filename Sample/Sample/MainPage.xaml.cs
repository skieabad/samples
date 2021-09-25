using Xamarin.Forms;

namespace Sample
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            this.InitializeComponent();
            this.BindingContext = new MainViewModel();
        }


        protected override void OnAppearing()
        {
            base.OnAppearing();
            (this.BindingContext as ViewModel)?.OnAppearing();
        }


        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            (this.BindingContext as ViewModel)?.OnDisappearing();
        }
    }
}
