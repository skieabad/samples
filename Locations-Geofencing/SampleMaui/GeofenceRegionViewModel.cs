using System.Windows.Input;
using Shiny.Locations;

namespace Sample;


public record GeofenceRegionViewModel(
    GeofenceRegion Region,
    ICommand Remove,
    ICommand RequestCurrentState
)
{
    public string Text => this.Region.Identifier;
    public string Detail => $"{this.Region.Radius.TotalMeters}m from {this.Region.Center.Latitude}/{this.Region.Center.Longitude}";
}
