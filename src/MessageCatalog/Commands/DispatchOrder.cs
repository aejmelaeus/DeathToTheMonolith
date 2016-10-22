using System;
using NServiceBus;
using System.Collections.Generic;

namespace MessageCatalog.Commands
{
    public class DispatchOrder : ICommand
    {
        public Guid OrderId { get; set; }
        public Guid UserId { get; set; }
        public List<Guid> ProductIds { get; set; }
    }
}
