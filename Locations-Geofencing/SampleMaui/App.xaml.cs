[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace Sample;


public partial class App : Application
{
    public App(MainPage page)
    {
        this.InitializeComponent();
        this.MainPage = page;
    }
}