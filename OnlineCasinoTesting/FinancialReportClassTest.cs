using Moq;
using OnlineCasinoProjectConsole;
using System;
using System.IO;
using Xunit;

namespace OnlineCasinoTesting
{
    public class FinancialReportClassTest
    {
        private Mock<IFileHandling> _mockFileHandling;

        public FinancialReportClassTest()
        {
            _mockFileHandling = new Mock<IFileHandling>(MockBehavior.Strict);
        }

        //
        // ReportDayTests
        //

        [Theory]
        [InlineData("20 October 2021")]
        public void generateFinancialReportDayTest(DateTime date)
        {
            string fileName = "FinancialReport\\" + date.ToString("yyMM") + "\\" + date.ToString("yyyyMMdd") + ".json";
            string returnedStr = "[{ \"betAmount\":123.0,\"payout\":0.0},{ \"betAmount\":32.0,\"payout\":0.0}]";
            _mockFileHandling.SetupAllProperties();
            _mockFileHandling.Setup(t => t.readAllText(fileName)).Returns(returnedStr);
            FinancialReport financialReportTest = new FinancialReport(_mockFileHandling.Object);
            var res = financialReportTest.generateFinancialReportDay(date);
            Assert.Equal(155.0, res);
        }
        [Theory]
        [InlineData("20 October 2021")]
        public void generateFinancialReportDayExceptionTest(DateTime date)
        {
            string fileName = "FinancialReport\\" + date.ToString("yyMM") + "\\" + date.ToString("yyyyMMdd") + ".json";
            string returnedStr = "[{ \"betAmount\":123.0,\"payout\":0.0},{ \"betAmount\":32.0,\"payout\":0.0}]";
            _mockFileHandling.SetupAllProperties();
            IOException IO = new IOException();
            _mockFileHandling.Setup(t => t.readAllText(fileName)).Returns(returnedStr);
            _mockFileHandling.Setup(t => t.readAllText(fileName)).Throws(IO);
            FinancialReport financialReportTest = new FinancialReport(_mockFileHandling.Object);
            var res = financialReportTest.generateFinancialReportDay(date);
            Assert.Equal(0.0, res);
        }


        //
        // ReportMonthTests
        //

        [Theory]
        [InlineData("20 October 2021")]
        public void generateFinancialReportMonthTest(DateTime date)
        {
            string DirectoryName = "FinancialReport\\" + date.ToString("yyMM");
            string[] returnedfileNames = { "FinancialReport\\" + date.ToString("yyMM") + "\\" + date.ToString("yyyyMMdd") + ".json" };
            string strReturnedfileNames = "FinancialReport\\" + date.ToString("yyMM") + "\\" + date.ToString("yyyyMMdd") + ".json";
            string returnedStr = "[{ \"betAmount\":123.0,\"payout\":0.0},{ \"betAmount\":32.0,\"payout\":0.0}]";
            _mockFileHandling.Setup(t => t.directoryGetFiles(DirectoryName)).Returns(returnedfileNames);
            _mockFileHandling.Setup(t => t.readAllText(strReturnedfileNames)).Returns(returnedStr);
            FinancialReport financialReportTest = new FinancialReport(_mockFileHandling.Object);
            var res = financialReportTest.generateFinancialReportMonth(date);
            Assert.Equal(155.0, res);
        }

        [Theory]
        [InlineData("20 October 2021")]
        public void generateFinancialReportMonthExceptionTest(DateTime date)
        {
            string DirectoryName = "FinancialReport\\" + date.ToString("yyMM");
            string[] returnedfileNames = { "FinancialReport\\" + date.ToString("yyMM") + "\\" + date.ToString("yyyyMMdd") + ".json" };
            string strReturnedfileNames = "FinancialReport\\" + date.ToString("yyMM") + "\\" + date.ToString("yyyyMMdd") + ".json";
            string returnedStr = "[{ \"betAmount\":123.0,\"payout\":0.0},{ \"betAmount\":32.0,\"payout\":0.0}]";
            _mockFileHandling.Setup(t => t.directoryGetFiles(DirectoryName)).Returns(returnedfileNames);
            _mockFileHandling.Setup(t => t.readAllText(strReturnedfileNames)).Returns(returnedStr);
            IOException IO = new IOException();
            _mockFileHandling.Setup(t => t.readAllText(strReturnedfileNames)).Throws(IO).Verifiable();
            FinancialReport financialReportTest = new FinancialReport(_mockFileHandling.Object);
            var res = financialReportTest.generateFinancialReportMonth(date);
            Assert.Equal(0.0, res);
            _mockFileHandling.Verify();
        }

        //
        // ReportYearTests
        //

