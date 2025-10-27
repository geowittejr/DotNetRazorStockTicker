using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using AppDataModels.Config;
using AppDataModels.DomainModels;

namespace AppServices.Caching
{
    public class TickerCache : ITickerCache
    {
        private readonly StockQuotesApiOptions _options;
        private readonly IMemoryCache _memoryCache;

        public TickerCache(IOptions<StockQuotesApiOptions> options, IMemoryCache memoryCache) 
        { 
            _options = options.Value;
            _memoryCache = memoryCache;
        }    

        public void CacheStockTicker(StockTicker stockTicker)
        {
            _memoryCache.Set(
                stockTicker.Symbol, 
                stockTicker, 
                TimeSpan.FromSeconds(_options.CacheTtlSeconds));
        }

        public StockTicker? GetStockTicker(string stockSymbol)
        {
            return _memoryCache.Get<StockTicker?>(stockSymbol);
        }

        public void RemoveStockTicker(string stockSymbol)
        {
            _memoryCache.Remove(stockSymbol);
        }
    }
}
