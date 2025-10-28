using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDataModels.DomainModels
{
    /// <summary>
    /// Represents a stock ticker
    /// </summary>
    public class StockTicker
    {
        /// <summary>
        /// The unique stock ticker symbol (e.g., "AAPL", "MSFT").
        /// </summary>
        public required string Symbol { get; set; }

        /// <summary>
        /// The full company name associated with the ticker symbol.
        /// </summary>
        public required string CompanyName { get; set; }

        /// <summary>
        /// The current price per share for the stock.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Earnings per share (EPS) used for valuation calculations.
        /// </summary>
        public decimal EarningsPerShare { get; set; }

        /// <summary>
        /// Price-to-earnings (P/E) ratio
        /// </summary>
        public decimal PriceToEarningsRatio { get; set; }

        /// <summary>
        /// UTC timestamp indicating when the record was created. Defaults to DateTime.UtcNow.
        /// </summary>
        public DateTime CreatedUtc { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// UTC timestamp indicating when the record was last updated. Defaults to DateTime.UtcNow.
        /// </summary>
        public DateTime UpdatedUtc { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Status code or indicator recorded when the entity was created (default "200").
        /// </summary>
        public string CreatedStatusCode { get; set; } = "200";

        /// <summary>
        /// Status code or indicator recorded when the entity was last updated (default "200").
        /// </summary>
        public string UpdatedStatusCode { get; set; } = "200";
    }
}
