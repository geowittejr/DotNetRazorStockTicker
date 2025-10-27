using AppDataModels.DomainModels;
using AppDataModels.Utility;

namespace AppServices
{
    public interface ITickerService
    {
        Task<Result<List<StockTicker>>> GetStockTickersAsync(IEnumerable<string> stockSymbols);
    }
}
