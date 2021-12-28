using Shiny;
using Xamarin.Forms;

namespace Sample
{
    public partial class App : Application
    {
        public App()
        {
            this.InitializeComponent();
            this.Resources.Add(new Styles());
        }
    }
}
