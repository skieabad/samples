using Xamarin.Forms;

namespace Sample
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            this.Resources.Add(new Styles());
            this.MainPage = new NavigationPage(new MainPage());
        }
    }
}
