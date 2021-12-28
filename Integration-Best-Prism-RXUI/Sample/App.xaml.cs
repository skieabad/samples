using Shiny;

namespace Sample
{
    public partial class App : FrameworkApplication
    {
        // this is only necessary if you use App.xaml
        protected override void Initialize()
        {
            this.InitializeComponent();
            this.Resources.Add(new Styles());
            base.Initialize();
        }
    }
}
