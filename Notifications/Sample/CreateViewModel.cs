using System;
using System.Linq;
using System.Windows.Input;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Reactive.Linq;
using System.Reactive.Disposables;
using Xamarin.Forms;
using Shiny.Notifications;
using Shiny;


namespace Sample
{
    public class CreateViewModel : ViewModel
    {
        readonly INotificationManager notificationManager;
        CompositeDisposable? disposer;


        public CreateViewModel()
        {
            this.notificationManager = ShinyHost.Resolve<INotificationManager>();
            this.NavToChannels = new Command(async () => await this.Navigation.PushAsync(new ChannelListPage()));

            this.SelectedDate = DateTime.Now;
            this.SelectedTime = DateTime.Now.TimeOfDay.Add(TimeSpan.FromMinutes(1));

            this.SendNow = new Command(async () => await this.BuildAndSend(
                "Test Now",
                "This is a test of the sendnow stuff",
                null
            ));
            this.Send = new Command(async () =>
            {
                if (this.NotificationTitle.IsEmpty())
                {
                    await this.Alert("Title is required");
                    return;
                }
                if (this.NotificationMessage.IsEmpty())
                {
                    await this.Alert("Message is required");
                    return;
                }
                if (this.ScheduledTime < DateTime.Now)
                {
                    await this.Alert("Scheduled Date & Time must be in the future");
                    return;
                }
                await this.BuildAndSend(
                    this.NotificationTitle,
                    this.NotificationMessage,
                    this.ScheduledTime
                );
            });

            this.PermissionCheck = new Command(async () =>
            {
                var result = await notificationManager.RequestAccess();
                await this.Alert("Permission Check Result: " + result);
            });

            this.StartChat = new Command(async () =>
                await notificationManager.Send(
                    "Shiny Chat",
                    "Hi, What's your name?",
                    "ChatName",
                    DateTime.Now.AddSeconds(10)
                )
            );
        }


        async Task BuildAndSend(string title, string message, DateTime? scheduleDate = null)
        {
            var notification = new Notification
            {
                Title = title,
                Message = message,
                BadgeCount = this.BadgeCount,
                ScheduleDate = scheduleDate,
                Channel = this.Channel
            };
            if (Int32.TryParse(this.Identifier, out var id))
            {
                notification.Id = id;
            }
            if (!this.Payload.IsEmpty())
            {
                notification.Payload = new Dictionary<string, string> {
                    { nameof(this.Payload), this.Payload }
                };
            }
            notification.Android.UseBigTextStyle = this.UseAndroidBigTextStyle;

            await this.notificationManager.Send(notification);
            this.Reset();
        }


        public ICommand NavToChannels { get; }
        public ICommand PermissionCheck { get; }
        public ICommand Send { get; }
        public ICommand SendNow { get; }
        public ICommand StartChat { get; }


        DateTime scheduledTime;
        public DateTime ScheduledTime
        {
            get => this.scheduledTime;
            private set => this.Set(ref this.scheduledTime, value);
        }


        string id;
        public string Identifier
        {
            get => this.id;
            set => this.Set(ref this.id, value);
        }


        string title = "Test Title";
        public string NotificationTitle
        {
            get => this.title;
            set => this.Set(ref this.title, value);
        }


        string msg = "Test Message";
        public string NotificationMessage
        {
            get => this.msg;
            set => this.Set(ref this.msg, value);
        }


        bool actions = true;
        public bool UseActions
        {
            get => this.actions;
            set => this.Set(ref this.actions, value);
        }


        DateTime date;
        public DateTime SelectedDate
        {
            get => this.date;
            set => this.Set(ref this.date, value);
        }


        TimeSpan time;
        public TimeSpan SelectedTime
        {
            get => this.time;
            set => this.Set(ref this.time, value);
        }


        int badge;
        public int BadgeCount
        {
            get => this.badge;
            set => this.Set(ref this.badge, value);
        }


        string payload;
        public string Payload
        {
            get => this.payload;
            set => this.Set(ref this.payload, value);
        }


        bool big;
        public bool UseAndroidBigTextStyle
        {
            get => this.big;
            set => this.Set(ref this.big, value);
        }


        string channel;
        public string? Channel
        {
            get => this.channel;
            set => this.Set(ref this.channel, value);
        }


        string[] channels;
        public string[] Channels
        {
            get => this.channels;
            private set
            {
                this.channels = value;
                this.RaisePropertyChanged();
            }
        }

        public bool IsAndroid => ShinyPlatform.Current.IsAndroid();


        public async override void OnAppearing()
        {
            base.OnAppearing();

            this.disposer = new CompositeDisposable();
            Observable
                .Interval(TimeSpan.FromSeconds(10))
                .Where(_ => this.ScheduledTime < DateTimeOffset.Now)
                .SubOnMainThread(_ => this.SelectedTime.Add(TimeSpan.FromSeconds(1)))
                .DisposedBy(this.disposer);

            this.WhenAnyProperty(x => x.SelectedDate)
                .Subscribe(_ => this.CalcDate())
                .DisposedBy(this.disposer);

            this.WhenAnyProperty(x => x.SelectedTime)
                .Subscribe(_ => this.CalcDate())
                .DisposedBy(this.disposer);

            this.Channels = (await this.notificationManager.GetChannels())
                .Select(x => x.Identifier)
                .ToArray();
        }


        public override void OnDisappearing()
        {
            base.OnDisappearing();
            this.disposer?.Dispose();
        }


        void CalcDate()
        {
            this.ScheduledTime = new DateTime(
                this.SelectedDate.Year,
                this.SelectedDate.Month,
                this.SelectedDate.Day,
                this.SelectedTime.Hours,
                this.SelectedTime.Minutes,
                0
            );
        }

        void Reset()
        {
            this.Identifier = String.Empty;
            this.Payload = String.Empty;
            this.Channel = null;
        }
    }
}