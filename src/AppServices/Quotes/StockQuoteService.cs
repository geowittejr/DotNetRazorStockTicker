using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using AppDataModels.Config;
using AppDataModels.DomainModels;
using AppDataModels.Utility;
using AppServices.Quotes.Models;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using AppServices.Quotes.Utility;

namespace AppServices.Quotes
{
    public class StockQuoteService : IStockQuoteService
    {
        private readonly StockQuotesApiOptions _options;
        private readonly ILogger<StockQuoteService> _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        public StockQuoteService(
            IOptions<StockQuotesApiOptions> options, 
            ILogger<StockQuoteService> logger, 
            IHttpClientFactory httpClientFactory) 
        { 
            _options = options.Value;
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }
        public async Task<Result<StockTicker>> GetStockTickerAsync(string stockSymbol)
        {
            // Start all three async calls at once
            var lookupTask = LookupSymbolAsync(stockSymbol);
            var quoteTask = GetSymbolQuoteAsync(stockSymbol);
            var metricsTask = GetSymbolMetricsAsync(stockSymbol);

            // Wait for all to complete
            await Task.WhenAll(lookupTask, quoteTask, metricsTask);

            // Now combine results
            var lookup = await lookupTask; 
            var quote = await quoteTask;
            var metrics = await metricsTask;

            var stockTicker = new StockTicker
            {
                Symbol = stockSymbol,
                CompanyName = lookup.IsSuccess ? lookup.Value!.CompanyName : "[UNAVAILABLE]",
                Price = quote.IsSuccess ? quote.Value!.Price : 0.00M,
                EarningsPerShare = metrics.IsSuccess ? metrics.Value!.Metric.EarningsPerShare : 0.00M,
                PriceToEarningsRatio = metrics.IsSuccess ? metrics.Value!.Metric.PriceToEarningsRatio : 0.00M,
                ApiStatusCode = GetApiStatusCode(lookup, quote, metrics)
            };

            return Result<StockTicker>.Success(stockTicker);
        }

        private string GetApiStatusCode(Result<SymbolLookupResult> lookupResult, Result<SymbolQuoteResponse> quoteResult, Result<SymbolMetricsResponse> metricsResult)
        {
            if (lookupResult.Error != null)
                return lookupResult.Error.StatusCode;
            if (quoteResult.Error != null)
                return quoteResult.Error.StatusCode;
            if (metricsResult.Error != null)
                return metricsResult.Error.StatusCode;
            return "200";
        }

        private async Task<Result<SymbolLookupResult>> LookupSymbolAsync(string symbol)
        {
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");

            SymbolLookupResponse? result = null;
            try
            {
                var response = await client.GetAsync($"{_options.BaseUrl}/search?q={symbol}&exchange=US&token={_options.ApiKey}");
                
                var resultError = response.GetResultErrorIfAny();
                if (resultError != null)
                    return Result<SymbolLookupResult>.Failure(resultError);

                result = await response.DeserializeContentAsync<SymbolLookupResponse>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during symbol lookup for {Symbol}", symbol);
                return Result<SymbolLookupResult>.Failure(new ResultError("404", $"Symbol lookup failed for symbol: {symbol}", ex));
            }

            if (result != null && result.Count > 0)
            {
                var resultSymbol = result.Results.FirstOrDefault(r => r.Symbol.Equals(symbol, StringComparison.OrdinalIgnoreCase));
                return Result<SymbolLookupResult>.Success(resultSymbol!);
            }
            else
            {
                return Result<SymbolLookupResult>.Failure(new ResultError("404", $"Symbol lookup failed for symbol: {symbol}"));
            }
        }

        private async Task<Result<SymbolQuoteResponse>> GetSymbolQuoteAsync(string symbol)
        {
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");

            SymbolQuoteResponse? result = null;
            try
            {
                var response = await client.GetAsync($"{_options.BaseUrl}/quote?symbol={symbol}&token={_options.ApiKey}");
                
                var resultError = response.GetResultErrorIfAny();
                if (resultError != null)
                    return Result<SymbolQuoteResponse>.Failure(resultError);

                result = await response.DeserializeContentAsync<SymbolQuoteResponse>();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during quote request for {Symbol}", symbol);
                return Result<SymbolQuoteResponse>.Failure(new ResultError("404", $"Quote request failed for symbol: {symbol}", ex));
            }

            if (result != null)
            {
                result.Symbol = symbol;
                return Result<SymbolQuoteResponse>.Success(result);
            }
            else
            {
                return Result<SymbolQuoteResponse>.Failure(new ResultError("404", $"Quote request failed for symbol: {symbol}"));
            }
        }

        private async Task<Result<SymbolMetricsResponse>> GetSymbolMetricsAsync(string symbol)
        {
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");

            SymbolMetricsResponse? result = null;
            try
            {
                var response = await client.GetAsync($"{_options.BaseUrl}/stock/metric?symbol={symbol}&metric=all&token={_options.ApiKey}");

                var resultError = response.GetResultErrorIfAny();
                if (resultError != null)
                    return Result<SymbolMetricsResponse>.Failure(resultError);

                result = await response.DeserializeContentAsync<SymbolMetricsResponse>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during quote request for {Symbol}", symbol);
                return Result<SymbolMetricsResponse>.Failure(new ResultError("404", $"Quote request failed for symbol: {symbol}", ex));
            }

            if (result != null)
            {
                result.Metric.Symbol = symbol;
                return Result<SymbolMetricsResponse>.Success(result);
            }
            else
            {
                return Result<SymbolMetricsResponse>.Failure(new ResultError("404", $"Quote request failed for symbol: {symbol}"));
            }
        }
    }
}
