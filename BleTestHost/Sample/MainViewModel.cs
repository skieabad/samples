using Shiny;
using Shiny.BluetoothLE.Hosting;
using System.Windows.Input;
using Xamarin.Forms;


namespace Sample
{
    public class MainViewModel : SampleViewModel
    {
        public MainViewModel()
        {
            var manager = ShinyHost.Resolve<IBleHostingManager>();

            this.WriteNotifyFlow = new Command(() =>
            {

            });
        }


        public ICommand WriteNotifyFlow { get; }
        public ICommand ReadWriteFlow { get; }
        public ICommand ContinuousNotify { get; }
    }
}
