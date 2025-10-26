using AppDataModels.DomainModels;

namespace AppServices.Caching
{
    public interface ITickerCache
    {
        void CacheStockTicker(StockTicker stockTicker);
        StockTicker? GetStockTicker(string stockSymbol);
        void RemoveStockTicker(string stockSymbol);
    }
}
