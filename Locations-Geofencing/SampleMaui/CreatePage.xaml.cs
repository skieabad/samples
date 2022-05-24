namespace Sample;

public partial class CreatePage : ContentPage
{
	public CreatePage(CreateViewModel viewModel)
	{
		this.InitializeComponent();
		this.BindingContext = viewModel;
	}
}