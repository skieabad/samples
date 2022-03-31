using Shiny;
using Shiny.Push;
using Xamarin.Forms;


namespace Sample
{
    public partial class MainPage : TabbedPage
    {
        bool init = true;

        public MainPage()
        {
            this.InitializeComponent();
        }


        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (init)
            {
                var push = ShinyHost.Resolve<IPushManager>() as IPushTagSupport;
                if (push != null)
                    this.Children.Add(new TagsPage());

                init = false;
            }
        }
    }
}