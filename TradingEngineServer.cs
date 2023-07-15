using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.Concurrent;
using System.Threading.Tasks.Dataflow;
// This is our configuration file namespace
using TradingEngineServer.Core.Configuration;
using TradingEngineServer.Logging;
using TradingEngineServer.Logging.LoggingConfiguration;
namespace TradingEngineServer.Core
{

    // Maknig my Trading Engine a background service, and also an ITradingEngineServer
    // Now I have access to many things
    // Sealed means that no derived class can override functions in this class
    sealed class TradingEngineServer : BackgroundService, ITradingEngineServer
    {

        // Member variables
        // Storing the logger
        //private readonly ILogger<TradingEngineServer> _logger;
        private readonly ITextLogger _logger;
        // Storing the configuration
        private readonly TradingEngineServerConfiguration _tradingEngineServerConfig;

        // Constructor
        public TradingEngineServer(ITextLogger textLogger,
                                   IOptions<TradingEngineServerConfiguration> config)
        {

            // ?? is the null coalescing operator
            _logger = (TextLogger?)(textLogger ?? throw new ArgumentNullException(nameof(textLogger)));

            // This is injecting the configuration server
            // Value retursn the value of the IOptions 
            _tradingEngineServerConfig = config.Value ?? throw new ArgumentNullException(nameof(config));
        }

        // Forwarding the function for execute async to run it
        // This way, we now have a public way to call ExecuteAsync!
        // We don't need this, because microsoft library for hosting, so Microsfot.Extensions.Hosting will call executeAsync for us, so we implemented this all ourself. 
        public Task Run(CancellationToken token) => ExecuteAsync(token);


        private async Task ProcessOrders(CancellationToken stoppingToken){
            while (!stoppingToken.IsCancellationRequested)
            {

                var order = orderQueue.Take(stoppingToken);

                await ProcessOrderAsync(order);

                /*
                //if (orderQueue.TryDequeue(out var order))
                if (orderQueue.)
                {
                    // Process the order asynchronously
                    await ProcessOrderAsync(order);
                    //await Console.Out.WriteLineAsync("Starting to write");

                    // Log the processed order
                   // _logger.Information(nameof(TradingEngineServer), $"Order processed: {order.Id}");

                }
                else
                {
                    // No orders in the queue, wait for a short period
                    // Might cause issues
                    //await Task.Delay(TimeSpan.FromMilliseconds(100), stoppingToken);
                    await Task.Delay(100);
                }
                */
            }
        }

        private async Task ProcessOrderAsync(Order order)
        {
            // Add your logic here to handle the order asynchronously
            await Task.Delay(5000);

            // Log the processed order
            _logger.Information(nameof(TradingEngineServer), $"Finished order processed: {order.Id}");
        }

        /*
            // This is our executed Async
            // Backrournd Service has this function. We need to make this avialable publicly. 
            // We will do this through an interaface because this is private in the BackgroundSerivce Parent Class
            // We can do this through a Task
            // See above
        */
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {


            _logger.Information(nameof(TradingEngineServer), "Starting trading engine");

            var processingTask = Task.Run(() => ProcessOrders(stoppingToken));


            while (!stoppingToken.IsCancellationRequested)
            {

                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                    char keyChar = keyInfo.KeyChar;
                    int key = keyChar - '0';
                    if (key >= 0 && key <= 9)
                    {
                        Console.WriteLine("Adding order of size: " + key);
                        Console.Write("Queue: ");
                        var order = new Order();
                        order.Id = key;
                        //orderQueue.Enqueue(order);
                        orderQueue.Add(order);
                        foreach (var item in orderQueue)
                        {
                            Console.Write(item.Id + " ");
                        }
                        Console.WriteLine();
                    }
                }
           
                /*
                // string input = await Console.In.ReadLineAsync();
                if (input == "5")
                {
                    var order = new Order();
                    order.Id = 5;
                    orderQueue.Enqueue(order);
                }
                */
            }

            await processingTask;

            _logger.Information(nameof(TradingEngineServer), "Stopping trading engine");
        }

        class Order
        {
            public int Id { get; set; }
    
        }
       // private readonly ConcurrentQueue<Order> orderQueue = new ConcurrentQueue<Order>();
        private readonly BlockingCollection<Order> orderQueue = new BlockingCollection<Order>();
        private readonly CancellationTokenSource tokenSource = new CancellationTokenSource();
    }
}
