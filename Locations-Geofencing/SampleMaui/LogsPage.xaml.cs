namespace Sample;


public partial class LogsPage : SampleContentPage
{
    public LogsPage(LogsViewModel viewModel)
    {
        this.InitializeComponent();
        this.BindingContext = viewModel;
    }
}