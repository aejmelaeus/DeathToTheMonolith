using System;
using NServiceBus;

namespace MessageCatalog.Events
{
    public class BillingCompleted : IEvent
    {
        public Guid OrderId { get; set; }
        public int OrderTotal { get; set; }
    }
}
