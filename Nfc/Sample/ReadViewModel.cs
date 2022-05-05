using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Text;
using System.Windows.Input;
using Shiny;
using Shiny.Nfc;


namespace Sample
{
    public class ReadViewModel : SampleViewModel
    {
        IDisposable? sub;


        public ReadViewModel()
        {
            var manager = ShinyHost.Resolve<INfcManager>();

            this.Listen = this.LoadingCommand(async () =>
            {
                if (this.IsListening)
                {
                    this.sub?.Dispose();
                    this.IsListening = false;
                }
                else
                { 
                    var access = await manager.RequestAccess().ToTask();

                    if (access != AccessState.Available)
                    {
                        await this.Alert("Permission failed: " + access);
                    }
                    else
                    {
                        this.sub = manager
                            .WhenRecordsDetected()
                            .Subscribe(
                                message =>
                                {
                                    this.TagId = Encoding.UTF8.GetString(message.Tag.Identifier);
                                    this.Records = message
                                        .Records
                                        .Select(x => new NDefItemViewModel(x))
                                        .ToList();
                                },
                                async ex =>
                                {
                                    await this.Alert("Error Starting NFC:" + ex);
                                    this.IsListening = false;
                                }
                            );

                        this.IsListening = true;
                    }
                }
            });
        }


        public ICommand Listen { get; }


        bool listening;
        public bool IsListening
        {
            get => this.listening;
            private set => this.Set(ref this.listening, value);
        }


        string tagId = "";
        public string TagId
        {
            get => this.tagId;
            private set => this.Set(ref this.tagId, value);
        }


        List<NDefItemViewModel> records;
        public List<NDefItemViewModel> Records
        {
            get => this.records;
            private set
            {
                this.records = value;
                this.RaisePropertyChanged();
            }
        }


        public override void OnDisappearing()
        {
            base.OnDisappearing();
            this.sub?.Dispose();
            this.IsListening = false;
        }
    }
}
