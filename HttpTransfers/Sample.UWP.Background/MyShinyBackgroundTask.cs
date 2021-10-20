using System;
using Windows.ApplicationModel.Background;
using Shiny;


namespace Sample.UWP.Background
{
    public sealed class MyShinyBackgroundTask : IBackgroundTask
    {
        public void Run(IBackgroundTaskInstance taskInstance)
            => this.ShinyRunBackgroundTask(taskInstance, new Startup());
    }
}
