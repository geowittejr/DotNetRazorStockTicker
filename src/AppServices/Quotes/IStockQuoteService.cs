using AppDataModels.DomainModels;
using AppDataModels.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppServices.Quotes
{
    /// <summary>
    /// Defines a service for retrieving stock quote information.
    /// </summary>
    public interface IStockQuoteService
    {
        /// <summary>
        /// Retrieves stock ticker information for the specified stock symbol.
        /// </summary>
        /// <param name="stockSymbol">The stock symbol to look up.</param>
        /// <returns>A Result containing either the StockTicker information if successful, 
        /// or an error if the operation failed.</returns>
        Task<Result<StockTicker>> GetStockTickerAsync(string stockSymbol);
    }
}
