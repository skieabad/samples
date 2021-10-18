using System;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Windows.Input;

using Shiny;
using Shiny.Locations;


namespace Sample
{
    public class GpsViewModel : ViewModel
    {
        readonly IGpsManager manager;
        IDisposable gpsListener;


        public GpsViewModel()
        {
            this.manager = ShinyHost.Resolve<IGpsManager>();

            var l = this.manager.CurrentListener;
            this.IsUpdating = l != null;
            this.UseBackground = l?.UseBackground ?? true;
            this.Priority = l?.Priority ?? GpsPriority.Normal;
            this.DesiredInterval = l?.Interval.TotalSeconds.ToString() ?? String.Empty;
            this.ThrottledInterval = l?.ThrottledInterval?.TotalSeconds.ToString() ?? String.Empty;
            this.MinimumDistanceMeters = l?.MinimumDistance?.TotalMeters.ToString() ?? String.Empty;

            this.NotificationTitle = manager.Title;
            this.NotificationMessage = manager.Message;

            //this.WhenAnyValue(x => x.IsUpdating)
            //    .Select(x => x ? "Stop Listening" : "Start Updating")
            //    .ToPropertyEx(this, x => x.ListenerText);

            //this.WhenAnyValue(x => x.NotificationTitle)
            //    .Skip(1)
            //    .Subscribe(x => this.manager.Title = x)
            //    .DisposedBy(this.DestroyWith);

            //this.WhenAnyValue(x => x.NotificationMessage)
            //    .Skip(1)
            //    .Subscribe(x => this.manager.Message = x)
            //    .DisposedBy(this.DestroyWith);

            //this.GetCurrentPosition = this.CreateOneReading(dialogs, LocationRetrieve.Current);
            //this.GetLastReading = this.CreateOneReading(dialogs, LocationRetrieve.Last);
            //this.GetLastOrCurrent = this.CreateOneReading(dialogs, LocationRetrieve.LastOrCurrent);

            //ReactiveCommand.Create(() => dialogs.ActionSheet(
            //    "Select Priority",
            //    false,
            //    ("Highest", () => this.Priority = GpsPriority.Highest),
            //    ("Normal", () => this.Priority = GpsPriority.Normal),
            //    ("Low", () => this.Priority = GpsPriority.Low)
            //));

            //this.ToggleUpdates = ReactiveCommand.CreateFromTask(
            //    async () =>
            //    {
            //        if (this.manager.CurrentListener != null)
            //        {
            //            await this.manager.StopListener();
            //            this.gpsListener?.Dispose();
            //        }
            //        else
            //        {
            //            var result = await dialogs.RequestAccess(async () =>
            //            {
            //                var access = await this.manager.RequestAccess(new GpsRequest
            //                {
            //                    UseBackground = this.UseBackground
            //                });
            //                this.Access = access.ToString();
            //                return access;
            //            });
            //            if (!result)
            //            {
            //                await dialogs.Alert("Insufficient permissions");
            //                return;
            //            }

            //            var request = new GpsRequest
            //            {
            //                UseBackground = this.UseBackground,
            //                Priority = this.Priority,
            //            };
            //            if (IsNumeric(this.DesiredInterval))
            //                request.Interval = ToInterval(this.DesiredInterval);

            //            if (IsNumeric(this.ThrottledInterval))
            //                request.ThrottledInterval = ToInterval(this.ThrottledInterval);

            //            if (IsNumeric(this.MinimumDistanceMeters))
            //                request.MinimumDistance = Distance.FromMeters(Int32.Parse(this.MinimumDistanceMeters));

            //            await this.manager.StartListener(request);
            //        }
            //        this.IsUpdating = this.manager.CurrentListener != null;
            //    },
            //    this.WhenAny(
            //        x => x.IsUpdating,
            //        x => x.DesiredInterval,
            //        x => x.ThrottledInterval,
            //        x => x.MinimumDistanceMeters,
            //        (u, i, t, d) =>
            //        {
            //            if (u.GetValue())
            //                return true;

            //            var isdesired = IsNumeric(i.GetValue());
            //            var isthrottled = IsNumeric(t.GetValue());
            //            var ismindist = IsNumeric(d.GetValue());

            //            if (isdesired && isthrottled)
            //            {
            //                var desired = ToInterval(i.GetValue());
            //                var throttle = ToInterval(t.GetValue());
            //                if (throttle.TotalSeconds >= desired.TotalSeconds)
            //                    return false;
            //            }
            //            return true;
            //        }
            //    )
            //);

            //this.UseRealtime = ReactiveCommand.Create(() =>
            //{
            //    var rt = GpsRequest.Realtime(false);
            //    this.ThrottledInterval = String.Empty;
            //    this.DesiredInterval = rt.Interval.TotalSeconds.ToString();
            //    this.Priority = rt.Priority;
            //});

            //this.RequestAccess = ReactiveCommand.CreateFromTask(async () =>
            //{
            //    var access = await this.manager.RequestAccess(new GpsRequest { UseBackground = this.UseBackground });
            //    this.Access = access.ToString();
            //});
            //this.BindBusyCommand(this.RequestAccess);
        }


