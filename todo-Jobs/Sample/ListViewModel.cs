using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Shiny.Jobs;
using Shiny;
using Xamarin.Forms;

namespace Sample
{
    public class ListViewModel : ViewModel
    {
        readonly IJobManager jobManager;


        public ListViewModel()
        {
            this.jobManager = ShinyHost.Resolve<IJobManager>();

            this.Create = new Command(async () => await this.Navigation.PushAsync(new CreatePage());

            this.LoadJobs = new Command(async () =>
            {
                var jobs = await jobManager.GetJobs();

                var blah = jobs
                    .Select(x => new
                    {
                        //Text = x.Identifier,
                        //Detail = x.LastRunUtc?.ToLocalTime().ToString("G") ?? "Never Run",
                        //PrimaryCommand = ReactiveCommand.CreateFromTask(() => jobManager.Run(x.Identifier)),
                        //SecondaryCommand = ReactiveCommand.CreateFromTask(async () =>
                        //{
                        //    await jobManager.Cancel(x.Identifier);
                        //    this.LoadJobs.Execute(null);
                        //})
                    })
                    .ToList();
            });
            //this.BindBusyCommand(this.LoadJobs);

            //this.RunAllJobs = ReactiveCommand.CreateFromTask(async () =>
            //{
            //    if (!await this.AssertJobs())
            //        return;

            //    if (this.jobManager.IsRunning)
            //    {
            //        await dialogs.Alert("Job Manager is already running");
            //    }
            //    else
            //    {
            //        await this.jobManager.RunAll();
            //        await dialogs.Snackbar("Job Batch Started");
            //    }
            //});

            //this.CancelAllJobs = ReactiveCommand.CreateFromTask(async _ =>
            //{
            //    if (!await this.AssertJobs())
            //        return;

            //    var confirm = await dialogs.Confirm("Are you sure you wish to cancel all jobs?");
            //    if (confirm)
            //    {
            //        await this.jobManager.CancelAll();
            //        this.LoadJobs.Execute(null);
            //    }
            //});
        }


        public ICommand LoadJobs { get; }
        public ICommand CancelAllJobs { get; }
        public ICommand RunAllJobs { get; }
        public ICommand Create { get; }


        public List<ShinyEvent> Jobs { get; private set; }


        public override void OnAppearing()
        {
            base.OnAppearing();
            this.LoadJobs.Execute(null);

            //this.jobManager
            //    .JobStarted
            //    .Subscribe(x =>
            //    {
            //        this.dialogs.Snackbar($"Job {x.Identifier} Started");
            //        this.LoadJobs.Execute(null);
            //    })
            //    .DisposedBy(this.DeactivateWith);

            //this.jobManager
            //    .JobFinished
            //    .Subscribe(x =>
            //    {
            //        this.dialogs.Snackbar($"Job {x.Job?.Identifier} Finished");
            //        this.LoadJobs.Execute(null);
            //    })
            //    .DisposedBy(this.DeactivateWith);
        }


        async Task<bool> AssertJobs()
        {
            var jobs = await this.jobManager.GetJobs();
            if (!jobs.Any())
            {
                //await this.dialogs.Alert("There are no jobs");
                return false;
            }

            return true;
        }
    }
}
