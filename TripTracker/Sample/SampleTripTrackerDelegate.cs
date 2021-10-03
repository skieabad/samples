using System;
using System.Threading.Tasks;
using Shiny;
using Shiny.Notifications;
using Shiny.TripTracker;



namespace ShinySponsors.TripTracker
{
    public class SampleTripTrackerDelegate : TripTrackerDelegate
    {
        const string N_TITLE = "Shiny Trip";
        readonly ITripTrackerManager manager;
        readonly INotificationManager notifications;


        public SampleTripTrackerDelegate(ITripTrackerManager manager, INotificationManager notifications)
        {
            this.manager = manager;
            this.notifications = notifications;
        }


        public override Task OnTripStart(Trip trip) => this.notifications.Send(
            N_TITLE,
            $"Starting a new {this.manager.TrackingType.Value} trip"
        );


        public override async Task OnTripEnd(Trip trip)
        {
            var km = Math.Round(Distance.FromMeters(trip.TotalDistanceMeters).TotalKilometers, 0);
            var avgSpeed = Math.Round(Distance.FromMeters(trip.AverageSpeedMetersPerHour).TotalKilometers, 0);
            var time = (trip.DateFinished - trip.DateStarted).Value.ToString();

            await this.notifications.Send(
                N_TITLE,
                $"You just finished a trip that was {km} km and took {time} with an average speed of {avgSpeed} km"
            );
        }
    }
}
