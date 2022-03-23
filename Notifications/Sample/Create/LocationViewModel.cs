using Shiny;
using Shiny.Locations;
using System;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;


namespace Sample.Create
{
    public class LocationViewModel : SampleViewModel
    {
        public LocationViewModel()
        {
            var gpsManager = ShinyHost.Resolve<IGpsManager>();

            this.Cancel = new Command(async () => await this.Navigation.PopModalAsync());

            this.Use = new Command(async () =>
            {
                State.CurrentNotification!.RepeatInterval = null;
                State.CurrentNotification!.ScheduleDate = null;

                State.CurrentNotification!.Geofence = new Shiny.Notifications.GeofenceTrigger
                {
                    Center = new Position(
                        this.Latitude,
                        this.Longitudue
                    ),
                    Radius = Distance.FromMeters(300),
                    Repeat = true
                };
                await this.Navigation.PopModalAsync();
            });

            this.SetCurrentLocation = new Command(async () =>
            {
                try
                { 
                    await gpsManager
                        .GetCurrentPosition()
                        .Timeout(TimeSpan.FromSeconds(20))
                        .ToTask();
                }
                catch (Exception ex)
                {
                    await this.Alert("ERROR", "Could not retrieve location - " + ex);
                }
            });
        }


        public ICommand Use { get; }
        public ICommand Cancel { get; }
        public ICommand SetCurrentLocation { get; }


        double latitude;
        public double Latitude
        {
            get => this.latitude;
            set => this.Set(ref this.latitude, value);
        }


        double longitudue;
        public double Longitudue
        {
            get => this.longitudue;
            set => this.Set(ref this.longitudue, value);
        }
    }
}
