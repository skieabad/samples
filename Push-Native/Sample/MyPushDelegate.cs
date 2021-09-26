using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Shiny.Push;
using Samples.Models;
using Samples.Infrastructure;


namespace Sample.Push
{
    public class MyPushDelegate : IPushDelegate
    {
        readonly SampleSqliteConnection conn;
        readonly IPushManager pushManager;


        public MyPushDelegate(SampleSqliteConnection conn, IPushManager pushManager)
        {
            this.conn = conn;
            this.pushManager = pushManager;
        }

        public Task OnEntry(PushNotificationResponse push)
            => this.Insert("PUSH ENTRY");

        public Task OnReceived(PushNotification push)
            => this.Insert("PUSH RECEIVED");

        public Task OnTokenRefreshed(string token)
            => this.Insert("PUSH TOKEN CHANGE");

        Task Insert(string info) => this.conn.InsertAsync(new
        {
            Payload = info,
            Token = this.pushManager.CurrentRegistrationToken,
            Timestamp = DateTime.UtcNow
        });
    }
}
