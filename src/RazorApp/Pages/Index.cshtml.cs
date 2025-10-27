using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AppServices;
using AppDataModels.DomainModels;
using System.Runtime.CompilerServices;
using AppDataModels.Utility;

namespace RazorApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ITickerService _tickerService;

        public IndexModel(ILogger<IndexModel> logger, ITickerService tickerService)
        {
            _logger = logger;
            _tickerService = tickerService;
        }

        [BindProperty]
        public int RefreshIntervalSeconds { get; set; }

        [BindProperty]
        public string StockSymbolsInput { get; set; } = string.Empty;

        public List<StockTicker> StockTickers { get; set; } = new();

        public void OnGet()
        {
            // Add default values for testing
            StockSymbolsInput = "AAPL GOOG MSFT";
        }

        public async Task OnPostGetStockTickers()
        {
            var symbols = ValidateAndReturnSymbolsInput();

            Result<List<StockTicker>> res = await _tickerService.GetStockTickersAsync(symbols);

            StockTickers = res.IsSuccess ? res.Value! : [];

            //foreach (var symbol in symbols)
            //{
                
            //    StockTickers.Add(new StockTicker
            //    {
            //        Symbol = symbol.ToUpper(),
            //        CompanyName = GetCompanyName(symbol),
            //        Price = GetRandomDecimal(100, 400),
            //        PriceToEarningsRatio = GetRandomDecimal(10, 35),
            //        EarningsPerShare = GetRandomDecimal(2, 10)
            //    });
            //}
        }

        private IEnumerable<string> ValidateAndReturnSymbolsInput()
        {
            // Split input into symbols and remove duplicates, e.g. "AAPL MSFT GOOG"
            var symbols = StockSymbolsInput
                .Split(" ", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                .Distinct();

            // Update the input string to ensure it's valid
            StockSymbolsInput = string.Join(" ", symbols);

            return symbols;
        }

        // Dummy methods to simulate API data
        private string GetCompanyName(string symbol)
        {
            return symbol.ToUpper() switch
            {
                "AAPL" => "Apple Inc.",
                "MSFT" => "Microsoft Corp.",
                "GOOG" => "Alphabet Inc.",
                "AMZN" => "Amazon.com Inc.",
                _ => $"{symbol.ToUpper()} Corporation"
            };
        }

        private decimal GetRandomDecimal(decimal min, decimal max)
        {
            var random = new Random();
            return Math.Round((decimal)random.NextDouble() * (max - min) + min, 2);
        }
    }
}