        [Theory]
        [InlineData("20 October 2021")]
        public void generateFinancialReportYearTest(DateTime date)
        {
            string[] returnedfileNames = { "FinancialReport\\" + date.ToString("yyMM") + "\\" + date.ToString("yyyyMMdd") + ".json" };
            string strReturnedfileNames = "FinancialReport\\" + date.ToString("yyMM") + "\\" + date.ToString("yyyyMMdd") + ".json";
            string returnedStr = "[{ \"betAmount\":123.0,\"payout\":0.0},{ \"betAmount\":32.0,\"payout\":0.0}]";
            for (int i = 0; i < 13; i++)
            {
                if (i != 10)
                {
                    _mockFileHandling.Setup(t => t.directoryExists("FinancialReport\\" + date.ToString("yy") + i.ToString("00"))).Returns(false);
                    continue;
                }
                else
                {
                    _mockFileHandling.Setup(t => t.directoryExists("FinancialReport\\" + date.ToString("yy") + i.ToString("00"))).Returns(true);
                    string DirectoryName = "FinancialReport\\" + date.ToString("yy") + i.ToString("00");
                    _mockFileHandling.Setup(t => t.directoryGetFiles(DirectoryName)).Returns(returnedfileNames);
                    _mockFileHandling.Setup(t => t.readAllText(strReturnedfileNames)).Returns(returnedStr);
                }
            }
            //_mockFileHandling.Setup(t => t.directoryGetFiles(DirectoryName)).Returns(returnedfileNames);
            //_mockFileHandling.Setup(t => t.readAllText(strReturnedfileNames)).Returns(returnedStr);
            FinancialReport financialReportTest = new FinancialReport(_mockFileHandling.Object);
            var res = financialReportTest.generateFinancialReportYear(date);
            Assert.Equal(155.0, res);
        }

        [Theory]
        [InlineData("20 October 2021")]
        public void generateFinancialReportYearNullTest(DateTime date)
        {
            string[] returnedfileNames = { "FinancialReport\\" + date.ToString("yyMM") + "\\" + date.ToString("yyyyMMdd") + ".json" };
            string strReturnedfileNames = "FinancialReport\\" + date.ToString("yyMM") + "\\" + date.ToString("yyyyMMdd") + ".json";
            string returnedStr = "[{ \"betAmount\":123.0,\"payout\":0.0},{ \"betAmount\":32.0,\"payout\":0.0}]";
            for (int i = 0; i < 13; i++)
            {

                _mockFileHandling.Setup(t => t.directoryExists("FinancialReport\\" + date.ToString("yy") + i.ToString("00"))).Returns(false);
                string DirectoryName = "FinancialReport\\" + date.ToString("yy") + i.ToString("00");
                _mockFileHandling.Setup(t => t.directoryGetFiles(DirectoryName)).Returns(returnedfileNames);
                _mockFileHandling.Setup(t => t.readAllText(strReturnedfileNames)).Returns(returnedStr);
            }
            //_mockFileHandling.Setup(t => t.directoryGetFiles(DirectoryName)).Returns(returnedfileNames);
            //_mockFileHandling.Setup(t => t.readAllText(strReturnedfileNames)).Returns(returnedStr);
            IOException IO = new IOException();
            _mockFileHandling.Setup(t => t.readAllText(strReturnedfileNames)).Throws(IO);
            FinancialReport financialReportTest = new FinancialReport(_mockFileHandling.Object);
            var res = financialReportTest.generateFinancialReportYear(date);
            Assert.Equal(0.0, res);
        }




        [Theory]
        [InlineData("20 October 2021")]
        public void generateFinancialReportYearExceptionTest(DateTime date)
        {
            string[] returnedfileNames = { "FinancialReport\\" + date.ToString("yyMM") + "\\" + date.ToString("yyyyMMdd") + ".json" };
            string strReturnedfileNames = "FinancialReport\\" + date.ToString("yyMM") + "\\" + date.ToString("yyyyMMdd") + ".json";
            string returnedStr = "[{ \"betAmount\":123.0,\"payout\":0.0},{ \"betAmount\":32.0,\"payout\":0.0}]";
            for (int i = 0; i < 13; i++)
            {
                if (i != 10)
                {
                    _mockFileHandling.Setup(t => t.directoryExists("FinancialReport\\" + date.ToString("yy") + i.ToString("00"))).Returns(false);
                    continue;
                }
                else
                {
                    _mockFileHandling.Setup(t => t.directoryExists("FinancialReport\\" + date.ToString("yy") + i.ToString("00"))).Returns(true);
                    string DirectoryName = "FinancialReport\\" + date.ToString("yy") + i.ToString("00");
                    _mockFileHandling.Setup(t => t.directoryGetFiles(DirectoryName)).Returns(returnedfileNames);
                    _mockFileHandling.Setup(t => t.readAllText(strReturnedfileNames)).Returns(returnedStr);
                }
            }
            //_mockFileHandling.Setup(t => t.directoryGetFiles(DirectoryName)).Returns(returnedfileNames);
            //_mockFileHandling.Setup(t => t.readAllText(strReturnedfileNames)).Returns(returnedStr);
            IOException IO = new IOException();
            _mockFileHandling.Setup(t => t.readAllText(strReturnedfileNames)).Throws(IO);
            FinancialReport financialReportTest = new FinancialReport(_mockFileHandling.Object);
            var res = financialReportTest.generateFinancialReportYear(date);
            Assert.Equal(0.0, res);
        }
    }
}
