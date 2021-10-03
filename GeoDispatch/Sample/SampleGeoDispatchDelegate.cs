//using System;
//using System.Threading.Tasks;
//using Shiny.GeoDispatch;


//namespace ShinySponsors.GeoDispatch
//{
//    public class SampleGeoDispatchDelegate : IGeoDispatchDelegate
//    {
//        readonly SampleSqliteConnection conn;


//        public SampleGeoDispatchDelegate(SampleSqliteConnection conn)
//        {
//            this.conn = conn;
//        }


//        //public Task OnDispatched(Shiny.GeoDispatch.GeoDispatchMessage dispatch)
//        //    => this.conn.InsertAsync(new GeoDispatchEvent
//        //    {
//        //        Identifier = dispatch.Identifier,
//        //        Latitude = dispatch.Latitude,
//        //        Longitude = dispatch.Longitude,
//        //        RadiusMeters = dispatch.Radius.TotalMeters,
//        //        Message = dispatch.Message,
//        //        DateCreated = DateTime.UtcNow
//        //    });


//        //public async Task OnResponse(Shiny.GeoDispatch.GeoDispatchMessage dispatch, bool accept, string? textReply)
//        //{
//        //    var @event = await this.conn
//        //        .Table<GeoDispatchEvent>()
//        //        .FirstOrDefaultAsync(x =>
//        //            x.Identifier.Equals(dispatch.Identifier) &&
//        //            x.DateResponded == null
//        //        );

//        //    if (@event != null)
//        //    {
//        //        @event.DateResponded = DateTime.UtcNow;
//        //        @event.Accepted = accept;
//        //        @event.TextReply = textReply;
//        //        await this.conn.UpdateAsync(@event);
//        //    }
//        //}
//    }
//}
