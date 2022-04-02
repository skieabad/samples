using System.Threading.Tasks;
using Refit;
using Shiny;


namespace Sample.Infrastructure
{
    public class SampleApi : NotifyPropertyChanged
    {
        readonly IPlatform platform;


        public SampleApi(IPlatform platform)
        {
            this.platform = platform;
        }


        public void Reset()
        {
            this.BaseUri = this.platform.IsAndroid() ? "http://10.0.2.2:5118" : "https://192.168.1.153:7118";
        }

        string baseUri;
        public string BaseUri
        {
            get => this.baseUri;
            set => this.Set(ref this.baseUri, value);
        }

        public Task Register(string deviceToken)
            => RestService.For<ISampleApi>(this.BaseUri).Register(this.platform.IsIos() ? "apple" : "google", deviceToken);

        public Task UnRegister(string deviceToken)
            => RestService.For<ISampleApi>(this.BaseUri).UnRegister(this.platform.IsIos() ? "apple" : "google", deviceToken);
    }
}
