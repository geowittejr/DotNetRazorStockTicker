using StockTickerData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTickerServices.Caching
{
    public class TickerCache : ITickerCache
    {
        public Task CacheStockTickerAsync(StockTicker stockTicker, int expirationSeconds = 60)
        {
            throw new NotImplementedException();
        }

        public Task<StockTicker?> GetStockTickerAsync(string stockSymbol)
        {
            throw new NotImplementedException();
        }

        public Task RemoveStockTickerAsync(string stockSymbol)
        {
            throw new NotImplementedException();
        }
    }
}
