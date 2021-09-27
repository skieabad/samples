using System;
using System.Threading.Tasks;
using Shiny.Beacons;


namespace Sample
{
    public class MyBeaconMonitorDelegate : IBeaconMonitorDelegate
    {
        readonly SampleSqliteConnection conn;
        public MyBeaconMonitorDelegate(SampleSqliteConnection conn) => this.conn = conn;


        public Task OnStatusChanged(BeaconRegionState newStatus, BeaconRegion region)
        {
            throw new NotImplementedException();
        }
    }
}
