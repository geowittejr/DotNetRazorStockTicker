using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace StockTickerServices.Quotes.Models
{
    public class SymbolQuoteResponse
    {
        [JsonPropertyName("symbol")]
        public required string Symbol { get; set; }
        [JsonPropertyName("c")]
        public decimal Price { get; set; }
    }
}
