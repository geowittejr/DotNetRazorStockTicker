using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AppServices;

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

        public async Task OnGetAsync()
        {
            _logger.LogInformation("Index page visited.");
            //await _tickerService.GetStockTickersAsync([null]);
        }
    }
}
