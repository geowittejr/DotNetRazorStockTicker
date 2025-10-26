using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using StockTickerData.ConfigOptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace StockTickerServices.Quotes
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

        public void DoSomething()
        {
            _logger.LogInformation("DoSomething called.");
        }

        public async Task LookupSymbolAsync(string symbol)
        {
            var client = _httpClientFactory.CreateClient();

            var response = await client.GetAsync($"{_options.BaseUrl}&token={_options.ApiKey}");
        }
    }
}
