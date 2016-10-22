using System;
using NServiceBus;
using System.Collections.Generic;

namespace MessageCatalog.Commands
{
    public class PlaceOrder : ICommand
    {
        public Guid UserId { get; set; }
        public List<Guid> ProductIds { get; set; }
    }
}