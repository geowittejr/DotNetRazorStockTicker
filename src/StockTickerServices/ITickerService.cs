using StockTickerData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTickerServices
{
    public interface ITickerService
    {
        Task<List<StockTicker>> GetStockTickersAsync(List<string> stockSymbols);
    }
}
