using System;
using System.Windows.Input;
using Prism.Navigation;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Shiny;
using Shiny.CallDirectory;


namespace ShinySponsors.CallDirectory
{
    public class EntryViewModel : ViewModel
    {
        public EntryViewModel(ICallDirectoryManager manager,
                              IDialogs dialogs,
                              INavigationService navigator)
        {
            this.Save = ReactiveCommand.CreateFromTask(
                async () =>
                {
                    await manager.Save(new CallerEntry
                    {
                        PhoneNumber = Utils.ParsePhoneNumber(this.Phone!),
                        Label = this.Label!,
                        IsBlocked = this.IsBlocked
                    });
                    await dialogs.Snackbar("Caller Entry Saved Successfully");
                    await navigator.GoBack();
                },
                this.WhenAny(
                    x => x.Label,
                    x => x.Phone,
                    x => x.IsBlocked,
                    (lbl, pn, blk) =>
                    {
                        if (!Utils.TryParsePhoneNumber(pn.GetValue(), out _))
                            return false;

                        if (!blk.GetValue() && lbl.GetValue().IsEmpty())
                            return false;

                        return true;
                    }
                )
            );
        }


        public ICommand Save { get; }
        [Reactive] public string Label { get; set; }
        [Reactive] public string Phone { get; set; }
        [Reactive] public bool IsBlocked { get; set; }


        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            var e = parameters.GetValue<CallerEntry>(nameof(CallerEntry));

            if (e != null)
            {
                this.Phone = e.PhoneNumber.ToString();
                this.Label = e.Label;
                this.IsBlocked = e.IsBlocked;
            }
        }
    }
}
