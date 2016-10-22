using System;
using Serilog;
using NServiceBus;
using MessageCatalog.Events;
using System.Threading.Tasks;
using MessageCatalog.Commands;

namespace Order.Handlers
{
    public class PlaceOrderHandler : IHandleMessages<PlaceOrder>
    {
        public Task Handle(PlaceOrder message, IMessageHandlerContext context)
        {
            // Fake it 'til you make it!
            Guid orderId = Guid.NewGuid();

            Log.Logger.Information($"Order with id: {orderId} accepted");

            return context.Publish(new OrderAccepted
            {
                OrderId = orderId,
                ProductIds = message.ProductIds,
                UserId = message.UserId
            });
        }
    }
}
