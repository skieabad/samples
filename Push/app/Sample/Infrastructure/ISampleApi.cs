using System.Threading.Tasks;
using Refit;


namespace Sample.Infrastructure
{
    public interface ISampleApi
    {
        [Get("/shiny/register/{platform}/{deviceToken}")]
        public Task Register(string platform, string deviceToken);

        [Get("/shiny/unregister/{platform}/{deviceToken}")]
        public Task UnRegister(string platform, string deviceToken);
    }
}
