using System;
using NServiceBus;
using Configuration;
using NServiceBus.Logging;
using System.ServiceProcess;
using System.Threading.Tasks;

namespace Order
{
    internal class ProgramService : ServiceBase
    {
        private IEndpointInstance _endpoint;

        private static readonly ILog Logger = LogManager.GetLogger<ProgramService>();

        private static void Main()
        {
            using (var service = new ProgramService())
            {
                // to run interactive from a console or as a windows service
                if (Environment.UserInteractive)
                {
                    Console.CancelKeyPress += (sender, e) =>
                    {
                        service.OnStop();
                    };
                    service.OnStart(null);
                    Console.WriteLine("\r\nPress enter key to stop program\r\n");
                    Console.Read();
                    service.OnStop();
                    return;
                }
                Run(service);
            }
        }

        protected override void OnStart(string[] args)
        {
            AsyncOnStart().GetAwaiter().GetResult();
        }

        async Task AsyncOnStart()
        {
            try
            {
                var endpointConfiguration = CustomEndpointConfiguration.Create("Order");
                endpointConfiguration.DefineCriticalErrorAction(OnCriticalError);

                _endpoint = await Endpoint.Start(endpointConfiguration).ConfigureAwait(false);

                PerformStartupOperations();
            }
            catch (Exception exception)
            {
                Logger.Fatal("Failed to start", exception);
                Environment.FailFast("Failed to start", exception);
            }
        }

        void PerformStartupOperations()
        {
            //TODO: perform any startup operations
        }

        Task OnCriticalError(ICriticalErrorContext context)
        {
            //TODO: Decide if shutting down the process is the best response to a critical error
            //https://docs.particular.net/nservicebus/hosting/critical-errors
            var fatalMessage = $"The following critical error was encountered:\n{context.Error}\nProcess is shutting down.";
            Logger.Fatal(fatalMessage, context.Exception);
            Environment.FailFast(fatalMessage, context.Exception);
            return Task.FromResult(0);
        }

        protected override void OnStop()
        {
            _endpoint?.Stop().GetAwaiter().GetResult();
        }
    }
}