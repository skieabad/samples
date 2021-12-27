using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Shiny.BluetoothLE;


namespace Sample
{
    public class PeripheralViewModel : SampleViewModel
    {
        public PeripheralViewModel(IPeripheral peripheral)
        {
            this.Title = peripheral.Name;

            this.Load = this.LoadingCommand(async () =>
            {
                this.Services = (await peripheral.GetServicesAsync())
                    .Select(x => new ServiceViewModel(x))
                    .ToList();

                this.RaisePropertyChanged(nameof(this.Services));
            });
        }


        public string Title { get; }
        public ICommand Load { get; }
        public List<ServiceViewModel> Services { get; private set; }


        public override void OnAppearing()
        {
            base.OnAppearing();
            this.Load.Execute(null);
        }
    }
}
