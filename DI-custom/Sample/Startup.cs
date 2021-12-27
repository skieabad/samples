using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Shiny;


namespace Sample
{
    public class Startup : ShinyStartup
    {
        public override void ConfigureServices(IServiceCollection services, IPlatform platform)
        {
            // we inject our db so we can use it in our shiny background events to store them for display later
            services.AddSingleton<SampleSqliteConnection>();
        }


        public override IServiceProvider? CreateServiceProvider(IServiceCollection services)
        {
            var factory = new AutofacServiceProviderFactory();
            var builder = new ContainerBuilder();
            builder.Populate(services);

            return factory.CreateServiceProvider(builder);
        }
    }
}
