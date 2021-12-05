using System;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using System.Windows.Input;
using Prism.Navigation;
using ReactiveUI;
using Shiny;
using Shiny.BluetoothLE;
using Shiny.BluetoothLE.Managed;
using Xamarin.Forms;


namespace Sample
{
    public class ManagedScanViewModel : ViewModel
    {
        readonly IManagedScan scanner;


        public ManagedScanViewModel(IBleManager bleManager, INavigationService navigator)
        {
            this.scanner = bleManager
                .CreateManagedScanner(RxApp.MainThreadScheduler, TimeSpan.FromSeconds(10))
                .DisposedBy(this.DeactivateWith);

            this.Toggle = new Command(async () =>
                this.IsBusy = await this.scanner.Toggle()
            );

            this.WhenAnyProperty(x => x.SelectedPeripheral)
                .Skip(1)
                .WhereNotNull()
                .Subscribe(async x =>
                {
                    this.SelectedPeripheral = null;
                    this.scanner.Stop();
                    await navigator.Navigate(
                        nameof(ManagedScanPage),
                        ("Peripheral", x.Peripheral)
                    );
                });
        }


        public ICommand Toggle { get; }


        ManagedScanResult? selected;
        public ManagedScanResult? SelectedPeripheral
        {
            get => this.selected;
            set
            {
                this.selected = value;
                this.RaisePropertyChanged();
            }
        }


        public ObservableCollection<ManagedScanResult> Peripherals
            => this.scanner.Peripherals;
    }
}

