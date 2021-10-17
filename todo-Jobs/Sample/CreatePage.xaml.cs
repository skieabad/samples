using System;
using Xamarin.Forms;


namespace Sample
{
	public partial class CreatePage : SampleContentPage
	{
		public CreatePage()
		{
			this.InitializeComponent();
			this.BindingContext = new CreateViewModel();
		}
	}
}