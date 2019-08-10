using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Persistence.Adapter;

using Serilog;
using Serilog.Formatting.Compact;
using Serilog.Sinks.ApplicationInsights.Sinks.ApplicationInsights.TelemetryConverters;

using SignUp.core;

[assembly: FunctionsStartup(typeof(SignupApi.Startup))]
namespace SignupApi
{
    public class Startup : FunctionsStartup
    {
        private FunctionSettings _settings;

        public override void Configure(IFunctionsHostBuilder builder)
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            _settings = config.Get<FunctionSettings>();

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console(new CompactJsonFormatter())
                .WriteTo.ApplicationInsights(_settings.AppInsightsInstrumentationKey, new TraceTelemetryConverter())
                .CreateLogger();

            BuildContainer(builder.Services);
            builder.Services.AddLogging(loggerBuilder => loggerBuilder.AddSerilog())
                            .BuildServiceProvider(validateScopes: true);

        }

        private void BuildContainer(IServiceCollection serviceCollection)
        {
            Bootstrapper.Register(serviceCollection);

            var adapterSettings = new PersistenceAdapterSettings(
                accountEndpoint: _settings.CosmosDbAccountEndpoint,
                accountKey: _settings.CosmosDbAccountKey);

            var persistenceAdapter = new PersistenceAdapter(adapterSettings);
            persistenceAdapter.Register(serviceCollection);
        }
    }
}