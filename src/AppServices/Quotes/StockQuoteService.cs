using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using AppDataModels.Config;
using AppDataModels.DomainModels;
using AppDataModels.Utility;

namespace AppServices.Quotes
{
    public class StockQuoteService : IStockQuoteService
    {
        private readonly StockQuotesOptions _options;
        private readonly ILogger<StockQuoteService> _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        public StockQuoteService(
            IOptions<StockQuotesOptions> options, 
            ILogger<StockQuoteService> logger, 
            IHttpClientFactory httpClientFactory) 
        { 
            _options = options.Value;
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }
        public async Task<Result<StockTicker>> GetStockTickerAsync(string stockSymbol)
        {
            return Result<StockTicker>.Success(new StockTicker { CompanyName = "Mock Company", Symbol = stockSymbol });
        }

        private async Task LookupSymbolAsync(string symbol)
        {
            var client = _httpClientFactory.CreateClient();

            var response = await client.GetAsync($"{_options.BaseUrl}&token={_options.ApiKey}");
        }
    }
}
