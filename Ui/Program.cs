using System;
using System.Collections.Generic;
using NServiceBus;
using System.Threading.Tasks;
using MessageCatalog.Commands;

namespace Ui
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            MainAsync().Wait();
        }

        private static async Task MainAsync()
        {
            var endpointConfiguration = GetEndpointConfiguration();

            var endpointInstance = await Endpoint.Start(endpointConfiguration).ConfigureAwait(false);
            try
            {
                Console.WriteLine("Bus created and configured => Sending Command!");
                var placeOrder = new PlaceOrder
                {
                    UserId = new Guid("42FB994A-2C9E-4139-B16C-315AD6AFEAB9"),
                    ProductIds = new List<Guid>
                    {
                        Guid.NewGuid(),
                        Guid.NewGuid()
                    }
                };

                await endpointInstance.Send(placeOrder).ConfigureAwait(false);
                Console.ReadKey();
            }
            finally
            {
                await endpointInstance.Stop().ConfigureAwait(false);
            }
        }

        private static EndpointConfiguration GetEndpointConfiguration()
        {
            var endpointConfiguration = new EndpointConfiguration("Ui");
            endpointConfiguration.UseSerialization<JsonSerializer>();
            endpointConfiguration.EnableInstallers();
            endpointConfiguration.UsePersistence<InMemoryPersistence>();
            endpointConfiguration.SendFailedMessagesTo("error");
            return endpointConfiguration;
        }
    }
}
