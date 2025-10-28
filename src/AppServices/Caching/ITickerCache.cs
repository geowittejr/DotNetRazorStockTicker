using AppDataModels.DomainModels;

namespace AppServices.Caching
{
    /// <summary>
    /// Defines operations for caching <see cref="StockTicker"/> instances.
    /// Implementations are responsible for storing, retrieving, and removing stock ticker data.
    /// </summary>
    public interface ITickerCache
    {
        /// <summary>
        /// Stores or updates the specified <see cref="StockTicker"/> in the cache.
        /// </summary>
        /// <param name="stockTicker">The <see cref="StockTicker"/> to cache. The <see cref="StockTicker.Symbol"/> should be populated.</param>
        void CacheStockTicker(StockTicker stockTicker);

        /// <summary>
        /// Retrieves a cached <see cref="StockTicker"/> by its symbol.
        /// </summary>
        /// <param name="stockSymbol">The symbol of the stock to retrieve from the cache.</param>
        /// <returns>The cached <see cref="StockTicker"/> if present; otherwise <c>null</c>.</returns>
        StockTicker? GetStockTicker(string stockSymbol);

        /// <summary>
        /// Removes the cached entry for the specified stock symbol, if it exists.
        /// </summary>
        /// <param name="stockSymbol">The symbol of the stock to remove from the cache.</param>
        void RemoveStockTicker(string stockSymbol);
    }
}
