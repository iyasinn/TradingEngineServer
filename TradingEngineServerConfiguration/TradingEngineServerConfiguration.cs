using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// This is the namespace we are working with now, and we are working on our current directory
namespace TradingEngineServer.Core.Configuration
{
    class TradingEngineServerConfiguration{
        public TradingEngineServerSettings Settings { 
            get; set;
        }
    }

    // This matches our app.settings configuration file
    class TradingEngineServerSettings { 
        public int Port { get; set; }
    }
}
