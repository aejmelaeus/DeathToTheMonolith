using System;
using NServiceBus;
using System.Collections.Generic;

namespace MessageCatalog.Events
{
    public class OrderAccepted : IEvent
    {
        public Guid OrderId { get; set; }
        public Guid UserId { get; set; }
        public List<Guid> ProductIds { get; set; }
    }
}