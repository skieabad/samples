using System;
using Shiny.Jobs;
using Shiny;


namespace Sample
{
    public class JobLoggerTask : IShinyStartupTask
    {
        readonly IJobManager jobManager;


        public JobLoggerTask(IJobManager jobManager)
        {
            this.jobManager = jobManager;
        }


        public void Start()
        {
            //this.jobManager.JobStarted.SubscribeAsync(args => this.conn.InsertAsync(new JobLog
            //{
            //    JobIdentifier = args.Identifier,
            //    JobType = args.Type.FullName,
            //    Started = true,
            //    Timestamp = DateTime.Now,
            //}));
            //this.jobManager.JobFinished.SubscribeAsync(args => this.conn.InsertAsync(new JobLog
            //{
            //    JobIdentifier = args.Job.Identifier,
            //    JobType = args.Job.Type.FullName,
            //    Error = args.Exception?.ToString(),
            //    Parameters = this.serializer.Serialize(args.Job.Parameters),
            //    Timestamp = DateTime.Now
            //}));
        }
    }
}
