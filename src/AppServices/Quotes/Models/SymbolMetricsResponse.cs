using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AppServices.Quotes.Models
{
    public class SymbolMetricsResponse
    {
        [JsonProperty(PropertyName = "metric")]
        public required SymbolMetric Metric { get; set; }
    }

    public class SymbolMetric
    {
        [JsonProperty(PropertyName = "symbol")]
        public required string Symbol { get; set; }
        [JsonProperty(PropertyName = "epsTTM")]
        public decimal EarningsPerShare { get; set; }
        [JsonProperty(PropertyName = "peTTM")]
        public decimal PriceToEarningsRatio { get; set; }
    }
}
