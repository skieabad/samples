namespace PushApi
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using Shiny.Extensions.Push;
    using Shiny.Extensions.Push.Infrastructure;


    public class FilePushRepository : IRepository
    {
        readonly FileInfo fileInfo;
        readonly List<PushRegistration> registrations;


        public FilePushRepository()
        {
            this.fileInfo = new FileInfo("pushdb.json");
            if (!this.fileInfo.Exists)
            {
                this.fileInfo.Create();
            }
            else
            {
                var json = File.ReadAllText(this.fileInfo.FullName);
                if (String.IsNullOrWhiteSpace(json))
                    this.registrations = JsonConvert.DeserializeObject<List<PushRegistration>>(json);
            }
            this.registrations ??= new List<PushRegistration>();
        }


        Task Flush()
        {
            lock (this.fileInfo)
                File.WriteAllText(this.fileInfo.FullName, JsonConvert.SerializeObject(this.registrations));

            return Task.CompletedTask;
        }


        public Task<IEnumerable<PushRegistration>> Get(PushFilter? filter, CancellationToken cancelToken)
            => Task.FromResult(this.GetRegs(filter));

        
        public Task Remove(PushFilter filter, CancellationToken cancelToken)
        {
            foreach (var reg in this.GetRegs(filter))
                this.registrations.Remove(reg);

            return this.Flush();
        }


        public Task RemoveBatch(PushRegistration[] pushRegistrations, CancellationToken cancelToken)
        {
            foreach (var delete in pushRegistrations)
            {
                var reg = this.registrations.FirstOrDefault(x =>
                    x.DeviceToken == delete.DeviceToken &&
                    x.Platform == x.Platform
                );
                if (reg != null)
                    this.registrations.Remove(reg);
            }
            return this.Flush();
        }


        public Task Save(PushRegistration reg, CancellationToken cancelToken)
        {
            this.registrations.Add(reg);
            return this.Flush();
        }


        IEnumerable<PushRegistration> GetRegs(PushFilter? filter)
        {
            var query = this.registrations.AsQueryable();

            if (filter != null)
            {
                if (!String.IsNullOrWhiteSpace(filter.DeviceToken))
                    query = query.Where(x => x.DeviceToken.Equals(filter.DeviceToken));

                if (!String.IsNullOrWhiteSpace(filter.UserId))
                    query = query.Where(x => x.UserId == filter.UserId);

            }
            return this.registrations.AsEnumerable();
        }
    }
}
