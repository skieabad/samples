using Xamarin.Forms;


namespace Sample
{
    public partial class MainPage : TabbedPage
    {
        public MainPage()
        {
            this.InitializeComponent();
            this.BindingContext = new MainViewModel();
        }
    }
}