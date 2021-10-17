using System;
using System.Threading.Tasks;
using Shiny;
using Shiny.Locations;
using Shiny.Notifications;


namespace Sample
{
    public class GeofenceDelegate : IGeofenceDelegate
    {
        readonly INotificationManager notificationManager;
        readonly SampleSqliteConnection conn;


        public GeofenceDelegate(INotificationManager notificationManager, SampleSqliteConnection conn)
        {
            this.notificationManager = notificationManager;
            this.conn = conn;
        }


        public async Task OnStatusChanged(GeofenceState newStatus, GeofenceRegion region)
        {
            //await this.services.Connection.InsertAsync(new GeofenceEvent
            //{
            //    Identifier = region.Identifier,
            //    Entered = newStatus == GeofenceState.Entered,
            //    Date = DateTime.Now
            //});
            //await this.services.Notifications.Send(
            //    this.GetType(),
            //    newStatus == GeofenceState.Entered,
            //    "Geofence Event",
            //    $"{region.Identifier} was {newStatus}"
            //);
        }
    }
}
