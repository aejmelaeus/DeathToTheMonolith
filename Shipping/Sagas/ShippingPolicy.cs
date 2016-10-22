using NServiceBus;
using MessageCatalog.Events;
using System.Threading.Tasks;
using MessageCatalog.Commands;


namespace Shipping.Sagas
{
    public class ShippingPolicy : Saga<ShippingPolicyData>,
        IAmStartedByMessages<OrderAccepted>, IAmStartedByMessages<BillingCompleted>
    {
        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<ShippingPolicyData> mapr)
        {
            mapr.ConfigureMapping<OrderAccepted>(e => e.OrderId)
                .ToSaga(s => s.OrderId);

            mapr.ConfigureMapping<BillingCompleted>(e => e.OrderId)
                .ToSaga(s => s.OrderId);
        }

        public Task Handle(OrderAccepted message, IMessageHandlerContext context)
        {
            Data.OrderIsAccepted = true;
            Data.UserId = message.UserId;
            Data.ProductIds = message.ProductIds;

            return DispatchOrderIfPossible(context);
        }

        public Task Handle(BillingCompleted message, IMessageHandlerContext context)
        {
            Data.OrderIsBilled = true;

            return DispatchOrderIfPossible(context);
        }

        private Task DispatchOrderIfPossible(IMessageHandlerContext context)
        {
            if (Data.OrderIsAccepted && Data.OrderIsBilled)
            {
                return context.Send(new DispatchOrder
                {
                    OrderId = Data.OrderId,
                    ProductIds = Data.ProductIds,
                    UserId = Data.UserId
                });
            }

            return Task.FromResult(0);
        }
    }
}
