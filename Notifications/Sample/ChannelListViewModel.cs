using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Shiny;
using Shiny.Notifications;
using Xamarin.Forms;


namespace Sample
{
    public class ChannelListViewModel : ViewModel
    {
        public ChannelListViewModel()
        {
            var notifications = ShinyHost.Resolve<INotificationManager>();
            this.Create = new Command(async () => await this.Navigation.PushAsync(new ChannelCreatePage()));

            this.LoadChannels = this.LoadingCommand(async () =>
            {
                var channels = await notifications.GetChannels();
                this.Channels = channels
                    .Select(x => new CommandItem
                    {
                        Text = x.Identifier,
                        PrimaryCommand = this.ConfirmCommand(
                            "Are you sure you wish to delete this channel?",
                            async () =>
                            {
                                await notifications.RemoveChannel(x.Identifier);
                                this.LoadChannels.Execute(null);
                            }
                        )
                    })
                    .ToList();
            });
        }


        public ICommand Create { get; }
        public ICommand LoadChannels { get; }


        IList<CommandItem> channels;
        public IList<CommandItem> Channels
        {
            get => this.channels;
            private set
            {
                this.channels = value;
                this.RaisePropertyChanged();
            }
        }


        public override void OnAppearing()
        {
            base.OnAppearing();
            this.LoadChannels.Execute(null);
        }
    }
}
