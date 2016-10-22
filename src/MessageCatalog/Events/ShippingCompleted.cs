using System;
using NServiceBus;

namespace MessageCatalog.Events
{
    public class ShippingCompleted : IEvent
    {
        public Guid OrderId { get; set; }
        public DateTime EstimatedArrival { get; set; }
    }
}