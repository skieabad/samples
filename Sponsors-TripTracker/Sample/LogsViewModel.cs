using ReactiveUI;
using Shiny;
using System.Collections.Generic;
using System.Windows.Input;


namespace Sample
{
    public class LogsViewModel : ViewModel
    {
        public LogsViewModel(SampleSqliteConnection conn)
        {
            this.Load = ReactiveCommand.CreateFromTask(async () =>
            {
                this.Events = await conn
                    .Events
                    .OrderByDescending(x => x.Timestamp)
                    .ToListAsync();
            });
            this.BindBusyCommand(this.Load);

            this.Clear = ReactiveCommand.CreateFromTask(async () =>
            {
                await conn.DeleteAllAsync<ShinyEvent>();
                this.Load.Execute(null);
            });
        }

        public ICommand Clear { get; }
        public ICommand Load { get; }


        List<ShinyEvent> events;
        public List<ShinyEvent> Events
        {
            get => this.events;
            set
            {
                this.events = value;
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
