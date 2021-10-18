using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Shiny;
using Shiny.Locations;
using Xamarin.Forms;


namespace Sample
{
    public class ListViewModel : ViewModel
    {
        readonly IGeofenceManager geofenceManager;


        public ListViewModel()
        {
            this.geofenceManager = ShinyHost.Resolve<IGeofenceManager>();

            this.Create = new Command(async () => await this.Navigation.PushAsync(new CreatePage()));
            this.DropAllFences = new Command(
                async () =>
                {
                    var confirm = await this.Confirm("Are you sure you wish to drop all geofences?");
                    if (confirm)
                    {
                        await this.geofenceManager.StopAllMonitoring();
                        await this.LoadRegions();
                    }
                }
            );
        }


        public ICommand Create { get; }
        public ICommand DropAllFences { get; }
        public IList<GeofenceRegionViewModel> Geofences { get; private set; } = new List<GeofenceRegionViewModel>();


        public override async void OnAppearing()
        {
            base.OnAppearing();
            await this.LoadRegions();
        }


        async Task LoadRegions()
        {
            var geofences = await this.geofenceManager.GetMonitorRegions();

            this.Geofences = geofences
                .Select(region => new GeofenceRegionViewModel
                {
                    Region = region,
                    Remove = new Command(async _ =>
                    {
                        var confirm = await this.Confirm("Are you sure you wish to remove geofence - " + region.Identifier);
                        if (confirm)
                        {
                            await this.geofenceManager.StopMonitoring(region.Identifier);
                            await this.LoadRegions();
                        }
                    }),
                    RequestCurrentState = new Command(async _ =>
                    {
                        GeofenceState? status = null;
                        using (var cancelSrc = new CancellationTokenSource())
                        {
                            //using (this.dialogs.Loading("Requesting State for " + region.Identifier, cancelSrc.Cancel))
                                status = await this.geofenceManager.RequestState(region, cancelSrc.Token);
                        }

                        if (status != null)
                        {
                            await Task.Delay(2000);
                            await this.Alert($"{region.Identifier} status is {status}");
                        }
                    })
                })
                .ToList();

            this.RaisePropertyChanged(nameof(this.Geofences));
        }
    }
}
