using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Threading.Tasks;
using System.Windows.Input;
using Shiny;
using Shiny.Nfc;
using Xamarin.Forms;


namespace Sample
{
    public class MainViewModel : SampleViewModel
    {
        IDisposable? sub;


        public MainViewModel()
        {
            var manager = ShinyHost.Resolve<INfcManager>();

            this.CheckPermission = new Command(async () =>
                await this.DoCheckPermission(manager)
            );

            this.Clear = new Command(() =>
                this.NDefRecords.Clear()
            );

            this.Read = new Command(async () =>
            {
                if (this.IsListening)
                {
                    await this.Alert("Already listening");
                    return;
                }
                await this.DoCheckPermission(manager);
                if (this.Access == AccessState.Available)
                    this.ManageObservable(manager.SingleRead());
            });

            this.Continuous = new Command(async () =>
            {
                await this.DoCheckPermission(manager);
                if (this.Access == AccessState.Available)
                {
                    if (this.IsListening)
                    {
                        this.IsListening = false;
                        this.sub?.Dispose();
                    }
                    else
                    {
                        this.ManageObservable(manager.ContinuousRead());
                    }
                }
            });
        }


        public ICommand Clear { get; }
        public ICommand Continuous { get; }
        public ICommand Read { get; }
        public ICommand CheckPermission { get; }
        public ObservableCollection<NDefItemViewModel> NDefRecords { get; } = new ObservableCollection<NDefItemViewModel>();


        AccessState state = AccessState.Unknown;
        public AccessState Access
        {
            get => this.state;
            private set => this.Set(ref this.state, value);
        }


        bool listening;
        public bool IsListening
        {
            get => this.listening;
            private set => this.Set(ref this.listening, value);
        }


        public override void OnDisappearing()
        {
            base.OnDisappearing();
            this.sub?.Dispose();
        }


        void ManageObservable(IObservable<NDefRecord[]> obs)
        {
            this.sub = obs
                .SelectMany(x => x.Select(y => new NDefItemViewModel(y)))
                .SubOnMainThread(
                    x => this.NDefRecords.Add(x),
                    async ex =>
                    {
                        this.sub?.Dispose();
                        this.IsListening = false;
                        await this.Alert(ex.ToString());
                    }
                );

            this.IsListening = true;
        }


        async Task DoCheckPermission(INfcManager? manager = null)
        {
            if (manager == null)
                this.Access = AccessState.NotSupported;
            else
                this.Access = await manager.RequestAccess().ToTask();
        }
    }
}
