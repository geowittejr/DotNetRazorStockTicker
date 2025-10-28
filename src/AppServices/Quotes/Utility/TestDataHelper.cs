using AppDataModels.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppServices.Quotes.Utility
{
    public class TestDataHelper
    {
        /// <summary>
        /// Get a random string of 20 stock symbols from the predefined values.
        /// </summary>
        /// <returns>A string of space-separated stock symbols</returns>
        public static string GetRandomStockSymbols()
        {
            var symbols = new[]
            {
                "AAPL","MSFT","GOOG","GOOGL","AMZN","META","NVDA","TSLA","AMD","INTC",
                "CSCO","ORCL","IBM","ADBE","CRM","QCOM","AVGO","TXN","NOW","JPM",
                "BAC","WFC","C","GS","MS","BLK","V","MA","AXP","SCHW","COIN",
                "JNJ","PFE","MRK","ABBV","LLY","TMO","UNH","AMGN","CVS","PG",
                "KO","PEP","MCD","NKE","SBUX","DIS","WMT","COST","HD","TGT",
                "XOM","CVX","COP","SLB","CAT","BA","GE","DE","F","GM",
                "T","VZ","TMUS","NFLX","CMCSA","WBD","PARA","ROKU","DAL","UAL",
                "AAL","LUV","BKNG","MAR","UBER","LYFT","ABNB","SNOW","PLTR","SPOT",
                "SQ","PYPL"
            };

            var random = new Random();
            var randomSymbols = symbols
                .OrderBy(_ => random.Next()) // shuffle
                .Take(20)                    // take 20
                .ToArray();

            return string.Join(" ", randomSymbols);
        }
    }
}
