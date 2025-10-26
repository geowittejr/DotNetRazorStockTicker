using AppDataModels.DomainModels;

namespace StockTickerServices
{
    public interface ITickerService
    {
        Task<List<StockTicker>> GetStockTickersAsync(List<string> stockSymbols);
    }
}
