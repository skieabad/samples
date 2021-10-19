using System;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using Shiny;
using Shiny.Locations;
using Xamarin.Forms;


namespace Sample
{
    public class GpsViewModel : ViewModel
    {
        readonly IGpsManager manager;
        CompositeDisposable disposer;


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

            this.GetCurrentPosition = this.CreateOneReading(LocationRetrieve.Current);
            this.GetLastReading = this.CreateOneReading(LocationRetrieve.Last);
            this.GetLastOrCurrent = this.CreateOneReading(LocationRetrieve.LastOrCurrent);

            this.SelectPriority = new Command(async () =>
            {
                var choice = await this.Choose(
                    "Select Priority",
                    GpsPriority.Highest.ToString(),
                    GpsPriority.Normal.ToString(),
                    GpsPriority.Low.ToString()
                );
                this.Priority = (GpsPriority)Enum.Parse(typeof(GpsPriority), choice);
            });


            this.ToggleUpdates = new Command(
                async () =>
                {
                    if (this.manager.CurrentListener != null)
                    {
                        await this.manager.StopListener();
                    }
                    else
                    {
                        var access = await this.manager.RequestAccess(new GpsRequest
                        {
                            UseBackground = this.UseBackground
                        });
                        this.Access = access.ToString();

                        if (access != AccessState.Available)
                        {
                            await this.Alert("Insufficient permissions - " + access);
                            return;
                        }

                        var request = new GpsRequest
                        {
                            UseBackground = this.UseBackground,
                            Priority = this.Priority,
                        };
                        if (IsNumeric(this.DesiredInterval))
                            request.Interval = ToInterval(this.DesiredInterval);

                        if (IsNumeric(this.ThrottledInterval))
                            request.ThrottledInterval = ToInterval(this.ThrottledInterval);

                        if (IsNumeric(this.MinimumDistanceMeters))
                            request.MinimumDistance = Distance.FromMeters(Int32.Parse(this.MinimumDistanceMeters));

                        try
                        {
                            await this.manager.StartListener(request);
                        }
                        catch (Exception ex)
                        {
                            await this.Alert(ex.ToString());
                        }
                    }
                    this.IsUpdating = this.manager.CurrentListener != null;
                },
                () =>
                {

                    var isdesired = IsNumeric(this.DesiredInterval);
                    var isthrottled = IsNumeric(this.ThrottledInterval);
                    var ismindist = IsNumeric(this.MinimumDistanceMeters);

                    if (isdesired && isthrottled)
                    {
                        var desired = ToInterval(this.DesiredInterval);
                        var throttle = ToInterval(this.ThrottledInterval);
                        if (throttle.TotalSeconds >= desired.TotalSeconds)
                            return false;
                    }
                    return true;
                }
            );

            this.UseRealtime = new Command(() =>
            {
                var rt = GpsRequest.Realtime(false);
                this.ThrottledInterval = String.Empty;
                this.DesiredInterval = rt.Interval.TotalSeconds.ToString();
                this.Priority = rt.Priority;
            });

            this.RequestAccess = new Command(async () =>
            {
                this.Access = (await this.manager.RequestAccess(new GpsRequest { UseBackground = this.UseBackground })).ToString();
            });
        }


        public override void OnAppearing()
        {
            base.OnAppearing();
            this.disposer = new CompositeDisposable();

            this.manager
                .WhenReading()
                .SubOnMainThread(this.SetValues)
                .DisposedBy(this.disposer);

            this.WhenAnyProperty(x => x.IsUpdating)
                .Select(x => x ? "Stop Listening" : "Start Updating")
                .Subscribe(x => this.ListenerText = x)
                .DisposedBy(this.disposer);

            this.WhenAnyProperty(x => x.NotificationTitle)
                .Skip(1)
                .Subscribe(x => this.manager.Title = x)
                .DisposedBy(this.disposer);

            this.WhenAnyProperty(x => x.NotificationMessage)
                .Skip(1)
                .Subscribe(x => this.manager.Message = x)
                .DisposedBy(this.disposer);

            this.WhenAnyProperty()
                .Skip(1)
                .Subscribe(_ => this.ToggleUpdates.ChangeCanExecute())
                .DisposedBy(this.disposer);
        }


        public override void OnDisappearing()
        {
            base.OnDisappearing();
            this.disposer?.Dispose();
        }


        void SetValues(IGpsReading reading)
        {
            this.Latitude = reading.Position.Latitude;
            this.Longitude = reading.Position.Longitude;
            this.Altitude = reading.Altitude;
            this.PositionAccuracy = reading.PositionAccuracy;

            this.Heading = reading.Heading;
            this.HeadingAccuracy = reading.HeadingAccuracy;
            this.Speed = reading.Speed;
            this.Timestamp = reading.Timestamp;
        }


        public Command UseRealtime { get; }
        public Command SelectPriority { get; }
        public Command GetLastReading { get; }
        public Command GetCurrentPosition { get; }
        public Command GetLastOrCurrent { get; }
        public Command RequestAccess { get; }
        public Command ToggleUpdates { get; }

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


        Command CreateOneReading(LocationRetrieve retrieve) => new Command(async () =>
        {
            var observable = retrieve switch
            {
                LocationRetrieve.Last => this.manager.GetLastReading(),
                LocationRetrieve.Current => this.manager.GetCurrentPosition(),
                _ => this.manager.GetLastReadingOrCurrentPosition()
            };
            var reading = await observable.ToTask();

            if (reading == null)
                await this.Alert("Could not getting GPS coordinates");
            else
                this.SetValues(reading);
        });


        enum LocationRetrieve
        {
            Last,
            Current,
            LastOrCurrent
        }
    }
}
