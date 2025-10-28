using Moq;
using System.Net;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using AppServices.Quotes;
using AppDataModels.Config;
using AppServices.Quotes.Models;
using AppDataModels.Utility;
using Newtonsoft.Json;

namespace AppServices.Tests
{
    public class StockQuoteServiceTests
    {
        #region Mock Setup

        private readonly Mock<ILogger<StockQuoteService>> _loggerMock = new();
        private readonly Mock<IOptions<StockQuotesApiOptions>> _optionsMock = new();
        private readonly Mock<IHttpClientFactory> _httpClientFactoryMock = new();

        // Helper to create StockQuoteService with mocked dependencies
        private StockQuoteService CreateStockQuoteServiceMock(HttpMessageHandler mockedHandler)
        {
            // Set up HttpClient with mocked handler
            var httpClientMock = new HttpClient(mockedHandler);
            _httpClientFactoryMock.Setup(f => f.CreateClient(It.IsAny<string>())).Returns(httpClientMock);

            // Set up options
            _optionsMock.Setup(o => o.Value).Returns(CreateStockQuotesApiOptions());

            return new StockQuoteService(_optionsMock.Object, _loggerMock.Object, _httpClientFactoryMock.Object);
        }

        // Helper class to mock HTTP message responses
        private class HttpMessageHandlerMock : HttpMessageHandler
        {
            private readonly Func<HttpRequestMessage, HttpResponseMessage> _responseFunc;

            public HttpMessageHandlerMock(Func<HttpRequestMessage, HttpResponseMessage> responseFunc)
            {
                _responseFunc = responseFunc;
            }

            protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
                => Task.FromResult(_responseFunc(request));
        }

        // Helper to create StockQuotesApiOptions
        private StockQuotesApiOptions CreateStockQuotesApiOptions() => new()
        {
            BaseUrl = "https://fake-api.test",
            ApiKey = "fake-api-key",
            CacheMaxExpiryMinutes = 10,
            CacheTtlSeconds = 60
        };

        private static HttpResponseMessage JsonResponse(object obj) => new(HttpStatusCode.OK)
        {
            Content = new StringContent(JsonConvert.SerializeObject(obj))
        };

        private static HttpResponseMessage ErrorResponse(HttpStatusCode statusCode) => new(statusCode)
        {
            Content = new StringContent("{\"error\":\"Bad request\"}")
        };

        #endregion Mock Setup

        #region Facts
        [Fact]
        public async Task GetStockTickerAsync_ReturnsSuccess_AndCombinedData()
        {
            // Arrange
            var httpMessageHandlerMock = new HttpMessageHandlerMock(httpRequestMessage =>
            {
                if (httpRequestMessage.RequestUri!.AbsoluteUri.Contains("/search"))
                    return JsonResponse(new { count = 1, result = new[] { new { symbol = "AAPL", description = "Apple Inc" } } });
                if (httpRequestMessage.RequestUri!.AbsoluteUri.Contains("/quote"))
                    return JsonResponse(new { c = 187.51M }); // price
                if (httpRequestMessage.RequestUri!.AbsoluteUri.Contains("/metric"))
                    return JsonResponse(new { metric = new { symbol = "AAPL", epsTTM = 6.25M, peTTM = 30.45M } });
                return ErrorResponse(HttpStatusCode.NotFound);
            });

            var service = CreateStockQuoteServiceMock(httpMessageHandlerMock);

            // Act
            var result = await service.GetStockTickerAsync("AAPL");

            // Assert
            Assert.True(result.Error == null);
            Assert.NotNull(result.Value);
            Assert.Equal("AAPL", result.Value.Symbol);
            Assert.Equal("Apple Inc", result.Value.CompanyName);
            Assert.Equal(187.51M, result.Value.Price);
            Assert.Equal("200", result.Value.CreatedStatusCode);
        }

        [Fact]
        public async Task GetStockTickerAsync_WhenAnyCallFails_SetsCreatedStatusCode()
        {
            // Arrange
            var httpMessageHandlerMock1 = new HttpMessageHandlerMock(httpRequestMessage =>
            {
                if (httpRequestMessage.RequestUri!.AbsoluteUri.Contains("/quote"))
                    return ErrorResponse(HttpStatusCode.InternalServerError);

                return JsonResponse(new { c = 150.00M });
            });

            var httpMessageHandlerMock = new HttpMessageHandlerMock(httpRequestMessage =>
            {
                if (httpRequestMessage.RequestUri!.AbsoluteUri.Contains("/search"))
                    return JsonResponse(new { count = 1, result = new[] { new { symbol = "AAPL", description = "Apple Inc" } } });
                if (httpRequestMessage.RequestUri!.AbsoluteUri.Contains("/quote"))
                    return JsonResponse(new { c = 187.51M }); // price
                //This will fail on the metric call
                return ErrorResponse(HttpStatusCode.NotFound);
            });

            var service = CreateStockQuoteServiceMock(httpMessageHandlerMock);

            // Act
            var result = await service.GetStockTickerAsync("AAPL");

            // Assert
            Assert.True(result.Error == null);
            Assert.NotNull(result.Value);
            Assert.NotEqual("200", result.Value.CreatedStatusCode);
            Assert.Equal("404", result.Value.CreatedStatusCode);
        }

        [Fact]
        public async Task GetStockTickerAsync_UnexpectedException_ReturnsFallbackValues()
        {
            // Arrange
            var httpMessageHandlerMock = new HttpMessageHandlerMock(req => throw new HttpRequestException("Network error"));
            var service = CreateStockQuoteServiceMock(httpMessageHandlerMock);

            // Act
            var result = await service.GetStockTickerAsync("AAPL");

            // Assert
            Assert.True(result.Error == null);
            Assert.NotNull(result.Value);
            Assert.Equal("[UNAVAILABLE]", result.Value.CompanyName);
            Assert.Equal(0M, result.Value.Price);
            Assert.Equal(0M, result.Value.EarningsPerShare);
            Assert.Equal(0M, result.Value.PriceToEarningsRatio);
        }

        #endregion Facts
    }
}