using AppDataModels.Config;
using AppDataModels.DomainModels;
using AppDataModels.Utility;
using AppServices.Caching;
using AppServices.Quotes;
using AppServices.Quotes.Models;
using AppServices.Quotes.Utility;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net;

namespace AppServices
{
    public class TickerService : ITickerService
    {
        private readonly StockQuotesApiOptions _options;
        private readonly ILogger<TickerService> _logger;
        private readonly IStockQuoteService _stockQuoteService;
        private readonly ITickerCache _tickerCache;

        public TickerService(
            IOptions<StockQuotesApiOptions> options,
            ILogger<TickerService> logger, 
            IStockQuoteService stockQuoteService,
            ITickerCache tickerCache) 
        {
            _options = options.Value;
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
                _logger.LogError(ex, "GetStockTickersAsync exception");

                return Result<IEnumerable<StockTicker>>.Failure(
                    HttpHelperMethods.GetResultErrorForStatusCode(HttpStatusCode.InternalServerError, ex)!);
            }
        }

        private async Task<Result<StockTicker>> GetStockTickerAsync(string symbol)
        {
            var cachedTicker = _tickerCache.GetStockTicker(symbol);

            // If the cached ticker isn't stale, send it.
            if(cachedTicker != null && cachedTicker.CreatedStatusCode == "200" && !StockTickerIsOutdated(cachedTicker))
            {
                _logger.LogInformation("Return cached ticker. Symbol: {symbol}", symbol);
                return Result<StockTicker>.Success(cachedTicker);
            }

            // Request a new ticker from the API.
            var apiResult = await _stockQuoteService.GetStockTickerAsync(symbol);

            // Ticker request was successful or there's no cached ticker.
            // Cache and return this one.
            if ((apiResult.IsSuccess && apiResult.Value!.CreatedStatusCode == "200") || cachedTicker == null)
            {
                _tickerCache.CacheStockTicker(apiResult.Value!);
                return apiResult;
            }

            // Ticker request was not successful.
            // Update and send the cached ticker.
            cachedTicker.UpdatedUtc = DateTime.UtcNow;
            cachedTicker.UpdatedStatusCode = apiResult.Value!.CreatedStatusCode;          

            return Result<StockTicker>.Success(cachedTicker);
        }

        private bool StockTickerIsOutdated(StockTicker ticker) =>
            (DateTime.UtcNow - ticker.CreatedUtc).TotalSeconds > _options.CacheTtlSeconds;
    }
}
