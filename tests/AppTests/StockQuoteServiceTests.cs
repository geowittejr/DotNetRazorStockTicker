using Microsoft.Extensions.Options;
using AppServices.Quotes;
using AppDataModels.Config;

namespace AppTests
{
    public class StockQuoteServiceTests
    {
        public IStockQuoteService GetStockQuoteService()
        {
            IOptions<StockQuotesApiOptions> options = Options.Create(new StockQuotesApiOptions
            {
                ApiKey = "test-api",
                BaseUrl = "https://test-url.com"
            });

            return new StockQuoteService(
                options,
                null!,
                null!);
        }

        [Fact]
        public async Task StockQuoteService_GetStockTickerAsync_ShouldNotThrowAnException()
        {
            var service = GetStockQuoteService();

            try
            {
                await service.GetStockTickerAsync("AAPL");
            }
            catch (Exception ex)
            {
                Assert.Fail("Method should not throw an exception.");
            }

            Assert.True(true);

        }
    }
}