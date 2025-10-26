using Microsoft.Extensions.Logging;
using StockTickerData.Models;
using StockTickerServices.Caching;
using StockTickerServices.Quotes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTickerServices
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
