using Shiny.Locations;
using System.Windows.Input;
using Xamarin.Forms;


namespace Sample.Create
{
    public class LocationViewModel : SampleViewModel
    {
        public LocationViewModel()
        {
            this.Use = new Command(() =>
            {

            });
        }

        public ICommand Use { get; }


        Position? position;
        public Position? SelectedPosition
        {
            get => this.position;
            set => this.Set(ref this.position, value);
        }
    }
}
