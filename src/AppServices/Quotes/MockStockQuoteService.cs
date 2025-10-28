using AppDataModels.DomainModels;
using AppDataModels.Utility;
using System.Linq;
using System.Net;

namespace AppServices.Quotes
{
    public class MockStockQuoteService : IStockQuoteService
    {
        public Task<Result<StockTicker>> GetStockTickerAsync(string stockSymbol)
        {
            var createdStatusCode = GetRandomStatusCode();
            var updatedStatusCode = GetRandomStatusCode();
            var hasErrorCode = !(createdStatusCode == "200" && updatedStatusCode == "200");

            var stockTicker = new StockTicker
            {
                Symbol = stockSymbol,
                CompanyName = hasErrorCode ? "[Unavailable]" : GetCompanyName(stockSymbol),
                Price = hasErrorCode ? 0.00M : GetRandomDecimal(105.00M, 600.00M),
                EarningsPerShare = hasErrorCode ? 0.00M : GetRandomDecimal(5.00M, 55.00M),
                PriceToEarningsRatio = hasErrorCode ? 0.00M : GetRandomDecimal(5.00M, 55.00M),
                CreatedStatusCode = createdStatusCode,
                UpdatedStatusCode = updatedStatusCode,
                CreatedUtc = DateTime.UtcNow.Date,
                UpdatedUtc = DateTime.UtcNow.Date
            };

            return Task.FromResult(Result<StockTicker>.Success(stockTicker));
        }

        private decimal GetRandomDecimal(decimal min, decimal max)
        {
            var random = new Random();
            return Math.Round((decimal)random.NextDouble() * (max - min) + min, 2);
        }

        private DateTime GetCreatedDateUtc()
        {
            return DateTime.UtcNow.Date;
        }

        private string GetRandomStatusCode()
        {
            var random = new Random();
            int roll = random.Next(100); // 0–99

            if (roll < 95) return "200";  // 94%
            if (roll < 98) return "429";  // 3%
            if (roll < 99) return "404";  // 2%
            return "500";                 // 1%
        }

        private string GetCompanyName(string symbol)
        {
            return symbol.ToUpper() switch
            {
                // Technology
                "AAPL" => "Apple Inc.",
                "MSFT" => "Microsoft Corporation",
                "GOOG" or "GOOGL" => "Alphabet Inc.",
                "AMZN" => "Amazon.com Inc.",
                "META" => "Meta Platforms Inc.",
                "NVDA" => "NVIDIA Corporation",
                "TSLA" => "Tesla Inc.",
                "AMD" => "Advanced Micro Devices Inc.",
                "INTC" => "Intel Corporation",
                "CSCO" => "Cisco Systems Inc.",
                "ORCL" => "Oracle Corporation",
                "IBM" => "International Business Machines Corporation",
                "ADBE" => "Adobe Inc.",
                "CRM" => "Salesforce Inc.",
                "QCOM" => "Qualcomm Incorporated",
                "AVGO" => "Broadcom Inc.",
                "TXN" => "Texas Instruments Incorporated",
                "NOW" => "ServiceNow Inc.",
                "JPM" => "JPMorgan Chase & Co.",
                "BAC" => "Bank of America Corporation",
                "WFC" => "Wells Fargo & Company",
                "C" => "Citigroup Inc.",
                "GS" => "The Goldman Sachs Group Inc.",
                "MS" => "Morgan Stanley",
                "BLK" => "BlackRock Inc.",
                "V" => "Visa Inc.",
                "MA" => "Mastercard Incorporated",
                "AXP" => "American Express Company",
                "SCHW" => "The Charles Schwab Corporation",
                "COIN" => "Coinbase Global Inc.",
                "JNJ" => "Johnson & Johnson",
                "PFE" => "Pfizer Inc.",
                "MRK" => "Merck & Co. Inc.",
                "ABBV" => "AbbVie Inc.",
                "LLY" => "Eli Lilly and Company",
                "TMO" => "Thermo Fisher Scientific Inc.",
                "UNH" => "UnitedHealth Group Incorporated",
                "AMGN" => "Amgen Inc.",
                "CVS" => "CVS Health Corporation",
                "PG" => "Procter & Gamble Company",
                "KO" => "The Coca-Cola Company",
                "PEP" => "PepsiCo Inc.",
                "MCD" => "McDonald's Corporation",
                "NKE" => "Nike Inc.",
                "SBUX" => "Starbucks Corporation",
                "DIS" => "The Walt Disney Company",
                "WMT" => "Walmart Inc.",
                "COST" => "Costco Wholesale Corporation",
                "HD" => "The Home Depot Inc.",
                "TGT" => "Target Corporation",
                "XOM" => "Exxon Mobil Corporation",
                "CVX" => "Chevron Corporation",
                "COP" => "ConocoPhillips",
                "SLB" => "Schlumberger Limited",
                "CAT" => "Caterpillar Inc.",
                "BA" => "The Boeing Company",
                "GE" => "General Electric Company",
                "DE" => "Deere & Company",
                "F" => "Ford Motor Company",
                "GM" => "General Motors Company",
                "T" => "AT&T Inc.",
                "VZ" => "Verizon Communications Inc.",
                "TMUS" => "T-Mobile US Inc.",
                "NFLX" => "Netflix Inc.",
                "CMCSA" => "Comcast Corporation",
                "WBD" => "Warner Bros. Discovery Inc.",
                "PARA" => "Paramount Global",
                "ROKU" => "Roku Inc.",
                "DAL" => "Delta Air Lines Inc.",
                "UAL" => "United Airlines Holdings Inc.",
                "AAL" => "American Airlines Group Inc.",
                "LUV" => "Southwest Airlines Co.",
                "BKNG" => "Booking Holdings Inc.",
                "MAR" => "Marriott International Inc.",
                "UBER" => "Uber Technologies Inc.",
                "LYFT" => "Lyft Inc.",
                "ABNB" => "Airbnb Inc.",
                "SNOW" => "Snowflake Inc.",
                "PLTR" => "Palantir Technologies Inc.",
                "SPOT" => "Spotify Technology S.A.",
                "SQ" => "Block Inc.",
                "PYPL" => "PayPal Holdings Inc.",
                _ => $"{symbol.ToUpper()} Corporation"
            };
        }

    }
}
