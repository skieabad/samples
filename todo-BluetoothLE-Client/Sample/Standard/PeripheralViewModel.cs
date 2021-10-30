using System;
using System.Windows.Input;
using Shiny.BluetoothLE;


namespace Sample.Standard
{
    public class PeripheralViewModel : SampleViewModel
    {
        public PeripheralViewModel(IPeripheral peripheral)
        {
            this.Load = this.LoadingCommand(async () =>
            {
                //peripheral.WithConnectIf;
            });
        }


        public string Title { get; }
        public ICommand Load { get; }
    }
}
