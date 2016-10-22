using System;
using Serilog;
using NServiceBus;
using System.Threading;
using MessageCatalog.Events;
using System.Threading.Tasks;
using MessageCatalog.Commands;

namespace Shipping.Handlers
{
    public class DispatchOrderHandler : IHandleMessages<DispatchOrder>
    {
        public Task Handle(DispatchOrder message, IMessageHandlerContext context)
        {
            var inThreeDays = DateTime.Now.AddDays(3);

            /*
            ** Call some fancy shipping logic!
            */

            Thread.Sleep(3000);
            Log.Logger.Information($"Shipping order: {message.OrderId}. Estimated delivery: {inThreeDays}");

            return context.Publish(new ShippingCompleted
            {
                OrderId = message.OrderId,
                EstimatedArrival = inThreeDays
            });
        }
    }
}
