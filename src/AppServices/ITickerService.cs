using AppDataModels.DomainModels;

namespace AppServices
{
    public interface ITickerService
    {
        Task<List<StockTicker>> GetStockTickersAsync(List<string> stockSymbols);
    }
}
