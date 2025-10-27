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

        public async Task<Result<IEnumerable<StockTicker>>> GetStockTickersAsync(IEnumerable<string> stockSymbols)
        {
            try
            {
                // Start all async calls in parallel
                var tasks = stockSymbols.Select(symbol => GetStockTickerAsync(symbol)).ToList();

                // Await completion of all tasks
                var results = await Task.WhenAll(tasks);

                return Result<IEnumerable<StockTicker>>.Success(results.Select(x => x.Value!));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching stock tickers");
                return Result<IEnumerable<StockTicker>>.Failure(
                    new ResultError("500", $"Unexpected error: {ex.Message}"));
            }
        }

        private async Task<Result<StockTicker>> GetStockTickerAsync(string symbol)
        {
            var cachedTicker = _tickerCache.GetStockTicker(symbol);
            if (cachedTicker != null)
            {
                _logger.LogInformation("Getting stock ticker for symbol '{symbol}' from cache", symbol);
                return Result<StockTicker>.Success(cachedTicker);
            }

            var res = await _stockQuoteService.GetStockTickerAsync(symbol);
            _logger.LogInformation("Getting stock ticker for symbol '{symbol}' from API", symbol);

            if (res.IsSuccess && res.Value?.ApiStatusCode == "200")
            {
                _tickerCache.CacheStockTicker(res.Value!);
            }            

            return res;
        }
    }
}
