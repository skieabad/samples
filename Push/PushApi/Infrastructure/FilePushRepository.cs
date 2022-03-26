namespace PushApi;

using Shiny.Extensions.Push;
using Shiny.Extensions.Push.Infrastructure;


public class FilePushRepository : IRepository
{

    public Task<IEnumerable<PushRegistration>> Get(PushFilter? filter, CancellationToken cancelToken)
    {
        throw new NotImplementedException();
    }


    public Task Remove(PushFilter filter, CancellationToken cancelToken)
    {
        throw new NotImplementedException();
    }


    public Task RemoveBatch(PushRegistration[] pushRegistrations, CancellationToken cancelToken)
    {
        throw new NotImplementedException();
    }


    public Task Save(PushRegistration reg, CancellationToken cancelToken)
    {
        throw new NotImplementedException();
    }
}
