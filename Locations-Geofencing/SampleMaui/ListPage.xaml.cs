namespace Sample;

public partial class ListPage : SampleContentPage
{
	public ListPage(ListViewModel viewModel)
	{
		this.InitializeComponent();
		this.BindingContext = viewModel;
	}
}