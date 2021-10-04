using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Prism.Navigation;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Shiny;
using Shiny.GeoDispatch;


namespace Sample
{
    public class PendingViewModel : ViewModel
    {
        public PendingViewModel(IGeoDispatchManager geodispatch, IDialogs dialogs)
        {
            this.WhenAnyValue(x => x.SelectedDispatch)
                .WhereNotNull()
                .SubscribeAsync(async dispatch =>
                {
                    var accept = await dialogs.Confirm(
                        $"Responding to {dispatch.Identifier}-{dispatch.Message}",
                        "Respond",
                        "Accept",
                        "Reject"
                    );
                    var textReply = await dialogs.Input(
                        $"Text reply for {dispatch.Identifier}-{dispatch.Message}"
                    );
                    await geodispatch.RespondToDispatch(dispatch.Identifier, accept, textReply?.Trim());
                    this.SelectedDispatch = null;
                    this.Load.Execute(null);
                })
                .DisposedBy(this.DeactivateWith);

            this.Load = ReactiveCommand.CreateFromTask(async () =>
            {
                this.Dispatches = (await geodispatch.GetPendingDispatches()).ToList();
                this.RaisePropertyChanged(nameof(this.Dispatches));
            });
        }


        public ICommand Load { get; }
        [Reactive] public GeoDispatchMessage? SelectedDispatch { get; set; }
        public IList<GeoDispatchMessage> Dispatches { get; private set; }


        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            this.Load.Execute(null);
        }
    }
}
