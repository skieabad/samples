using System.Windows.Input;
using Shiny.BluetoothLE;
using Shiny.BluetoothLE.Managed;
using Xamarin.Forms;


namespace Sample.Managed
{
    public class ManagedPeripheralViewModel : SampleViewModel
    {
        readonly IPeripheral peripheral;


        public ManagedPeripheralViewModel(IPeripheral peripheral)
        {
            this.peripheral = peripheral;
            this.ToggleRssi = new Command(() =>
                this.IsRssi = this.Peripheral.ToggleRssi()
            );
        }


        public override void OnAppearing()
        {
            base.OnAppearing();
            //this.peripheral
            //    .CreateManaged();
        }


        public override void OnDisappearing()
        {
            base.OnDisappearing();
            this.Peripheral?.Dispose();
        }


        public ICommand ToggleRssi { get; }
        public IManagedPeripheral Peripheral { get; private set; }

        bool isRssi;
        public bool IsRssi
        {
            get => this.isRssi;
            private set => this.Set(ref this.isRssi, value);
        }
    }
}
