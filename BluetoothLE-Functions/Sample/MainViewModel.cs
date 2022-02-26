using ReactiveUI;
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
                Create("GetAllCharacteristicAsync", cmd => DoPeripheral(bleManager, cmd, async peripheral =>
                {
                    var chs = await peripheral.GetAllCharacteristicsAsync();
                    cmd.Detail = $"Found {chs.Count} Characteristics";
                }))
            };
        }



        public List<CommandItem> Functions { get; }


        async Task DoPeripheral(IBleManager bleManager, CommandItem cmd, Func<IPeripheral, Task> doWork)
        {
            IPeripheral? peripheral = null;
            try
            {
                cmd.Detail = "Searching for Peripheral";
                peripheral = await bleManager
                    .ScanUntilPeripheralFound(Constants.ScanPeripheralName)
                    .Timeout(TimeSpan.FromSeconds(20))
                    .ToTask();

                await doWork(peripheral).ConfigureAwait(false);
            }
            catch
            {
                cmd.Detail = String.Empty;
                throw;
            }
            finally
            {
                peripheral?.CancelConnection();
            }
        }



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
