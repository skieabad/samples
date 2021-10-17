using System;
using Xamarin.Forms;


namespace Sample
{
	public partial class ListPage : SampleContentPage
	{
		public ListPage()
		{
			this.InitializeComponent();
			this.BindingContext = new ListViewModel();
		}
    }
}