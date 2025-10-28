using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDataModels.Config
{
    public class StockQuotesApiOptions
    {
        public const string SectionName = "StockQuotesApi";
        public required string BaseUrl { get; set; }
        public required string ApiKey { get; set; }
        public int CacheTtlSeconds { get; set; } = 60;
        public int CacheMaxExpiryMinutes { get; set; } = 10;
    }
}
