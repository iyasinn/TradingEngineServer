using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingEngineServer.Core
{
    // static so instantiated when it is needed
    // not deterministic when it is needed
    // because static we can use it anywhere during out program
    public static class TradingEngineServerServiceProvider{
        // What does get and set do ? 
        public static IServiceProvider ServiceProvider { get; set; }
    }
}
