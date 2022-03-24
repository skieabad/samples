using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Shiny;
using Shiny.Notifications;
using Xamarin.Forms;


namespace Sample
{
    public class PendingViewModel : SampleViewModel
    {
        public PendingViewModel()
        {
            var notifications = ShinyHost.Resolve<INotificationManager>();

            this.Create = this.NavigateCommand<Create.CreatePage>();

            this.Load = this.LoadingCommand(async () =>
            {
                var pending = await notifications.GetPending();
                this.PendingList = pending
                    .Select(x => new CommandItem
                    {
                        Text = $"[{x.Id}] {x.Title}",
                        //Detail = $"[{x.ScheduleDate.Value}] {x.Message}",
                        PrimaryCommand = new Command(async () =>
                        {
                            await notifications.Cancel(x.Id);
                            this.Load.Execute(null);
                        })
                    })
                    .ToList();
            });

            this.Clear = this.ConfirmCommand(
                "Clear All Pending Notifications?",
                async () =>
                {
                    await notifications.Clear();
                    this.Load.Execute(null);
                }
            );
        }


        public ICommand Load { get; }
        public ICommand Create { get; }
        public ICommand Clear { get; }


        IList<CommandItem> pending;
        public IList<CommandItem> PendingList
        {
            get => this.pending;
            private set
            {
                this.pending = value;
                this.RaisePropertyChanged();
            }
        }


        public override void OnAppearing()
        {
            base.OnAppearing();
            this.Load.Execute(null);
        }
    }
}
