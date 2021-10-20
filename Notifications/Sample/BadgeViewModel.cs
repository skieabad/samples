using System;
using System.Reactive.Linq;
using System.Windows.Input;
using Shiny;
using Shiny.Notifications;

using Xamarin.Forms;

namespace Sample
{
    public class BadgeViewModel : ViewModel
    {
        readonly INotificationManager notifications;
        IDisposable? sub;


        public BadgeViewModel()
        {
            this.notifications = ShinyHost.Resolve<INotificationManager>();
            this.Clear = new Command(() => this.Badge = 0);
        }


        public override void OnAppearing()
        {
            base.OnAppearing();
            this.Badge = this.notifications.Badge;

            this.sub = this.WhenAnyProperty(x => x.Badge)
                .Skip(1)
                .Throttle(TimeSpan.FromMilliseconds(500))
                .DistinctUntilChanged()
                .Subscribe(badge => notifications.Badge = badge);
        }


        public override void OnDisappearing()
        {
            base.OnDisappearing();
            this.sub?.Dispose();
        }


        public ICommand Clear { get; }


        int badge;
        public int Badge
        {
            get => this.badge;
            set => this.Set(ref this.badge, value);
        }
    }
}

