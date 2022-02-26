using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Shiny;
using Shiny.BluetoothLE;
using Shiny.Extensions.Dialogs;
using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Threading.Tasks;


namespace Sample
{
    public class MainViewModel : ViewModel
    {
        public MainViewModel(IBleManager bleManager, IDialogs dialogs)
        {
            this.Functions = new List<CommandItem>
            {
                Create("GetAllCharacteristicAsync", async (cmd) =>
                {
                    cmd.Detail = "Searching for Peripheral";
                    var peripheral = await bleManager.ScanUntilPeripheralFound("").Timeout(TimeSpan.FromSeconds(20)).ToTask();
                    cmd.Detail = "Peripheral Found";
                    var chs = await peripheral.GetAllCharacteristicsAsync();
                    await dialogs.Alert($"Found {chs.Count} Characteristics");
                })
            };
        }


        public List<CommandItem> Functions { get; }
        [Reactive] public string Log { get; set; }


        CommandItem Create(string title, Func<CommandItem, Task> action)
        {
            var command = new CommandItem { Text = title };
            command.PrimaryCommand = ReactiveCommand.CreateFromTask(
                async () => await action(command).ConfigureAwait(false)
            );
            return command;
        }
    }
}
