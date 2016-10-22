using System;
using NServiceBus;
using System.Collections.Generic;

namespace Order.Sagas
{
    public class OrderProcessData : IContainSagaData
    {
        public Guid Id { get; set; }
        public string Originator { get; set; }
        public string OriginalMessageId { get; set; }

        public Guid OrderId { get; set; }
        public List<Guid> ProductIds { get; set; }
        public Guid UserId { get; set; }
        public int OrderTotal { get; set; }
        public DateTime EstimatedArrival { get; set; }
        public bool OrderIsAccepted { get; set; }
    }
}