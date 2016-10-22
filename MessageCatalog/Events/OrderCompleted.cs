using System;
using NServiceBus;
using System.Collections.Generic;

namespace MessageCatalog.Events
{
    public class OrderCompleted : IEvent
    {
        public Guid OrderId { get; set; }
        public List<Guid> ProductIds { get; set; }
        public DateTime EstimatedArrival { get; set; }
        public Guid UserId { get; set; }
        public int OrderTotal { get; set; }
    }
}