using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StockTickerServices.Quotes;

namespace StockTickerRazorApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IStockQuoteService _stockQuoteService;

        public IndexModel(ILogger<IndexModel> logger, IStockQuoteService stockQuoteService)
        {
            _logger = logger;
            _stockQuoteService = stockQuoteService;
        }

        public void OnGet()
        {
            _logger.LogInformation("Index page visited.");
            _stockQuoteService.DoSomething();
        }
    }
}
