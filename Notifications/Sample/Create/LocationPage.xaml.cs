using Xamarin.Forms;
using Xamarin.Forms.Maps;


namespace Sample.Create
{
    public partial class LocationPage : ContentPage
    {
        public LocationPage()
        {
            this.InitializeComponent();
        }


        private void OnMapClicked(object sender, MapClickedEventArgs e)
        {
            ((LocationViewModel)this.BindingContext).SelectedPosition = new Shiny.Locations.Position(e.Position.Latitude, e.Position.Longitude);
        }
    }
}