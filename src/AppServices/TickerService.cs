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

        public async Task<Result<List<StockTicker>>> GetStockTickersAsync(IEnumerable<string> stockSymbols)
        {
            try
            {
                // Start all async calls in parallel
                var tasks = stockSymbols.Select(symbol => GetStockTickerAsync(symbol)).ToList();

                // Await completion of all tasks
                var results = await Task.WhenAll(tasks);

                // Separate successes and failures
                var successfulTickers = results
                    .Where(r => r.IsSuccess && r.Value != null)
                    .Select(r => r.Value!)
                    .ToList();

                if (successfulTickers.Count == 0)
                {
                    return Result<List<StockTicker>>.Failure(
                        new ResultError("404", "No stock tickers could be retrieved"));
                }

                return Result<List<StockTicker>>.Success(successfulTickers);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching stock tickers");
                return Result<List<StockTicker>>.Failure(
                    new ResultError("500", $"Unexpected error: {ex.Message}"));
            }
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
