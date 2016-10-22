using System;
using Serilog;
using System.Text;
using NServiceBus;
using System.Threading;
using MessageCatalog.Events;
using System.Threading.Tasks;

namespace Order.Sagas
{
    public class OrderProcess : Saga<OrderProcessData>, IAmStartedByMessages<OrderAccepted>,
        IAmStartedByMessages<BillingCompleted>, IAmStartedByMessages<ShippingCompleted>
    {
        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<OrderProcessData> mapr)
        {
            mapr.ConfigureMapping<OrderAccepted>(o => o.OrderId)
                .ToSaga(s => s.OrderId);

            mapr.ConfigureMapping<ShippingCompleted>(e => e.OrderId)
                .ToSaga(s => s.OrderId);

            mapr.ConfigureMapping<BillingCompleted>(e => e.OrderId)
                .ToSaga(s => s.OrderId);
        }

        public Task Handle(OrderAccepted message, IMessageHandlerContext context)
        {
            Data.OrderIsAccepted = true;
            Data.UserId = message.UserId;
            Data.ProductIds = message.ProductIds;

            return CompleteOrderIfPossible(context);
        }

        public Task Handle(BillingCompleted message, IMessageHandlerContext context)
        {
            Data.OrderTotal = message.OrderTotal;

            return CompleteOrderIfPossible(context);
        }

        public Task Handle(ShippingCompleted message, IMessageHandlerContext context)
        {
            Data.EstimatedArrival = message.EstimatedArrival;

            return CompleteOrderIfPossible(context);
        }

        private Task CompleteOrderIfPossible(IMessageHandlerContext context)
        {
            if (OrderIsAccepted && OrderIsShipped && OrderIsBilled)
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.AppendLine($"Order {Data.OrderId} completed for user {Data.UserId}.");
                stringBuilder.AppendLine($"Estimated arrival: {Data.EstimatedArrival}.");
                stringBuilder.AppendLine($"Order total: {Data.OrderTotal}");
                Log.Logger.Information(stringBuilder.ToString());

                Thread.Sleep(3000);

                return context.Publish(new OrderCompleted
                {
                    EstimatedArrival = Data.EstimatedArrival,
                    OrderId = Data.OrderId,
                    ProductIds = Data.ProductIds,
                    OrderTotal = Data.OrderTotal,
                    UserId = Data.UserId
                });
            }

            return Task.FromResult(0);
        }

        private bool OrderIsShipped => Data.EstimatedArrival != default(DateTime);
        private bool OrderIsBilled => Data.OrderTotal > 0;
        private bool OrderIsAccepted => Data.OrderIsAccepted;
    }
}