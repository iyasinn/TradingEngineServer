// Top Level Statement to use files from this 
using TradingEngineServer.Core;

// Directives that will let you access dependence injection and hosting
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

// <using> lets us dispose the object
// It gets rid of unmanaged resource slike database connections and streams
// This is an IHost
using var engine = TradingEngineServerHostBuilder.BuildTradingEngineServer();

// We've registerd the tradingEngine server in the host
// We're getting an object of type IHost when we get BuildTradingEngineServer, and we use services and we put that into ServiceProvider so that way we can always access engine.Services
TradingEngineServerServiceProvider.ServiceProvider = engine.Services;

{
    // Creating a scope within which scoped services exist
    using var scope = TradingEngineServerServiceProvider.ServiceProvider.CreateScope();

    // Asynchronous programming
    // This takes cancellation tokens, which lets you cancel a long running task, which are being done by different threads 
    // What is configure await
    await engine.RunAsync().ConfigureAwait(false);
}

/*
 * Async in C# uses Tasks 
 * Async blocks will have do connection
 * 
*/