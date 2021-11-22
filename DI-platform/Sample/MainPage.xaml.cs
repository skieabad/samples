using Shiny;
using Xamarin.Forms;

namespace Sample
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            this.InitializeComponent();
            this.lblHi.Text = ShinyHost.Resolve<IMyService>().SayHi();
        }
    }
}
