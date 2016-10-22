using System;
using NServiceBus;
using System.Collections.Generic;

namespace Shipping.Sagas
{
    public class ShippingPolicyData : IContainSagaData
    {
        public Guid Id { get; set; }
        public string Originator { get; set; }
        public string OriginalMessageId { get; set; }

        public Guid OrderId { get; set; }
        public List<Guid> ProductIds { get; set; }
        public Guid UserId { get; set; }
        public bool OrderIsAccepted { get; set; }
        public bool OrderIsBilled { get; set; }
    }
}