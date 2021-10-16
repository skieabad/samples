using System;
using System.Windows.Input;
using Shiny;
using Shiny.SpeechRecognition;
using Xamarin.Forms;
using System.Reactive.Disposables;


namespace Sample
{
    public class DictationViewModel : ViewModel
    {
        CompositeDisposable disposer = new CompositeDisposable();

        public DictationViewModel(ISpeechRecognizer speech)
        {
            speech
                .WhenListeningStatusChanged()
                .SubOnMainThread(x => this.IsListening = x);


            this.ToggleListen = new Command(()  =>
            {
                if (this.IsListening)
                {
                    //this.Deactivate();
                }
                else
                {
                    if (this.UseContinuous)
                    {
                        speech
                            .ContinuousDictation()
                            .SubOnMainThread(
                                x => this.Text += " " + x,
                                ex => this.Alert(ex.ToString())
                            )
                            .DisposedBy(disposer);
                    }
                    else
                    {
                        speech
                            .ListenUntilPause()
                            .SubOnMainThread(
                                x => this.Text = x,
                                ex => this.Alert(ex.ToString())
                            );
                    }
                }
            });
        }


        public ICommand ToggleListen { get; }
        [Reactive] public bool IsListening { get; private set; }
        [Reactive] public bool UseContinuous { get; set; } = true;
        //[Reactive] public bool ListenInBackground { get; set; }
        [Reactive] public string Text { get; private set; }
    }
}
