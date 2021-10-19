using System.Threading.Tasks;
using Shiny.Locations;

namespace Sample.ShinyStuff
{
    public class TestGeofenceDelegate : IGeofenceDelegate
    {
        public Task OnStatusChanged(GeofenceState newStatus, GeofenceRegion region) => Task.CompletedTask;
    }
}
