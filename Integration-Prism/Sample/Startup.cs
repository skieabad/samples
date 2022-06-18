using System;
using System.Threading.Tasks;
using DryIoc.Microsoft.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Prism.DryIoc;
using Prism.Ioc;
using Shiny;
using Shiny.Push;


namespace Sample
{
    public class Startup : ShinyStartup
    {
        public override void ConfigureServices(IServiceCollection services, IPlatform platform)
        {
            // we inject our db so we can use it in our shiny background events to store them for display later
            //services.AddSingleton<SampleSqliteConnection>();
            services.UseJobs(true);
            //services.UseFirebaseMessaging<MyPushDelegate>();
        }


        public override IServiceProvider CreateServiceProvider(IServiceCollection services)
        {
            // This registers and initializes the Container with Prism ensuring
            // that both Shiny & Prism use the same container
            ContainerLocator.SetContainerExtension(() => new DryIocContainerExtension());
            var container = ContainerLocator.Container.GetContainer();
            DryIocAdapter.Populate(container, services);
            return container.GetServiceProvider();
        }
    }

    public class MyPushDelegate : Shiny.Push.IPushDelegate
    {
        public Task OnEntry(PushNotification data) => Task.CompletedTask;
        public Task OnReceived(PushNotification data) => Task.CompletedTask;
        public Task OnTokenRefreshed(string token) => Task.CompletedTask;
    }
}
