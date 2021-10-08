using Microsoft.Extensions.DependencyInjection;
using Shiny;


namespace Sample
{
    public class Startup : ShinyStartup
    {
        public override void ConfigureServices(IServiceCollection services, IPlatform platform)
        {
            services.UseAllSensors(); // this wires ALL sensors from shiny

            // OR you can pick out what you need - careful though, not all sensors are available on all platforms
            //services.UseAccelerometer();
            //services.UseAmbientLightSensor();
            //services.UseBarometer();
            //services.UseCompass();
            //services.UseGyroscope();
            //services.UseHumidity();
            //services.UseMagnetometer();
            //services.UsePedometer();
            //services.UseProximitySensor();
            //services.UseTemperature();
        }
    }
}
