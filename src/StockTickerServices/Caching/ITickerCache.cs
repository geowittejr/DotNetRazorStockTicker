using StockTickerData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTickerServices.Caching
{
    public interface ITickerCache
    {
        Task CacheStockTickerAsync(StockTicker stockTicker, int expirationSeconds = 60);
        Task<StockTicker?> GetStockTickerAsync(string stockSymbol);
        Task RemoveStockTickerAsync(string stockSymbol);
    }
}
