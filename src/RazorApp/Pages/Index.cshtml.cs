using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AppServices;
using AppDataModels.DomainModels;
using System.Runtime.CompilerServices;
using AppDataModels.Utility;
using AppServices.Quotes.Utility;

namespace RazorApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ITickerService _tickerService;
        private readonly IHostEnvironment _hostEnvironment;

        public IndexModel(ILogger<IndexModel> logger, ITickerService tickerService, IHostEnvironment hostEnvironment)
        {
            _logger = logger;
            _tickerService = tickerService;
            _hostEnvironment = hostEnvironment;
        }

        [BindProperty]
        public int RefreshIntervalSeconds { get; set; }

        [BindProperty]
        public string StockSymbolsInput { get; set; } = string.Empty;

        public IEnumerable<StockTicker> StockTickers { get; set; } = [];

        public void OnGet()
        {
            // Add default values for testing only in development
            if (_hostEnvironment.IsDevelopment())
            {
                StockSymbolsInput = TestDataHelper.GetRandomStockSymbols();
            }
        }

        public async Task OnPostGetStockTickers()
        {
            var symbols = ValidateAndReturnSymbolsInput();

            Result<IEnumerable<StockTicker>> tickerResults = await _tickerService.GetStockTickersAsync(symbols);

            StockTickers = tickerResults.Value != null ? tickerResults.Value : []; 
        }

        private IEnumerable<string> ValidateAndReturnSymbolsInput()
        {
            // Split input into symbols and remove duplicates, e.g. "AAPL MSFT GOOG"
            var symbols = StockSymbolsInput
                .ToUpper()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)                
                .Distinct();

            // Update the input string to ensure it's valid
            StockSymbolsInput = string.Join(" ", symbols);

            return symbols;
        }
    }
}
