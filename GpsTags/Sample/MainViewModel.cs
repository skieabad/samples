using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Windows.Input;
using Shiny;
using Shiny.GpsTags;
using Xamarin.Forms;


namespace Sample
{
    public class MainViewModel : ViewModel
    {
        readonly ITagManager tagManager;
        CompositeDisposable? subscriptions;


        public MainViewModel()
        {
            this.tagManager = ShinyHost.Resolve<ITagManager>();
            this.Add = new Command(
                async () => await this.Navigation.PushAsync(new RegisterTagPage())
            );

            this.Load = new Command(async () =>
            {
                this.Tags = (await this.tagManager.GetTrackedTags())
                    .OrderBy(x => x.LastSeen)
                    .ToList();

                this.RaisePropertyChanged(nameof(this.Tags));
            });
        }


        public ICommand Add { get; }
        public ICommand Load { get; }
        public List<Tag> Tags { get; private set; }


        Tag? selectedTag;
        public Tag? SelectedTag
        {
            get => this.selectedTag;
            set => this.Set(ref this.selectedTag, value);
        }


        public override void OnAppearing()
        {
            base.OnAppearing();
            this.subscriptions = new CompositeDisposable();

            this.WhenAnyProperty(x => x.SelectedTag)
                .Where(x => x != null)
                .SubscribeAsync(async tag =>
                {
                    this.SelectedTag = null;
                    //var result = await this.Confirm("Do you wish to remove this tag?");
                    //if (result)
                    //{
                    //    await tagManager.StopTracking(tag.Identifier);
                    //    this.Load.Execute(null);
                    //}
                })
                .DisposedBy(this.subscriptions);

            this.tagManager
                .WhenTagStateChanged()
                .Subscribe(tag => Device.BeginInvokeOnMainThread(() =>
                {
                    this.Load.Execute(null);
                    //var txt = tag.InRange ? "in " : "out of ";
                    //await this.dialogs.Snackbar($"Tag {tag.Tag.Identifier} is {txt} range");
                }))
                .DisposedBy(this.subscriptions);
        }


        public override void OnDisappearing()
        {
            base.OnDisappearing();
            this.subscriptions?.Dispose();
        }
    }
}
