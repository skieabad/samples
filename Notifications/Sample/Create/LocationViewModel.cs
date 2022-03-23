using Shiny;
using Shiny.Locations;
using System.Windows.Input;
using Xamarin.Forms;


namespace Sample.Create
{
    public class LocationViewModel : SampleViewModel
    {
        public LocationViewModel()
        {
            this.Cancel = new Command(async () => await this.Navigation.PopModalAsync());

            this.Use = new Command(async () =>
            {
                State.CurrentNotification!.RepeatInterval = null;
                State.CurrentNotification!.ScheduleDate = null;

                State.CurrentNotification!.Geofence = new Shiny.Notifications.GeofenceTrigger
                {
                    Center = this.position,
                    Radius = Distance.FromMeters(300),
                    Repeat = true
                };
                await this.Navigation.PopModalAsync();
            });
        }

        public ICommand Use { get; }
        public ICommand Cancel { get; }


        Position? position;
        public Position? SelectedPosition
        {
            get => this.position;
            set => this.Set(ref this.position, value);
        }
    }
}
