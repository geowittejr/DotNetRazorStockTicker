using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AppServices.Quotes.Models
{
    public class SymbolLookupResponse
    {
        [JsonProperty(PropertyName = "count")]
        public int Count { get; set; }
        [JsonProperty(PropertyName = "result")]
        public SymbolLookupResult[] Results { get; set; } = [];
    }

    public class SymbolLookupResult
    {
        [JsonProperty(PropertyName = "symbol")]
        public required string Symbol { get; set; }
        [JsonProperty(PropertyName = "description")]
        public required string CompanyName { get; set; }
    }
}
