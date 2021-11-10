using System;
using System.Threading.Tasks;
using Shiny.Net.Http;
using Shiny.Notifications;


namespace Sample
{
    public class HttpTransferDelegate : IHttpTransferDelegate
    {
        readonly INotificationManager notificationManager;
        readonly SampleSqliteConnection conn;


        public HttpTransferDelegate(INotificationManager notificationManager, SampleSqliteConnection conn)
        {
            this.notificationManager = notificationManager;
            this.conn = conn;
        }


        public Task OnError(HttpTransfer transfer, Exception ex)
            => this.CreateHttpTransferEvent(transfer);



        public Task OnCompleted(HttpTransfer transfer)
            => this.CreateHttpTransferEvent(transfer);


        async Task CreateHttpTransferEvent(HttpTransfer transfer)
        {
            var detail = transfer.Status == HttpTransferState.Completed ? $"Completed" : "Failed";

            await this.conn.InsertAsync(new ShinyEvent
            {
                Text = transfer.Identifier,
                Detail = detail
            });
            await this.notificationManager.Send(
                "HTTP Transfer",
                detail
            );
        }
    }
}
