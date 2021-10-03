using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Windows.Input;
using Prism.Navigation;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Shiny;
using Shiny.ObdInterface;
using Shiny.XamForms;
using UnitsNet;


namespace Sample
{
    public class DeviceViewModel : ViewModel
    {
        IObdDevice? device;


        public DeviceViewModel(IDialogs dialogs)
        {
            this.ConnectToggle = ReactiveCommand.CreateFromTask(async () =>
            {
                if (this.device.IsConnected)
                {
                    this.device.Disconnect();
                }
                else
                {
                    //await dialogs.LoadingTask(
                    //    async () =>
                        await this
                            .device
                            .Connect()
                            .Timeout(TimeSpan.FromSeconds(10));
                    //    "Connecting...",
                    //    true
                    //);
                }
            });
        }


        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            this.device = parameters.GetValue<IObdDevice>("ObdDevice");
            this.Title = this.device.Name;

            this.Commands = new List<DeviceCommandItem>
            {
                this.Errors(),
                this.Hook("VIN", this.device.VehicleIdentificationNumber()),
                this.Hook("Speed", this.device.VehicleSpeed().Select(
                    x => this.UseMetric
                        ? $"{x.KilometersPerHour} km/h"
                        : $"{x.MilesPerHour} mph"
                    )
                ),
                this.Hook("Engine RPM", this.device.EngineSpeed().Select(
                    x => x.RevolutionsPerMinute.ToString()
                )),
                this.Hook("Fuel Level", this.device.FuelTankLevel().Select(
                    x => x + "%"
                )),
                this.Hook("Fuel Rate", this.device.EngineFuelRate().Select(
                    x => this.UseMetric ? $"{x.LitersPerSecond} l/s" : $"{x.UsGallonsPerSecond} g/s"
                )),
                this.Hook("Mass Air Flow", this.device.MassAirFlow().Select(
                    x => x.GramsPerSecond + "g/s"
                )),
                this.Hook("Throttle %", this.device.ThrottlePosition().Select(
                    x => x + "%"
                )),
                this.Hook("Distance Since Codes Cleared", this.device.DistanceSinceCodesCleared().Select(
                    x => this.UseMetric
                        ? $"{x.Kilometers} km"
                        : $"{x.Miles} m"
                )),
                this.HookTemp("Air Intake Temp", this.device.AirIntakeTemperature()),
                this.HookTemp("Coolant Temp", this.device.EngineCoolantTemperature()),
                this.HookTemp("Oil Temp", this.device.EngineOilTemperature()),
            };
            this.device
                .ConnectedStatusChanged
                .SubOnMainThread(x =>
                {
                    this.IsDeviceConnected = x;
                    this.Commands.ForEach(cmd => cmd.IsDeviceConnected = x);
                })
                .DisposedBy(this.DeactivateWith);

            this.RaisePropertyChanged(nameof(this.Commands));
        }


        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            base.OnNavigatedFrom(parameters);
            this.device?.Disconnect();
        }


        public ICommand ConnectToggle { get; }
        public List<DeviceCommandItem> Commands { get; private set; }
        [Reactive] public bool IsDeviceConnected { get; private set; }
        [Reactive] public bool UseMetric { get; set; } = true;


        DeviceCommandItem Errors()
        {
            var item = new DeviceCommandItem
            {
                Text = "Error",
                Detail = "Waiting"
            };
            item.Subscription = this
                .device
                .Errors
                .DistinctUntilChanged() // may want to timestamp, so this isn't always wanted
                .SubOnMainThread(ex => item.Detail = ex.ToString())
                .DisposedBy(this.DeactivateWith);

            return item;
        }


        DeviceCommandItem HookTemp(string title, IObservable<Temperature> tempObs) =>
            this.Hook(title, tempObs.Select(x =>
                this.UseMetric
                    ? $"{x.DegreesCelsius} C"
                    : $"{x.DegreesFahrenheit} F"
            )
        );


        DeviceCommandItem Hook(string title, IObservable<string> func)
        {
            var commandItem = new DeviceCommandItem { Text = title };
            commandItem.PrimaryCommand = ReactiveCommand.Create(
                () =>
                {
                    if (commandItem.Subscription == null)
                    {
                        commandItem.Detail = "Waiting";
                        commandItem.Subscription = func
                            .DistinctUntilChanged() // may want to timestamp, so this isn't always wanted
                            .SubOnMainThread(
                                value => commandItem.Detail = $"[{DateTime.Now:HH:mm:ss}] {value}",
                                ex => commandItem.Detail = "[ERROR] " + ex,
                                () => commandItem.Detail += " (Completed)"
                            )
                            .DisposedBy(this.DeactivateWith);
                    }
                    else
                    {
                        commandItem.Detail = "Not Hooked";
                        commandItem.Stop();
                    }
                }
            );

            return commandItem;
        }
    }


    public class DeviceCommandItem : CommandItem
    {
        [Reactive] public bool IsDeviceConnected { get; set; }
        [Reactive] public bool IsSubscribed { get; private set; }

        IDisposable? sub;
        public IDisposable? Subscription
        {
            get => this.sub;
            set
            {
                this.sub = value;
                this.IsSubscribed = value != null;
            }
        }


        public void Stop()
        {
            this.Detail = "Not Hooked";
            this.Subscription?.Dispose();
            this.Subscription = null;
        }
    }
}
