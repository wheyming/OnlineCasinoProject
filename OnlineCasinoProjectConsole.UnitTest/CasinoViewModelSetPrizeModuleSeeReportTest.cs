using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using OnlineCasinoProjectConsole.Interfaces;
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
    public class CasinoViewModelSetPrizeModuleSeeReportTest
    {
        private Mock<IDateConverter> _mockDateConverter;
        public CasinoViewModelSetPrizeModuleSeeReportTest()
        {
            _mockDateConverter = new Mock<IDateConverter>(MockBehavior.Strict);
        }

        [Theory]
        [InlineData(1)]
        public void SetPrizeModuleTest(int input)
        {
            var expectedResult = "Test";
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

            var result = underTest.SetPrizeModuleStatus(input);

            Assert.Equal("Test", result);
        }

        [Theory]
        [InlineData("Nov")]
        public void SeeDayFinancialReportTestSuccess(string input)
        {
            var expectedResult = 2.0;
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
            var underTest = new CasinoViewModel(new HttpClient(mockMessageHandler.Object), _mockDateConverter.Object);

            _mockDateConverter.Setup(t => t.InputDayConvert(input)).Returns(new DateTime(2021, 11, 01));

            var result = underTest.SeeDayFinancialReport(input);

            Assert.Equal("\nEarnings for 01 November 2021: $2", result);
        }

        [Theory]
        [InlineData("Nov")]
        public void SeeDayFinancialReportTestIncorrectDate(string input)
        {
            var expectedResult = 2.0;
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
            var underTest = new CasinoViewModel(new HttpClient(mockMessageHandler.Object), _mockDateConverter.Object);

            _mockDateConverter.Setup(t => t.InputDayConvert(input)).Returns(DateTime.MinValue);

            var result = underTest.SeeDayFinancialReport(input);

            Assert.Equal("Incorrect date format.", result);
        }

        [Theory]
        [InlineData("Nov")]
        public void SeeDayFinancialReportTestFail(string input)
        {
            var expectedResult = 2.0;
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
            var underTest = new CasinoViewModel(new HttpClient(mockMessageHandler.Object), _mockDateConverter.Object);

            _mockDateConverter.Setup(t => t.InputDayConvert(input)).Returns(DateTime.MinValue);

            var result = underTest.SeeDayFinancialReport(input);

            Assert.Equal("Unexpected Error Occured.", result);
        }

        [Theory]
        [InlineData("Nov")]
        public void SeeMonthFinancialReportTestSuccess(string input)
        {
            IList<double> expectedResult = new List<double>(new double[32]);
            expectedResult[0] = 2.0;
            expectedResult[1] = 1.0;
            expectedResult[2] = 1.0;

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
            var underTest = new CasinoViewModel(new HttpClient(mockMessageHandler.Object), _mockDateConverter.Object);

            _mockDateConverter.Setup(t => t.InputMonthConvert(input)).Returns(new DateTime(2021, 11, 01));

            var result = underTest.SeeMonthFinancialReport(input);

            IList<string> expectedReturnList = new List<string>()
            {
                "\n",
                "1 November 2021: $1",
                "2 November 2021: $1",
                "\nEarnings for November 2021: $2"
            };

            Assert.Equal(expectedReturnList, result);
        }

        [Theory]
        [InlineData("Nov")]
        public void SeeMonthFinancialReportTestFail(string input)
        {
            IList<double> expectedResult = new List<double>(new double[32]);
            expectedResult[0] = 2.0;
            expectedResult[1] = 1.0;
            expectedResult[2] = 1.0;

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
            var underTest = new CasinoViewModel(new HttpClient(mockMessageHandler.Object), _mockDateConverter.Object);

            _mockDateConverter.Setup(t => t.InputMonthConvert(input)).Returns(new DateTime(2021, 11, 01));

            var result = underTest.SeeMonthFinancialReport(input);

            IList<string> expectedReturnList = new List<string>()
            {
                "\nUnexpected Error Occured."
            };

            Assert.Equal(expectedReturnList, result);
        }

        [Theory]
        [InlineData("Nov")]
        public void SeeMonthFinancialReportTestIncorrectDate(string input)
        {
            IList<double> expectedResult = new List<double>(new double[32]);
            expectedResult[0] = 2.0;
            expectedResult[1] = 1.0;
            expectedResult[2] = 1.0;

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
            var underTest = new CasinoViewModel(new HttpClient(mockMessageHandler.Object), _mockDateConverter.Object);

            _mockDateConverter.Setup(t => t.InputMonthConvert(input)).Returns(DateTime.MinValue);

            var result = underTest.SeeMonthFinancialReport(input);

            IList<string> expectedReturnList = new List<string>()
            {
                "\nIncorrect date format."
            };

            Assert.Equal(expectedReturnList, result);
        }

        [Theory]
        [InlineData("Nov")]
        public void SeeYearFinancialReportTestSuccess(string input)
        {
            IList<double> expectedResult = new List<double>(new double[13])
            {
                [0] = 2.0,
                [1] = 1.0,
                [2] = 1.0
            };

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
            var underTest = new CasinoViewModel(new HttpClient(mockMessageHandler.Object), _mockDateConverter.Object);

            _mockDateConverter.Setup(t => t.InputYearConvert(input)).Returns(new DateTime(2021, 11, 01));

            var result = underTest.SeeYearFinancialReport(input);

            IList<string> expectedReturnList = new List<string>()
            {
                "\n",
                "January 2021: $1",
                "February 2021: $1",
                "\nEarnings for 2021: $2"
            };

            Assert.Equal(expectedReturnList, result);
        }

        [Theory]
        [InlineData("Nov")]
        public void SeeYearFinancialReportTestFail(string input)
        {
            IList<double> expectedResult = new List<double>(new double[13])
            {
                [0] = 2.0,
                [1] = 1.0,
                [2] = 1.0
            };

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
            var underTest = new CasinoViewModel(new HttpClient(mockMessageHandler.Object), _mockDateConverter.Object);

            _mockDateConverter.Setup(t => t.InputYearConvert(input)).Returns(new DateTime(2021, 11, 01));

            var result = underTest.SeeYearFinancialReport(input);

            IList<string> expectedReturnList = new List<string>()
            {
                "\nUnexpected Error Occured."
            };

            Assert.Equal(expectedReturnList, result);
        }

        [Theory]
        [InlineData("Nov")]
        public void SeeYearFinancialReportTestIncorrectDate(string input)
        {
            IList<double> expectedResult = new List<double>(new double[13])
            {
                [0] = 2.0,
                [1] = 1.0,
                [2] = 1.0
            };

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
            var underTest = new CasinoViewModel(new HttpClient(mockMessageHandler.Object), _mockDateConverter.Object);

            _mockDateConverter.Setup(t => t.InputYearConvert(input)).Returns(DateTime.MinValue);

            var result = underTest.SeeYearFinancialReport(input);

            IList<string> expectedReturnList = new List<string>()
            {
                "\nIncorrect date format."
            };

            Assert.Equal(expectedReturnList, result);
        }
    }
}
