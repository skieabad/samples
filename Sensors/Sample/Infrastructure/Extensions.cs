using System;
using Xamarin.Forms;


namespace Sample
{
    public static class Extensions
    {
        public static IDisposable SubOnMainThread<T>(this IObservable<T> observable, Action<T> onAction) =>
            observable.Subscribe(x => Device.BeginInvokeOnMainThread(() => onAction(x)));
    }
}
