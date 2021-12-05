using System.Windows.Input;
using Prism.Navigation;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Shiny;
using Shiny.BluetoothLE;
using Shiny.BluetoothLE.Managed;
using Xamarin.Forms;


namespace Sample
{
    public class ManagedPeripheralViewModel : ViewModel
    {
        public ManagedPeripheralViewModel()
        {
            this.ToggleRssi = ReactiveCommand.Create(() =>
                this.IsRssi = this.Peripheral?.ToggleRssi() ?? false
            );
        }


        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            this.peripheral = parameters.GetValue<IPeripheral>("Peripheral");
        }


        public override void OnAppearing()
        {
            base.OnAppearing();
            this.Peripheral = this.peripheral!.CreateManaged();
        }


        public override void OnDisappearing()
        {
            base.OnDisappearing();
            this.Peripheral?.Dispose();
        }


        IPeripheral? peripheral;
        public ICommand ToggleRssi { get; }
        public IManagedPeripheral? Peripheral { get; private set; }

        [Reactive] public bool IsRssi { get; private set; }
    }
}
