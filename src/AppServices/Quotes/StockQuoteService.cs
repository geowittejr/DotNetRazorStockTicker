using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using AppDataModels.Config;
using AppDataModels.DomainModels;
using AppDataModels.Utility;
using AppServices.Quotes.Models;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

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
            var company = await lookupTask; 
            var quote = await quoteTask;
            var metrics = await metricsTask;

            var stockTicker = new StockTicker
            {
                Symbol = stockSymbol,
                CompanyName = company.IsSuccess ? company.Value!.CompanyName : "N/A",
                Price = quote.IsSuccess ? quote.Value!.Price : 0.00M,
                EarningsPerShare = metrics.IsSuccess ? metrics.Value!.EarningsPerShare : 0.00M,
                PriceToEarningsRatio = metrics.IsSuccess ? metrics.Value!.PriceToEarningsRatio : 0.00M
            };

            return Result<StockTicker>.Success(stockTicker);
        }

        private async Task<Result<SymbolLookupResponse>> LookupSymbolAsync(string symbol)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"{_options.BaseUrl}/search?q={symbol}&exchange=US&token={_options.ApiKey}");

            var obj = await GetObjectFromResponse<SymbolLookupResponse>(response);

            if (obj != null)
            {
                obj.Symbol = symbol;
                return Result<SymbolLookupResponse>.Success(obj);
            }
            else
            {
                return Result<SymbolLookupResponse>.Failure(new ResultError("404", $"Symbol lookup failed for symbol: {symbol}"));
            }
        }

        private async Task<Result<SymbolQuoteResponse>> GetSymbolQuoteAsync(string symbol)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"{_options.BaseUrl}/quote?symbol={symbol}&token={_options.ApiKey}");

            var obj = await GetObjectFromResponse<SymbolQuoteResponse>(response);

            if (obj != null)
            {
                obj.Symbol = symbol;
                return Result<SymbolQuoteResponse>.Success(obj);
            }
            else
            {
                return Result<SymbolQuoteResponse>.Failure(new ResultError("404", $"Symbol lookup failed for symbol: {symbol}"));
            }
        }

        private async Task<Result<SymbolMetricsResponse>> GetSymbolMetricsAsync(string symbol)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"{_options.BaseUrl}/stock/metric?symbol={symbol}&metric=all&token={_options.ApiKey}");

            var obj = await GetObjectFromResponse<SymbolMetricsResponse>(response);

            if (obj != null)
            {
                obj.Symbol = symbol;
                return Result<SymbolMetricsResponse>.Success(obj);
            }
            else
            {
                return Result<SymbolMetricsResponse>.Failure(new ResultError("404", $"Symbol lookup failed for symbol: {symbol}"));
            }
        }

        private async Task<T> GetObjectFromResponse<T>(HttpResponseMessage response)
        {
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            var obj = JsonConvert.DeserializeObject<T>(json);
            return obj!;
        }
    }
}
