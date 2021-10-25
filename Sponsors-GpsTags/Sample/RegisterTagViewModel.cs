using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Shiny;
using Shiny.Beacons;
using Shiny.GpsTags;


namespace Sample
{
    public class RegisterTagViewModel : SampleViewModel
    {
        const string EstimoteUuid = "B9407F30-F5F8-466E-AFF9-25556B57FE6D";

        IDisposable? scanSub;


        public RegisterTagViewModel()
        {
            var tagManager = ShinyHost.Resolve<ITagManager>();
            //this.WhenAnyValue(x => x.BeaconUuid)
            //    .Where(x => Guid.TryParse(x, out var _))
            //    .Select(x => Guid.Parse(x))
            //    .SubscribeAsync(async uuid =>
            //    {
            //        var result = await tagManager.RequestAccess();
            //        if (result.FullResult != AccessState.Available || result.FullResult != AccessState.Restricted)
            //        {
            //            await dialogs.Snackbar("GPS Tag permissions insufficient");
            //            return;
            //        }
            //        var tags = await tagManager.GetTrackedTags();
            //        this.Beacons.Clear();

            //        this.IsBusy = true;
            //        this.scanSub = tagManager
            //            .FindBeacons(Guid.Parse(this.BeaconUuid))
            //            .Where(beacon => !tags.Any(tag =>
            //                beacon.Uuid.Equals(tag.BeaconUuid) &&
            //                beacon.Major.Equals(tag.BeaconMajor) &&
            //                beacon.Minor.Equals(tag.BeaconMinor)
            //            ))
            //            .Synchronize()
            //            .SubOnMainThread(beacon => this.Beacons.Add(beacon));
            //    })
            //    .DisposedBy(this.DeactivateWith);

            //this.WhenAnyValue(x => x.BeaconUuid)
            //    .Where(x => !Guid.TryParse(x, out var _))
            //    .Subscribe(uuid =>
            //    {
            //        this.IsBusy = false;
            //        this.scanSub?.Dispose();
            //    })
            //    .DisposedBy(this.DeactivateWith);

            //this.Create = ReactiveCommand.CreateFromTask(
            //    async () =>
            //    {
            //        var confirm = await dialogs.Confirm("Do you want to be alerted for this tag?");
            //        await tagManager.StartTracking(
            //            this.Identifier!.Trim(),
            //            this.SelectedBeacon!,
            //            confirm
            //                ? AlertState.None
            //                : AlertState.Everytime
            //        );
            //        await navigator.GoBack();
            //    },
            //    this.WhenAny(
            //        x => x.SelectedBeacon,
            //        x => x.Identifier,
            //        (beacon, id) =>
            //            beacon.GetValue() != null &&
            //            !id.GetValue().IsEmpty()
            //    )
            //);
        }


        public ICommand Create { get; }
        public string BeaconUuid { get; set; } = EstimoteUuid;
        public string Identifier { get; set; }
        public bool IsAlert { get; set; }
        public Beacon? SelectedBeacon { get; set; }
        public ObservableCollection<Beacon> Beacons { get; private set; } = new ObservableCollection<Beacon>();
    }
}