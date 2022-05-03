using Shiny;
using Shiny.Nfc;

namespace Sample
{
    public class WriteViewModel : SampleViewModel
    {
        public WriteViewModel()
        {
            var manager = ShinyHost.Resolve<INfcManager>();
        }
    }
}
