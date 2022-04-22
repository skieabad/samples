using System;
using System.Net.Http;
using System.Threading.Tasks;
using Refit;
using Shiny;


namespace Sample.Infrastructure
{
    public class SampleApi : NotifyPropertyChanged
    {
        readonly IPlatform platform;
        public SampleApi(IPlatform platform) => this.platform = platform;

        string baseUri;
        public string BaseUri
        {
            get => this.baseUri!;
            set => this.Set(ref this.baseUri, value);
        }

        public void Reset()
            => this.BaseUri = this.platform.IsAndroid() ? "http://10.0.2.2:5118" : "https://192.168.1.153:7118";

        public Task Register(string deviceToken)
            => this.GetApiClient().Register(this.platform.IsIos() ? "apple" : "google", deviceToken);

        public Task UnRegister(string deviceToken)
            => this.GetApiClient().UnRegister(this.platform.IsIos() ? "apple" : "google", deviceToken);


        ISampleApi GetApiClient() => RestService.For<ISampleApi>(new HttpClient
        (
            new HttpClientHandler()
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
            }
        )
        {
            BaseAddress = new Uri(this.BaseUri),
        });
    }
}
