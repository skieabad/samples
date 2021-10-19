using Shiny.DataSync;
using System;


namespace Sample
{
    public class MyEntity : ISyncEntity
    {
        public string EntityId { get; set; } = Guid.NewGuid().ToString();
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
