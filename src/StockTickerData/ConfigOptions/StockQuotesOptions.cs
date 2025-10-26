using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTickerData.ConfigOptions
{
    public class StockQuotesOptions
    {
        public const string SectionName = "StockQuotes";
        public required string BaseUrl { get; set; }
        public required string ApiKey { get; set; }
    }
}
