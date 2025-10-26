using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTickerData.Models
{
    public class StockTicker
    {
        public required string Symbol { get; set; }
        public required string CompanyName { get; set; }
        public decimal Price { get; set; }
        public decimal EarningsPerShare { get; set; }
        public decimal PriceToEarningsRatio { get; set; }
    }
}
