using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDataModels.Config
{
    public class StockQuotesApiOptions
    {
        /// <summary>
        /// Section name used in appsettings.json
        /// </summary>
        public const string SectionName = "StockQuotesApi";
        /// <summary>
        /// Bse URL for the Stock Quotes API
        /// </summary>
        public required string BaseUrl { get; set; }
        /// <summary>
        /// Gets or sets the API key used for authenticating requests.
        /// </summary>
        public required string ApiKey { get; set; }
        /// <summary>
        /// Cache time-to-live in seconds
        /// </summary>
        public int CacheTtlSeconds { get; set; } = 60;
        /// <summary>
        /// Cache maximum expiry in minutes
        /// </summary>
        public int CacheMaxExpiryMinutes { get; set; } = 10;
    }
}
