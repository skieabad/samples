using System;
using System.Windows.Input;
using System.Reactive.Linq;
using Prism.Navigation;
using ReactiveUI;
using Shiny;
using Shiny.ObdInterface;
using Shiny.XamForms;
using ReactiveUI.Fody.Helpers;


namespace Sample
{
    public class MainViewModel : ViewModel
    {
        readonly IObdScanner scanner;


        public MainViewModel(INavigationService navigator, IObdScanner scanner)
        {
            this.scanner = scanner;

            this.WhenAnyValue(x => x.SelectedItem)
                .WhereNotNull()
                .SubscribeAsync(item =>
                {
                    this.SelectedItem = null;
                    return navigator.Navigate("ObdDevice", ("ObdDevice", (IObdDevice)item.Data));
                })
                .DisposedBy(this.DestroyWith);

            this.scanner
                .WhenDeviceScan()
                .Where(x => x.Action == ObdDeviceScanAction.Add)
                .Select(x => new CommandItem
                {
                    Text = x.Device.Name,
                    Data = x.Device
                })
                .Subscribe(x => this.Devices.Add(x))
                .DisposedBy(this.DestroyWith);

            this.ToggleScan = ReactiveCommand.Create(() =>
            {
                if (scanner.IsScanning)
                {
                    this.Stop();
                }
                else
                {
                    this.IsBusy = true;
                    this.Devices.Clear();
                    scanner.StartScan(RxApp.MainThreadScheduler);
                }
            });
        }


        public ICommand ToggleScan { get; }
        public ObservableList<CommandItem> Devices { get; } = new ObservableList<CommandItem>();
        [Reactive] public CommandItem? SelectedItem { get; set; }


        public override void OnDisappearing()
        {
            base.OnDisappearing();
            this.Stop();
        }


        void Stop()
        {
            this.IsBusy = false;
            this.scanner.StopScan();
        }
    }
}
