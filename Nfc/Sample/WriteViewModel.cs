using System;
using System.Text;
using System.Windows.Input;
using Shiny;
using Shiny.Nfc;

namespace Sample
{
    public class WriteViewModel : SampleViewModel
    {
        public WriteViewModel()
        {
            var manager = ShinyHost.Resolve<INfcManager>();

            this.Write = this.LoadingCommand(async () =>
            {
                manager
                    .Broadcast(false, new NDefRecord
                    {

                    })
                    .Subscribe(
                        tag => this.Alert("Success wrote to tag: " + Encoding.UTF8.GetString(tag.Identifier)),
                        ex => this.Alert("Failed to write: " + ex)
                    );
            });
        }


        public ICommand Write { get; }
    }
}
