using System;
using Xamarin.Forms;


namespace Sample
{
    public partial class BasicPage : ContentPage
    {
        public BasicPage()
        {
            this.InitializeComponent();
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