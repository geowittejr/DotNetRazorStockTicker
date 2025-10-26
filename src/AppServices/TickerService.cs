using AppDataModels.DomainModels;
using Microsoft.Extensions.Logging;
using AppServices.Caching;
using AppServices.Quotes;
using AppDataModels.Utility;

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

        public async Task<Result<List<StockTicker>>> GetStockTickersAsync(List<string> stockSymbols)
        {
            var tasks = stockSymbols.Select(symbol => GetStockTickerAsync(symbol));

            var res = Task.WhenAll(tasks).ContinueWith(t => t.Result.ToList());

            return Result<List<StockTicker>>.Failure(new ResultError("500", "failed"));
        }

        private async Task<Result<StockTicker>> GetStockTickerAsync(string symbol)
        {
            var cachedTicker = _tickerCache.GetStockTicker(symbol);
            if (cachedTicker != null)
            {
                _logger.LogInformation("Cache hit for symbol: {Symbol}", symbol);
                return Result<StockTicker>.Success(cachedTicker);
            }

            var res = await _stockQuoteService.GetStockTickerAsync(symbol);

            return Result<StockTicker>.Success(res.Value!);
        }
    }
}
