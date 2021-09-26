using Shiny;
using System.Windows.Input;


namespace Sample
{
    public class LogsViewModel : ViewModel
    {
        readonly SampleSqliteConnection conn;


        public LogsViewModel()
        {
            this.conn = ShinyHost.Resolve<SampleSqliteConnection>();
        }

        public ICommand Clear { get; }
    }
}
