using System;
using Shiny;


namespace Sample.ShinyStuff
{
    public class TestStartupTask : IShinyStartupTask
    {
        public void Start()
        {
            // startup tasks are a great way of running logic when the Shiny container is finished building
            // don't block here as your app will take longer to start.  You also shouldn't expect anything async to be completed here before your app starts
            // lastly, also auto-registered ;)
        }
    }
}
