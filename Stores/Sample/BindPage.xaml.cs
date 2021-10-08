using System;
using Xamarin.Forms;


namespace Sample
{
    public partial class BindPage : ContentPage
    {
        public BindPage()
        {
            this.InitializeComponent();
            this.BindingContext = new BindViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            this.TryFireOnAppearing();
        }


        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            this.TryFireOnDisappearing();
        }
    }
}