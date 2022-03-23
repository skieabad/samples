using System;
using System.Linq;
using System.Windows.Input;
using System.Collections.Generic;
using System.Reactive.Linq;
using Xamarin.Forms;
using Shiny.Notifications;
using Shiny;


namespace Sample.Create
{
    public class CreateViewModel : SampleViewModel
    {
        readonly INotificationManager notificationManager;


        public CreateViewModel()
        {
            State.CurrentNotification = new Notification();

            this.notificationManager = ShinyHost.Resolve<INotificationManager>();

            this.SetGeofence = this.NavigateCommand<LocationPage>(true);
            this.SetInterval = this.NavigateCommand<IntervalPage>(true);
            this.SetScheduleDate = this.NavigateCommand<SchedulePage>(true);

            this.Send = new Command(async () =>
            {
                try
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

                    var n = State.CurrentNotification!;
                    n.Title = this.NotificationTitle;
                    n.Message = this.NotificationMessage;
                    n.Thread = this.Thread;
                    n.Channel = this.Channel;
                    if (Int32.TryParse(this.Identifier, out var id))
                        n.Id = id;
                
                    if (!this.Payload.IsEmpty())
                    {
                        n.Payload = new Dictionary<string, string> {
                            { nameof(this.Payload), this.Payload }
                        };
                    }
                    n.Android.UseBigTextStyle = this.UseAndroidBigTextStyle;

                    await notificationManager.Send(n).ConfigureAwait(false);
                    await this.Alert("Notification Sent");
                    await this.Navigation.PopAsync();
                }
                catch (Exception ex)
                {
                    await this.Alert("Failed to send notification " + ex);
                }
            });


        }


        public ICommand SetScheduleDate { get; }
        public ICommand SetInterval { get; }
        public ICommand SetGeofence { get; }
        public ICommand Send { get; }
        

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

        public bool IsAndroid => ShinyHost.Resolve<IPlatform>().IsAndroid();


        public async override void OnAppearing()
        {
            base.OnAppearing();

            this.Channels = (await this.notificationManager.GetChannels())
                .Select(x => x.Identifier)
                .ToArray();
        }
    }
}