using Casino.Common;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using OnlineCasinoProjectConsole.Utility;
using OnlineCasinoProjectConsole.ViewModel;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace OnlineCasinoProjectConsole.UnitTest
{
    public class CasinoViewModelPlaySlotParseTest
    {
        public CasinoViewModelPlaySlotParseTest()
        {
        }

        [Theory]
        [InlineData(2.0)]
        public void PlaySlotTestNone(double betAmount)
        {
            var expectedResult = (new List<int>() { 1, 2, 3 }, 0, SlotsResultType.None);
            var json = JsonConvert.SerializeObject(expectedResult);

            var mockMessageHandler = new Mock<HttpMessageHandler>();

            mockMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(json, Encoding.UTF8, "application/json")
                });
            var underTest = new CasinoViewModel(new HttpClient(mockMessageHandler.Object), new DateConverter());

            var result = underTest.PlaySlot(betAmount);

            Assert.Equal(new List<int>() { 1, 2, 3 }, result.Item1);
            Assert.Equal("\nUnfortunately, you did not win anything. Thank you for playing.", result.Item2);
        }

        [Theory]
        [InlineData(2.0)]
        public void PlaySlotTestDouble(double betAmount)
        {
            var expectedResult = (new List<int>() { 1, 1, 3 }, 4.0, SlotsResultType.Double);
            var json = JsonConvert.SerializeObject(expectedResult);

            var mockMessageHandler = new Mock<HttpMessageHandler>();

            mockMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(json, Encoding.UTF8, "application/json")
                });
            var underTest = new CasinoViewModel(new HttpClient(mockMessageHandler.Object), new DateConverter());

            var result = underTest.PlaySlot(betAmount);

            Assert.Equal(new List<int>() { 1, 1, 3 }, result.Item1);
            Assert.Equal("\nDOUBLE!! Congratulations. Your winnings are: $4", result.Item2);
        }

        [Theory]
        [InlineData(2.0)]
        public void PlaySlotTestTriple(double betAmount)
        {
            var expectedResult = (new List<int>() { 1, 1, 1 }, 6.0, SlotsResultType.Triple);
            var json = JsonConvert.SerializeObject(expectedResult);

            var mockMessageHandler = new Mock<HttpMessageHandler>();

            mockMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(json, Encoding.UTF8, "application/json")
                });
            var underTest = new CasinoViewModel(new HttpClient(mockMessageHandler.Object), new DateConverter());

            var result = underTest.PlaySlot(betAmount);

            Assert.Equal(new List<int>() { 1, 1, 1 }, result.Item1);
            Assert.Equal("\nTRIPLE!! Congratulations. Your winnings are: $6", result.Item2);
        }

        [Theory]
        [InlineData(2.0)]
        public void PlaySlotTestJackPot(double betAmount)
        {
            var expectedResult = (new List<int>() { 7, 7, 7 }, 14.0, SlotsResultType.JackPot);
            var json = JsonConvert.SerializeObject(expectedResult);

            var mockMessageHandler = new Mock<HttpMessageHandler>();

            mockMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(json, Encoding.UTF8, "application/json")
                });
            var underTest = new CasinoViewModel(new HttpClient(mockMessageHandler.Object), new DateConverter());

            var result = underTest.PlaySlot(betAmount);

            Assert.Equal(new List<int>() { 7, 7, 7 }, result.Item1);
            Assert.Equal("\nJACKPOT!! Congratulations. Your winnings are: $14", result.Item2);
        }

        [Theory]
        [InlineData(2.0)]
        public void PlaySlotTestError(double betAmount)
        {
            var expectedResult = (new List<int>() { 7, 7, 7 }, 14.0, SlotsResultType.JackPot);
            var json = JsonConvert.SerializeObject(expectedResult);

            var mockMessageHandler = new Mock<HttpMessageHandler>();

            mockMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Content = new StringContent(json, Encoding.UTF8, "application/json")
                });
            var underTest = new CasinoViewModel(new HttpClient(mockMessageHandler.Object), new DateConverter());

            var result = underTest.PlaySlot(betAmount);

            Assert.Null(result.Item1);
            Assert.Equal("An Error has Occured.", result.Item2);
        }

        [Theory]
        [InlineData("1")]
        public void ParseInputStringIntTestSuccess(string input)
        {
            var mockMessageHandler = new Mock<HttpMessageHandler>();

            mockMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Content = new StringContent("Test")
                });

            var underTest = new CasinoViewModel(new HttpClient(mockMessageHandler.Object), new DateConverter());
            underTest.ParseInputStringInt(input, out var output);
            Assert.Equal(Convert.ToInt32(input), output);
        }

        [Theory]
        [InlineData("aa")]
        public void ParseInputStringIntTestFormat(string input)
        {
            var mockMessageHandler = new Mock<HttpMessageHandler>();

            mockMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Content = new StringContent("Test")
                });

            var underTest = new CasinoViewModel(new HttpClient(mockMessageHandler.Object), new DateConverter());

            underTest.ParseInputStringInt(input, out var output);
            Assert.Null(output);
        }

        [Theory]
        [InlineData("12321313781731381832781371283182731")]
        public void ParseInputStringIntOverflow(string input)
        {
            var mockMessageHandler = new Mock<HttpMessageHandler>();

            mockMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Content = new StringContent("Test")
                });

            var underTest = new CasinoViewModel(new HttpClient(mockMessageHandler.Object), new DateConverter());
            underTest.ParseInputStringInt(input, out var output);
            Assert.Null(output);
        }

        [Theory]
        [InlineData("1")]
        public void ParseInputStringDoubleTestSuccess(string input)
        {
            var mockMessageHandler = new Mock<HttpMessageHandler>();

            mockMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Content = new StringContent("Test")
                });

            var underTest = new CasinoViewModel(new HttpClient(mockMessageHandler.Object), new DateConverter());
            underTest.ParseInputStringDouble(input, out var output);
            Assert.Equal(Convert.ToDouble(input), output);
        }

        [Theory]
        [InlineData("aa")]
        public void ParseInputStringDoubleTestFormat(string input)
        {
            var mockMessageHandler = new Mock<HttpMessageHandler>();

            mockMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Content = new StringContent("Test")
                });

            var underTest = new CasinoViewModel(new HttpClient(mockMessageHandler.Object), new DateConverter());

            underTest.ParseInputStringDouble(input, out var output);
            Assert.Equal(0, output);
        }

        [Theory]
        [InlineData("99999999999999999999999999999999999999999" +
            "9999999999999999999999999999999999999999999999999" +
            "9999999999999999999999999999999999999999999999999" +
            "9999999999999999999999999999999999999999999999999" +
            "9999999999999999999999999999999999999999999999999" +
            "9999999999999999999999999999999999999999999999999" +
            "9999999999999999999999999999999999999999999999999" +
            "9999999999999999999999999999999999999999999999999")]
        public void ParseInputStringDoubleOverflow(string input)
        {
            var mockMessageHandler = new Mock<HttpMessageHandler>();

            mockMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Content = new StringContent("Test")
                });

            var underTest = new CasinoViewModel(new HttpClient(mockMessageHandler.Object), new DateConverter());
            underTest.ParseInputStringDouble(input, out var output);
            Assert.Equal(0, output);
        }
    }
}
