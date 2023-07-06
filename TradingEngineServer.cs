﻿using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
// This is our configuration file namespace
using TradingEngineServer.Core.Configuration;

namespace TradingEngineServer.Core{

    // Maknig my Trading Engine a background service, and also an ITradingEngineServer
    // Now I have access to many things
    // Sealed means that no derived class can override functions in this class
    sealed class TradingEngineServer : BackgroundService, ITradingEngineServer {

        // Member variables
        // Storing the logger
        private readonly ILogger<TradingEngineServer> _logger;
        // Storing the configuration
        private readonly TradingEngineServerConfiguration _tradingEngineServerConfig;

        // Constructor
        public TradingEngineServer(ILogger<TradingEngineServer> logger, 
                                   IOptions<TradingEngineServerConfiguration> config){

            // ?? is the null coalescing operator
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        
            // This is injecting the configuration server
            // Value retursn the value of the IOptions 
            _tradingEngineServerConfig = config.Value ?? throw new ArgumentNullException(nameof(config));
        }

        // Forwarding the function for execute async to run it
        // This way, we now have a public way to call ExecuteAsync!
        // We don't need this, because microsoft library for hosting, so Microsfot.Extensions.Hosting will call executeAsync for us, so we implemented this all ourself. 
        public Task Run(CancellationToken token) => ExecuteAsync(token);

        // This is our executed Async
        // Backrournd Service has this function. We need to make this avialable publicly. 
        // We will do this through an interaface because this is private in the BackgroundSerivce Parent Class
        // We can do this through a Task
        // See above
        protected override Task ExecuteAsync(CancellationToken stoppingToken){

            // This toekn is what is passing through to CancellationToken
        //    CancellationTokenSource cancel = new //CancellationTokenSource();
       //     cancel.Cancel();
       //     cancel.Dispose();
       //     cancel.Token;

            _logger.LogInformation($"Starting {nameof(TradingEngineServer)}");

            while (!stoppingToken.IsCancellationRequested){
                //  _logger.LogInformation("running");

            }

            _logger.LogInformation("hfsdfsd\n");
            _logger.LogInformation($"{stoppingToken.IsCancellationRequested}");
            _logger.LogInformation($"Stopping {nameof(TradingEngineServer)} boiiii");

            return Task.CompletedTask;

        }
    }
}
