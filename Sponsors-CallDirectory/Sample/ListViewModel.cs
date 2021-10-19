using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Navigation;
using Shiny;
using Shiny.CallDirectory;
using Shiny.XamForms;


namespace Sample
{
    public class ListViewModel : Shiny.Scenarios.ListViewModel<CommandItem>
    {
        readonly ICallDirectoryManager callDirectory;
        readonly IDialogs dialogs;
        readonly INavigationService navigator;


        public ListViewModel(ICallDirectoryManager callDirectory,
                             IDialogs dialogs,
                             INavigationService navigator)
        {
            this.callDirectory = callDirectory;
            this.dialogs = dialogs;
            this.navigator = navigator;
            this.NewEntry = navigator.NavigateCommand("CallDirectoryEntry");
            this.ClearEntries = dialogs.ConfirmCommand(
                () =>
                {
                    return callDirectory.ClearEntries();
                },
                "Are you sure you wish to clear all entries?"
            );
        }


        public ICommand NewEntry { get; }
        public ICommand ClearEntries { get; }


        protected override async Task<List<CommandItem>> GetData(INavigationParameters parameters)
        {
            var entries = await this.callDirectory.GetAllEntries();
            return entries
                .Select(x => new CommandItem
                {
                    Text = x.PhoneNumber.ToString(),
                    Detail = $"{x.Label} (Blocked: {x.IsBlocked})",
                    Data = x
                })
                .ToList();
        }


        protected override void OnSelectedItem(CommandItem item)
        {
            base.OnSelectedItem(item);

            var entry = (CallerEntry)item.Data;
            this.dialogs.ActionSheet(
                "What would you like to do?",
                true,
                (
                    "Delete",
                    async () =>
                    {
                        var result = await this.dialogs.Confirm($"Are you sure you wish to remove entry '{entry.PhoneNumber}'?");
                        if (result)
                            await this.callDirectory.Remove(entry.PhoneNumber);
                    }
                ),(
                    "Edit",
                    async () => await this.navigator.Navigate(
                        "CallerEntryPage",
                        (nameof(CallerEntry), entry)
                    )
                )
            );
        }
    }
}
