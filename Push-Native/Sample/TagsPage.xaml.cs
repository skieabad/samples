using System;
using Xamarin.Forms;


namespace Sample
{
    public partial class TagsPage : ContentPage
    {
        public TagsPage()
        {
            this.InitializeComponent();
            this.BindingContext = new TagsViewModel();
        }
    }
}