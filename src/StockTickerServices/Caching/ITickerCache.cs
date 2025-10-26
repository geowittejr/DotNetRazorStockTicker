using AppDataModels.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTickerServices.Caching
{
    public interface ITickerCache
    {
        void CacheStockTicker(StockTicker stockTicker);
        StockTicker? GetStockTicker(string stockSymbol);
        void RemoveStockTicker(string stockSymbol);
    }
}
