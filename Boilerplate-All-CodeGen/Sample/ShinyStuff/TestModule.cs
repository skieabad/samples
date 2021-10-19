using System;
using Microsoft.Extensions.DependencyInjection;
using Shiny;


namespace Sample.ShinyStuff
{
    public class TestModule : ShinyModule
    {
        public override void Register(IServiceCollection services)
        {
            // modules are great ways of clumbing together groups of dependencies you may have - note, these are also auto-wired for you
        }
    }
}
