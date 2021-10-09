using Shiny.Jobs;
using System.Threading;
using System.Threading.Tasks;


namespace Sample.ShinyStuff
{
    public class TestJob : IJob
    {
        public Task Run(JobInfo jobInfo, CancellationToken cancelToken) => Task.CompletedTask;
    }
}
