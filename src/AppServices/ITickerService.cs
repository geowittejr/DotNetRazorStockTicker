using AppDataModels.DomainModels;
using AppDataModels.Utility;

namespace AppServices
{
    /// <summary>
    /// Provides methods for retrieving stock ticker information.
    /// </summary>
    public interface ITickerService
    {
        /// <summary>
        /// Asynchronously retrieves stock ticker details for the specified stock symbols.
        /// </summary>
        /// <param name="stockSymbols">A collection of stock symbols to retrieve information for.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains a <see cref="Result{T}"/>
        /// with an enumerable of <see cref="StockTicker"/> objects if successful, or error details if failed.
        /// </returns>
        Task<Result<IEnumerable<StockTicker>>> GetStockTickersAsync(IEnumerable<string> stockSymbols);
    }
}
