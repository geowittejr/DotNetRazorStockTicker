using AppDataModels.DomainModels;
using Microsoft.Extensions.Logging;
using AppServices.Caching;
using AppServices.Quotes;

namespace AppServices
{
    public class TickerService : ITickerService
    {
        private readonly ILogger<TickerService> _logger;
        private readonly IStockQuoteService _stockQuoteService;
        private readonly ITickerCache _tickerCache;

        public TickerService(
            ILogger<TickerService> logger, 
            IStockQuoteService stockQuoteService,
            ITickerCache tickerCache) 
        { 
            _logger = logger;
            _stockQuoteService = stockQuoteService;
            _tickerCache = tickerCache;
        }

        public Task<List<StockTicker>> GetStockTickersAsync(List<string> stockSymbols)
        {
            var tasks = stockSymbols.Select(symbol => GetStockTickerAsync(symbol));

            return Task.WhenAll(tasks).ContinueWith(t => t.Result.ToList());
        }

        private async Task<StockTicker> GetStockTickerAsync(string symbol)
        {

            var cachedTicker = _tickerCache.GetStockTicker(symbol);
            if (cachedTicker != null)
            {
                _logger.LogInformation("Cache hit for symbol: {Symbol}", symbol);
                return cachedTicker;
            }

            return await _stockQuoteService.GetStockTickerAsync(symbol);
        }
    }
}
