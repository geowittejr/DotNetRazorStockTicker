using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace StockTickerServices.Quotes.Models
{
    public class SymbolMetricsResponse
    {
        [JsonPropertyName("symbol")]
        public required string Symbol { get; set; }
        [JsonPropertyName("epsTTM")]
        public decimal EarningsPerShare { get; set; }
        [JsonPropertyName("peTTM")]
        public decimal PriceToEarningsRatio { get; set; }
    }
}
