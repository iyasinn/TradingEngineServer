using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TradingEngineServer.Core.Configuration;
using TradingEngineServer.Logging;
using TradingEngineServer.Logging.LoggingConfiguration;

namespace TradingEngineServer.Core{

    /*
     * We need a static method that returns a host for us
     * We need the TradingEngineServer as the hosted service
     * We will be executing the Async functino as well 
    */
    public sealed class TradingEngineServerHostBuilder{

        // => is for lambda if we're not fowarding, so the second =>
        public static IHost BuildTradingEngineServer()
            => Host.CreateDefaultBuilder().ConfigureServices((context, services) => {

                // Start with configuration
                // We want the ability to have config options
                services.AddOptions();
                // Add a potential class, which is the TradingEngineServerConfiguration
               
                services.Configure<TradingEngineServerConfiguration>(context.Configuration.GetSection(nameof(TradingEngineServerConfiguration)));
                services.Configure<LoggerConfiguration>(context.Configuration.GetSection(nameof(LoggerConfiguration)));

                // Add singleton objects, whatever these are
                // You can only have one AddSingleton
               
               services.AddSingleton<ITradingEngineServer, TradingEngineServer>();

                // Now we have associate ITextLogger with TextLogger
                // SO the same class will be called every single time every time we inject an ITextLogger
                services.AddSingleton<ITextLogger, TextLogger>();

                // Add our hosted service 
                // The type that microsofots library will inherit
                services.AddHostedService<TradingEngineServer>();

            
            }).Build();

            // .Build() let's us return an instance of IHost
    }
}
