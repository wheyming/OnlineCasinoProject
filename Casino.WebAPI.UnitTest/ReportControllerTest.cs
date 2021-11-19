using Casino.WebAPI.Controllers;
using Casino.WebAPI.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Xunit;

namespace Casino.WebAPI.UnitTest
{
    public class ReportControllerTest
    {
        private readonly Mock<ICasinoContext> _mockCasinoContext;

        public ReportControllerTest()
        {
            _mockCasinoContext = new Mock<ICasinoContext>(MockBehavior.Strict);
        }

        [Theory]
        [InlineData("01 November 2021")]
        public void getFinancialReportMonthTest(DateTime monthYear)
        {
            var userList = new List<Models.Report>
            {
                new Models.Report( 2.0, 0, new DateTime(2021, 11, 01)),
                new Models.Report( 2.0, 0, new DateTime(2021, 11, 02)),
                new Models.Report( 2.0, 6.0, new DateTime(2021, 11, 03))
            }.AsQueryable();

            var mockReportDbSet = new Mock<DbSet<Models.Report>>();
            mockReportDbSet.As<IQueryable<Models.Report>>().Setup(t => t.Provider).Returns(userList.Provider);
            mockReportDbSet.As<IQueryable<Models.Report>>().Setup(t => t.Expression).Returns(userList.Expression);
            mockReportDbSet.As<IQueryable<Models.Report>>().Setup(t => t.ElementType).Returns(userList.ElementType);
            mockReportDbSet.As<IQueryable<Models.Report>>().Setup(t => t.GetEnumerator()).Returns(userList.GetEnumerator());

            _mockCasinoContext.Setup(t => t.Reports).Returns(mockReportDbSet.Object);

            var reportController = new ReportController(_mockCasinoContext.Object);
            var financialReportMonthResult = reportController.GenerateFinancialReportMonth(monthYear);

            IList<double> expectedResult = new List<double>(new double[32]);
            expectedResult[1] += 2.0;
            expectedResult[2] += 2.0;
            expectedResult[3] += -4.0;

            Assert.Equal(expectedResult, financialReportMonthResult);
        }

        [Theory]
        [InlineData("01 November 2021")]
        public void getFinancialReportYearTest(DateTime Year)
        {
            var reportList = new List<Models.Report>
            {
                new Models.Report( 2.0, 0, new DateTime(2021, 01, 01)),
                new Models.Report( 2.0, 0, new DateTime(2021, 02, 02)),
                new Models.Report( 2.0, 6.0, new DateTime(2021, 03, 03))
            }.AsQueryable();

            var mockReportDbSet = new Mock<DbSet<Models.Report>>();
            mockReportDbSet.As<IQueryable<Models.Report>>().Setup(t => t.Provider).Returns(reportList.Provider);
            mockReportDbSet.As<IQueryable<Models.Report>>().Setup(t => t.Expression).Returns(reportList.Expression);
            mockReportDbSet.As<IQueryable<Models.Report>>().Setup(t => t.ElementType).Returns(reportList.ElementType);
            mockReportDbSet.As<IQueryable<Models.Report>>().Setup(t => t.GetEnumerator()).Returns(reportList.GetEnumerator());

            _mockCasinoContext.Setup(t => t.Reports).Returns(mockReportDbSet.Object);

            var reportController = new ReportController(_mockCasinoContext.Object);
            var financialReportYearResult = reportController.GenerateFinancialReportYear(Year);

            IList<double> expectedResult = new List<double>(new double[13]);
            expectedResult[1] += 2.0;
            expectedResult[2] += 2.0;
            expectedResult[3] += -4.0;

            Assert.Equal(expectedResult, financialReportYearResult);
        }

        [Theory]
        [InlineData("01 November 2021")]
        public void getFinancialReportDayTest(DateTime date)
        {
            var reportList = new List<Models.Report>
            {
                new Models.Report( 2.0, 0, new DateTime(2021, 11, 01)),
                new Models.Report( 2.0, 0, new DateTime(2021, 11, 01)),
                new Models.Report( 2.0, 14.0, new DateTime(2021, 11, 01))
            }.AsQueryable();

            var mockReportDbSet = new Mock<DbSet<Models.Report>>();
            mockReportDbSet.As<IQueryable<Models.Report>>().Setup(t => t.Provider).Returns(reportList.Provider);
            mockReportDbSet.As<IQueryable<Models.Report>>().Setup(t => t.Expression).Returns(reportList.Expression);
            mockReportDbSet.As<IQueryable<Models.Report>>().Setup(t => t.ElementType).Returns(reportList.ElementType);
            mockReportDbSet.As<IQueryable<Models.Report>>().Setup(t => t.GetEnumerator()).Returns(reportList.GetEnumerator());

            _mockCasinoContext.Setup(t => t.Reports).Returns(mockReportDbSet.Object);

            var fakeController = new ReportController();

            var reportController = new ReportController(_mockCasinoContext.Object);
            var financialReportDayResult = reportController.GenerateFinancialReportDay(date);

            Assert.Equal(-8.0, financialReportDayResult);
        }
    }
}
