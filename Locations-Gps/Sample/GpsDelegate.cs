using System;
using System.Threading.Tasks;
using Shiny.Locations;


namespace Sample
{
    public class GpsDelegate : IGpsDelegate
    {
        readonly SampleSqliteConnection conn;
        public GpsDelegate(SampleSqliteConnection conn) => this.conn = conn;


        public Task OnReading(IGpsReading reading) => this.conn.InsertAsync(new ShinyEvent
        {
            Text = $"{reading.Position.Latitude} / {reading.Position.Longitude} - H: {reading.Heading}",
            Detail = $"Acc: {reading.PositionAccuracy} - SP: {reading.Speed}",
            Timestamp = reading.Timestamp.ToLocalTime()
        });

            //    new GpsEvent
            //{
            //    Latitude = reading.Position.Latitude,
            //    Longitude = reading.Position.Longitude,
            //    Altitude = reading.Altitude,
            //    PositionAccuracy = reading.PositionAccuracy,
            //    Heading = reading.Heading,
            //    HeadingAccuracy = reading.HeadingAccuracy,
            //    Speed = reading.Speed,
            //    Date = reading.Timestamp.ToLocalTime()
            //});
    }
}
