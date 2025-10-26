using Microsoft.Extensions.Options;
using StockTickerServices.Quotes;
using AppDataModels.Config;

namespace StockTickerTests
{
    public class StockQuoteServiceTests
    {
        public IStockQuoteService GetStockQuoteService()
        {
            IOptions<StockQuotesOptions> options = Options.Create(new StockQuotesOptions
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
        public void StockQuoteService_DoSomething_ShouldNotThrowAnException()
        {
            var service = GetStockQuoteService();

            try
            {
                service.DoSomething();
            }
            catch (Exception ex)
            {
                Assert.Fail("DoSomething should not throw an exception.");
            }

            Assert.True(true);

        }
    }
}