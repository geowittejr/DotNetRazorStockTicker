using AppDataModels.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppServices.Quotes
{
    public interface IStockQuoteService
    {
        Task<StockTicker> GetStockTickerAsync(string stockSymbol);
    }
}
