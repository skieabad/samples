using System;
using System.Threading.Tasks;
using ReactiveUI.Fody.Helpers;
using Shiny.MediaSync;


namespace Sample
{
    public class SampleMediaSyncDelegate : MediaSyncDelegate
    {
        readonly SampleSqliteConnection conn;
        public SampleMediaSyncDelegate(SampleSqliteConnection conn) => this.conn = conn;


        [Reactive] public bool CanSyncImages { get; set; }
        [Reactive] public bool CanSyncVideos { get; set; }
        [Reactive] public bool CanSyncAudio { get; set; }


        public override async Task<bool> CanSync(MediaAsset media)
        {
            switch (media.Type)
            {
                case MediaTypes.Audio: return this.CanSyncAudio;
                case MediaTypes.Image: return this.CanSyncImages;
                case MediaTypes.Video: return this.CanSyncVideos;
                default: return false;
            }
        }


        public override Task OnSyncCompleted(MediaAsset media) => this.conn.InsertAsync(new ShinyEvent
        {
            Text = media.Type.ToString(),
            Detail = media.FilePath,
            Timestamp = DateTime.UtcNow
        });
    }
}
