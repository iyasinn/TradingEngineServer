using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Interface for Trading Engine Server

namespace TradingEngineServer.Core
{   
    // internal means not using anywhere
    interface ITradingEngineServer {

        // Go read up on what a task is
        Task Run(CancellationToken token);

    }
}
