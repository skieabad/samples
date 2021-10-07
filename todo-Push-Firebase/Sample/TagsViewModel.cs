using System;
using System.Linq;
using System.Windows.Input;
using Shiny;
using Shiny.Push;

using Xamarin.Forms;

namespace Sample
{
    public class TagsViewModel : ViewModel
    {
        public TagsViewModel()
        {
            var push = (IPushTagSupport)ShinyHost.Resolve<IPushManager>();

            this.Add = new Command(async () =>
            {
                var result = await this.Prompt("Name of tag?");
                if (!result.IsEmpty())
                {
                    await this.Loading(() => push.AddTag(result));
                    this.Load.Execute(null);
                }
            });

            this.Clear = new Command(async () =>
            {
                var result = await this.Confirm("Are you sure you wish to clear all tags?");
                if (result)
                {
                    await this.Loading(() => push.ClearTags());
                    this.Load.Execute(null);
                }
            });

            this.Load = this.LoadingCommand(async () =>
            {
                //this.Tags = this.push
                //    .RegisteredTags
                //            .Select(tag => new CommandItem
                //            {
                //                Text = tag,
                //                PrimaryCommand = ReactiveCommand.CreateFromTask(async () =>
                //                {
                //                    var result = await dialogs.Confirm($"Are you sure you wish to remove tag '{tag}'?");
                //                    if (result)
                //                    {
                //                        await tags.RemoveTag(tag);
                //                        this.Load.Execute(null);
                //                    }
                //                }
                //    .Select(x => new {
                //    })
                //    .ToList();

                //this.RaisePropertyChanged(nameof(this.Tags));
            });
        }


        public ICommand Load { get; }
        public ICommand Add { get; }
        public ICommand Clear { get; }
        //public IList<CommandItem> Tags { get; private set; }

        public override void OnAppearing()
        {
            base.OnAppearing();
            this.Load.Execute(null);
        }
    }
}