        public override void OnAppearing()
        {
            base.OnAppearing();

            //this.gpsListener = this.manager
            //    .WhenReading()
            //    .SubOnMainThread(this.SetValues)
            //    .DisposeWith(this.DeactivateWith);
        }


        void SetValues(IGpsReading reading)
        {
            //using (this.DelayChangeNotifications())
            //{
            //    this.Latitude = reading.Position.Latitude;
            //    this.Longitude = reading.Position.Longitude;
            //    this.Altitude = reading.Altitude;
            //    this.PositionAccuracy = reading.PositionAccuracy;

            //    this.Heading = reading.Heading;
            //    this.HeadingAccuracy = reading.HeadingAccuracy;
            //    this.Speed = reading.Speed;
            //    this.Timestamp = reading.Timestamp;
            //}
        }


        public ICommand UseRealtime { get; }
        public ICommand SelectPriority { get; }
        public ICommand GetLastReading { get; }
        public ICommand GetCurrentPosition { get; }
        public ICommand GetLastOrCurrent { get; }
        public ICommand ToggleUpdates { get; }
        public ICommand RequestAccess { get; }


        public bool IsAndroid => ShinyHost.Resolve<IPlatform>().IsAndroid();

        string listenText;
        public string ListenerText
        {
            get => this.listenText;
            private set => this.Set(ref this.listenText, value);
        }

        string nTitle;
        public string NotificationTitle
        {
            get => this.nTitle;
            set => this.Set(ref this.nTitle, value);
        }

        string nMsg;
        public string NotificationMessage
        {
            get => this.nMsg;
            set => this.Set(ref this.nMsg, value);
        }

        bool useBg = true;
        public bool UseBackground
        {
            get => this.useBg;
            set => this.Set(ref this.useBg, value);
        }

        GpsPriority priority = GpsPriority.Normal;
        public GpsPriority Priority
        {
            get => this.priority;
            set => this.Set(ref this.priority, value);
        }

        string dInt;
        public string DesiredInterval
        {
            get => this.dInt;
            set => this.Set(ref this.dInt, value);
        }

        string thrInt;
        public string ThrottledInterval
        {
            get => this.thrInt;
            set => this.Set(ref this.thrInt, value);
        }

        string minDist;
        public string MinimumDistanceMeters
        {
            get => this.minDist;
            set => this.Set(ref this.minDist, value);
        }

        string access;
        public string Access
        {
            get => this.access;
            private set => this.Set(ref this.access, value);
        }

        bool updating;
        public bool IsUpdating
        {
            get => this.updating;
            private set => this.Set(ref this.updating, value);
        }

        double lat;
        public double Latitude
        {
            get => this.lat;
            private set => this.Set(ref this.lat, value);
        }

        double lng;
        public double Longitude
        {
            get => this.lng;
            private set => this.Set(ref this.lng, value);
        }

        double alt;
        public double Altitude
        {
            get => this.alt;
            private set => this.Set(ref this.alt, value);
        }

        double pAcc;
        public double PositionAccuracy
        {
            get => this.pAcc;
            private set => this.Set(ref this.pAcc, value);
        }

        double heading;
        public double Heading
        {
            get => this.heading;
            private set => this.Set(ref this.heading, value);
        }

        double hAcc;
        public double HeadingAccuracy
        {
            get => this.hAcc;
            private set => this.Set(ref this.hAcc, value);
        }

        double speed;
        public double Speed
        {
            get => this.speed;
            private set => this.Set(ref this.speed, value);
        }

        DateTime timestamp;
        public DateTime Timestamp
        {
            get => this.timestamp;
            private set => this.Set(ref this.timestamp, value);
        }


        static bool IsNumeric(string value)
        {
            if (value.IsEmpty())
                return false;

            if (Int32.TryParse(value, out var r))
                return r > 0;

            return false;
        }


        static TimeSpan ToInterval(string value)
        {
            var i = Int32.Parse(value);
            var ts = TimeSpan.FromSeconds(i);
            return ts;
        }


        //IReactiveCommand CreateOneReading(IDialogs dialogs, LocationRetrieve retrieve)
        //{
        //    IReactiveCommand command = ReactiveCommand.CreateFromTask(async ct =>
        //    {
        //        var observable = retrieve switch
        //        {
        //            LocationRetrieve.Last => this.manager.GetLastReading(),
        //            LocationRetrieve.Current => this.manager.GetCurrentPosition(),
        //            _ => this.manager.GetLastReadingOrCurrentPosition()
        //        };
        //        var reading = await observable.ToTask(ct);

        //        if (reading == null)
        //            await dialogs.Alert("Could not getting GPS coordinates");
        //        else
        //            this.SetValues(reading);
        //    });
        //    this.BindBusyCommand(command);
        //    return command;
        //}


        enum LocationRetrieve
        {
            Last,
            Current,
            LastOrCurrent
        }
    }
}
