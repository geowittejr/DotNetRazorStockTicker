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
        private readonly IHttpClientFactory _httpClientFactory;
        public StockQuoteService(IHttpClientFactory httpClientFactory) 
        { 
            _httpClientFactory = httpClientFactory;
        }

        public async Task LookupSymbolAsync(string symbol)
        {
            var client = _httpClientFactory.CreateClient();

            var response = await client.GetAsync($"/api/v1/search?q={symbol}&token={{YOUR_API_KEY}}");
        }
    }
}
