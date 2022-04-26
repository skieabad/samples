using Shiny;
using Shiny.BluetoothLE.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Timers;
using System.Windows.Input;
using Xamarin.Forms;


namespace Sample
{
    public class MainViewModel : SampleViewModel
    {
        const string ServiceUuid = "ff00b0eb-07b2-4937-82e3-c3d8b59f1690";
        const string CharacteristicUuid1 = "ff00b0eb-07b2-4937-82e3-c3d8b59f1691";
        const string CharacteristicUuid2 = "ff00b0eb-07b2-4937-82e3-c3d8b59f1692";
        IDisposable? disposable;


        public MainViewModel()
        {
            var manager = ShinyHost.Resolve<IBleHostingManager>();

            this.ClearLogs = new Command(() =>
            {
                this.Logs?.Clear();
                this.RaisePropertyChanged(nameof(this.Logs));
            });

            this.Stop = new Command(() => disposable?.Dispose());

            this.WriteNotifyFlow = new Command(async () =>
            {
                disposable?.Dispose();
                this.WriteEvent("Starting write/notify flow");

                await manager.AddService(ServiceUuid, false, sb =>
                {
                    var notifier = sb.AddCharacteristic(CharacteristicUuid1, cb => cb.SetNotification(sub =>
                    {
                        var subs = sub.Characteristic.SubscribedCentrals.Count;
                        var msg = "Device ";
                        if (sub.IsSubscribing) msg += "Un";
                        msg += "Subscribing - Subscriptions Now: " + sub.Characteristic.SubscribedCentrals.Count;
                        this.WriteEvent(msg);
                    }));

                    sb.AddWriteCharacteristic(CharacteristicUuid2, async (peripheral, data) =>
                    {
                        var send = BitConverter.GetBytes(data[0] + 1);
                        await notifier.Notify(send, peripheral);
                    });
                });
                disposable = Disposable.Create(() =>
                {
                    this.WriteEvent("Stopping write/notify flow");
                    manager.RemoveService(ServiceUuid);
                });
            });

            this.ReadWriteFlow = new Command(async () =>
            {
                this.WriteEvent("Starting read/write flow");
                disposable?.Dispose();

                await manager.AddService(ServiceUuid, false, sb =>
                {
                    var read = -1;

                    sb.AddReadCharacteristic(CharacteristicUuid1, _ =>
                    {
                        this.WriteEvent("Read received - sending: " + read);
                        return BitConverter.GetBytes(read);
                    });

                    sb.AddWriteCharacteristic(CharacteristicUuid2, (peripheral, data) =>
                    {
                        read = data[0] + 1;
                        this.WriteEvent("Write received - next read: " + read);
                    });
                });
                disposable = Disposable.Create(() =>
                {
                    this.WriteEvent("Stopping read/write flow");
                    manager.RemoveService(ServiceUuid);
                });
            });

            this.ContinuousNotify = new Command(async () =>
            {
                this.WriteEvent("Starting continuous notify");
                disposable?.Dispose();
                var timer = new Timer();

                await manager.AddService(ServiceUuid, false, sb =>
                {
                    var notifier = sb.AddCharacteristic(CharacteristicUuid1, cb => cb.SetNotification(sub =>
                    {
                        if (sub.IsSubscribing)
                        {
                            this.WriteEvent("New Subscribed Received");
                            if (!timer.Enabled)
                            timer.Start();
                        }
                        else
                        {
                            var subs = sub.Characteristic.SubscribedCentrals.Count;
                            this.WriteEvent("Device unsubscribed - total subscribers: " + subs);
                            if (subs == 0)
                                timer.Stop();
                        }
                    }));
                    timer.Elapsed += async (sender, args) =>
                    {
                        timer.Stop();
                        try
                        {
                            var bytes = BitConverter.GetBytes(DateTimeOffset.UtcNow.Ticks);
                            await notifier.Notify(bytes);
                            this.WriteEvent("Notification sent");
                        }
                        catch (Exception ex)
                        {
                            this.WriteEvent("Error writing notification - " + ex);
                        }
                        timer.Start();
                    };
                });

                disposable = Disposable.Create(() =>
                {
                    this.WriteEvent("Stopping continuous notify");
                    timer.Dispose();
                    manager.RemoveService(ServiceUuid);
                });
            });

            this.DeviceInfo = new Command(() =>
            {
                disposable?.Dispose();
                manager.AddDeviceInfoService(new Shiny.BluetoothLE.DeviceInfo
                {
                    ManufacturerName = "Shiny",
                    HardwareRevision = "0.3",
                    FirmwareRevision = "0.2",
                    SoftwareRevision = "0.1",
                    ModelNumber = "hello_world",
                    SerialNumber = "XXX"
                });

                disposable = Disposable.Create(() => manager.RemoveService("180A"));
            });
        }


        public IList<LogItem>? Logs { get; private set; }
        public ICommand WriteNotifyFlow { get; }
        public ICommand ReadWriteFlow { get; }
        public ICommand ContinuousNotify { get; }
        public ICommand DeviceInfo { get; }
        public ICommand ClearLogs { get; }
        public ICommand Stop { get; }


        void WriteEvent(string e) => Device.BeginInvokeOnMainThread(() =>
        {
            var logs = this.Logs?.ToList() ?? new List<LogItem>();
            logs.Insert(0, new LogItem(e));
            this.Logs = logs;
            this.RaisePropertyChanged(nameof(this.Logs));
        });
    }


    public struct LogItem
    {
        public LogItem(string description)
        {
            this.Description = description ?? throw new ArgumentNullException(nameof(description));
            this.Timestamp = DateTime.Now;
        }

        public string Description { get; }
        public DateTime Timestamp { get; }
    }
}
