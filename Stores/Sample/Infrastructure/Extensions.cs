using System;
using Xamarin.Forms;


namespace Sample
{
    public static class Extensions
    {
        public static void TryFireOnAppearing(this Page page)
            => (page.BindingContext as ViewModel)?.OnAppearing();

        public static void TryFireOnDisappearing(this Page page)
            => (page.BindingContext as ViewModel)?.OnDisappearing();
    }
}
