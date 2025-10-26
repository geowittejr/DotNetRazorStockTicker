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
            throw new NotImplementedException();
        }
    }
}
