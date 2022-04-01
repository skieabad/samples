using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Refit;
using Shiny;


namespace Sample.Infrastructure
{
    public class SampleApi
    {
        readonly ISampleApi apiClient;
        readonly IPlatform platform;


        SampleApi()
        {
            this.platform = ShinyHost.Resolve<IPlatform>();
            var localUri = ShinyHost.Resolve<IConfiguration>()["LocalUri"];
            localUri ??= this.platform.IsAndroid() ? "http://10.0.2.2:5118" : "https://localhost:7118";

            this.apiClient = RestService.For<ISampleApi>(localUri);
        }


        public Task Register(string deviceToken)
            => this.apiClient.Register(this.platform.IsIos() ? "apple" : "google", deviceToken);

        public Task UnRegister(string deviceToken)
            => this.apiClient.UnRegister(this.platform.IsIos() ? "apple" : "google", deviceToken);

        public static SampleApi Current { get; } = new SampleApi();
    }
}
