using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Shiny;
using Shiny.Jobs;
using Shiny.Notifications;


namespace Sample
{
    public class CreateViewModel : ViewModel
    {
        readonly IJobManager jobManager;
        readonly INotificationManager notifications;


        public CreateViewModel()
        {
            this.jobManager = ShinyHost.Resolve<IJobManager>();
            this.notifications = ShinyHost.Resolve<INotificationManager>();

            var valObs = this.WhenAnyProperty(
                x => x.JobName,
                x => x.SecondsToRun,
                (name, seconds) =>
                    !name.GetValue().IsEmpty() &&
                    seconds.GetValue() >= 10
            );

            this.CreateJob = ReactiveCommand.CreateFromTask(
                async _ =>
                {
                    var job = new JobInfo(typeof(SampleJob), this.JobName.Trim())
                    {
                        Repeat = this.Repeat,
                        BatteryNotLow = this.BatteryNotLow,
                        DeviceCharging = this.DeviceCharging,
                        RunOnForeground = this.RunOnForeground,
                        RequiredInternetAccess = (InternetAccess)Enum.Parse(typeof(InternetAccess), this.RequiredInternetAccess)
                    };
                    job.SetParameter("SecondsToRun", this.SecondsToRun);
                    await this.jobManager.Register(job);
                    await navigator.GoBack();
                },
                valObs
            );

            this.RunAsTask = ReactiveCommand.Create(
                () => this.jobManager.RunTask(this.JobName + "Task", async _ =>
                {
                    await notifications.Send("Shiny", $"Task {this.JobName} Started");
                    var ts = TimeSpan.FromSeconds(this.SecondsToRun);
                    await Task.Delay(ts);
                    await notifications.Send("Shiny", $"Task {this.JobName} Finshed");
                }),
                valObs
            );

            this.ChangeRequiredInternetAccess = ReactiveCommand.CreateFromTask(async () =>
            {
                var cfg = new Dictionary<string, Action>
                {
                    {
                        InternetAccess.None.ToString(),
                        () => this.RequiredInternetAccess = InternetAccess.None.ToString()
                    },
                    {
                        InternetAccess.Any.ToString(),
                        () => this.RequiredInternetAccess = InternetAccess.Any.ToString()
                    },
                    {
                        InternetAccess.Unmetered.ToString(),
                        () => this.RequiredInternetAccess = InternetAccess.Any.ToString()
                    }
                };

                await this.dialogs.ActionSheet("Select", cfg);
            });
        }


        public ICommand CreateJob { get; }
        public ICommand RunAsTask { get; }
        public ICommand ChangeRequiredInternetAccess { get; }

        string access;
        public string AccessStatus
        {
            get => this.access;
            private set => this.Set(ref this.access, value);
        }


        string jobName = "TestJob";
        public string JobName
        {
            get => this.jobName;
            set => this.Set(ref this.jobName, value);
        }


        int seconds = 10;
        public int SecondsToRun
        {
            get => this.seconds;
            set => this.Set(ref this.seconds, value);
        }


        string inetaccess = InternetAccess.None.ToString();
        public string RequiredInternetAccess
        {
            get => this.inetaccess;
            set => this.Set(ref this.inetaccess, value);
        }


        bool battery;
        public bool BatteryNotLow
        {
            get => this.battery;
            set => this.Set(ref this.battery, value);
        }


        bool charging;
        public bool DeviceCharging
        {
            get => this.charging;
            set => this.Set(ref this.charging, value);
        }


        bool repeat = true;
        public bool Repeat
        {
            get => this.repeat;
            set => this.Set(ref this.repeat, value);
        }


        bool foreground;
        public bool RunOnForeground
        {
            get => this.foreground;
            set => this.Set(ref this.foreground, value);
        }


        public override async void OnAppearing()
        {
            base.OnAppearing();
            this.AccessStatus = (await this.jobManager.RequestAccess()).ToString();
        }
    }
}
