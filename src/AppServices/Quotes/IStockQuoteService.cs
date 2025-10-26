using AppDataModels.DomainModels;
using AppDataModels.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppServices.Quotes
{
    public interface IStockQuoteService
    {
        Task<Result<StockTicker>> GetStockTickerAsync(string stockSymbol);
    }
}
