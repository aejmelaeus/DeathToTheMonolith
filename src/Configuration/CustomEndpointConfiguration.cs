using Serilog;
using NServiceBus;
using NServiceBus.Logging;
using NServiceBus.Serilog;

namespace Configuration
{
    public static class CustomEndpointConfiguration
    {
        public static EndpointConfiguration Create(string endpointName)
        {
            Log.Logger = new LoggerConfiguration()
                .Enrich.WithProperty("Application", endpointName)
                .WriteTo.Seq("http://localhost:5341")
                .CreateLogger();

            LogManager.Use<SerilogFactory>();

            var endpointConfiguration = new EndpointConfiguration(endpointName);
            endpointConfiguration.UseSerialization<JsonSerializer>();
            endpointConfiguration.SendFailedMessagesTo("error");
            endpointConfiguration.AuditProcessedMessagesTo("audit");
            endpointConfiguration.UsePersistence<RavenDBPersistence>();
            endpointConfiguration.EnableInstallers();

            return endpointConfiguration;
        }
    }
}
