using Serilog;
using NServiceBus;
using System.Threading;
using MessageCatalog.Events;
using System.Threading.Tasks;

namespace Billing.Handlers
{
    public class OrderAcceptedHandler : IHandleMessages<OrderAccepted>
    {
        public Task Handle(OrderAccepted message, IMessageHandlerContext context)
        {
            Thread.Sleep(5000);

            const int orderTotal = 500;

            Log.Logger.Information($"Billing complete for order: {message.OrderId}, order total: {orderTotal}");

            return context.Publish(new BillingCompleted
            {
                OrderId = message.OrderId,
                OrderTotal = orderTotal
            });
        }
    }
}
