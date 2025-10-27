using AppDataModels.DomainModels;
using AppDataModels.Utility;

namespace AppServices
{
    public interface ITickerService
    {
        Task<Result<IEnumerable<StockTicker>>> GetStockTickersAsync(IEnumerable<string> stockSymbols);
    }
}
