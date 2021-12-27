using Shiny;


namespace Sample
{
    public partial class App : FrameworkApplication
    {
        protected override void Initialize()
        {
            this.Resources.Add(new Styles());
            this.InitializeComponent();
            base.Initialize();
        }
    }
}
