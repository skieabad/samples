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


namespace Sample.Create
{
    public class CreateViewModel : SampleViewModel
    {
        readonly INotificationManager notificationManager;
        CompositeDisposable? disposer;


        public CreateViewModel()
        {
            this.notificationManager = ShinyHost.Resolve<INotificationManager>();

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

                await this.BuildAndSend(
                    this.NotificationTitle,
                    this.NotificationMessage,
                    null
                );
            });


        }


        async Task BuildAndSend(string title, string message, DateTime? scheduleDate = null)
        {
            var notification = new Notification
            {
                Title = title,
                Message = message,
                BadgeCount = this.BadgeCount,
                ScheduleDate = scheduleDate,
                //Thread = this.Thread,
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
        public ICommand SendGeofence { get; }
        public ICommand StartChat { get; }


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


        string thread;
        public string Thread
        {
            get => this.thread;
            set => this.Set(ref this.thread, value);
        }


        bool actions = true;
        public bool UseActions
        {
            get => this.actions;
            set => this.Set(ref this.actions, value);
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

            this.Channels = (await this.notificationManager.GetChannels())
                .Select(x => x.Identifier)
                .ToArray();
        }


        public override void OnDisappearing()
        {
            base.OnDisappearing();
            this.disposer?.Dispose();
        }



        void Reset()
        {
            this.Identifier = String.Empty;
            this.Payload = String.Empty;
            this.Channel = null;
        }
    }
}