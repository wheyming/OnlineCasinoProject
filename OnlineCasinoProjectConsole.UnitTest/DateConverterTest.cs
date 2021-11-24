using OnlineCasinoProjectConsole.Utility;
using System;
using Xunit;

namespace OnlineCasinoProjectConsole.UnitTest
{
    public class DateConverterTest
    {
        [Theory]
        [InlineData("21 October 2021")]
        [InlineData("21102021")]
        [InlineData("201021")]
        [InlineData("250111")]
        public void DayConverterTest(string input)
        {
            DateConverter dateConverterTest = new DateConverter();
            var res = dateConverterTest.InputDayConvert(input);
            Assert.NotEqual(DateTime.MinValue, res);
        }

        [Theory]
        [InlineData("abc")]
        [InlineData("")]
        [InlineData("1")]
        [InlineData("*(#$*(*a")]
        public void DayConverterTestFail(string input)
        {
            DateConverter dateConverterTest = new DateConverter();
            var res = dateConverterTest.InputDayConvert(input);
            Assert.Equal(DateTime.MinValue, res);
        }


        [Theory]
        [InlineData("October 2021")]
        [InlineData("102021")]
        [InlineData("1021")]
        [InlineData("Oct 2021")]
        [InlineData("2110")]
        [InlineData("Oct 21")]
        [InlineData("21 Oct 21")]
        [InlineData("22 October 21")]
        public void MonthConverterTest(string input)
        {
            DateConverter dateConverterTest = new DateConverter();
            var res = dateConverterTest.InputMonthConvert(input);
            Assert.NotEqual(DateTime.MinValue, res);
        }

        [Theory]
        [InlineData("abc")]
        [InlineData("")]
        [InlineData("1")]
        [InlineData("*(#$*(*a")]
        public void MonthConverterTestFail(string input)
        {
            DateConverter dateConverterTest = new DateConverter();
            var res = dateConverterTest.InputMonthConvert(input);
            Assert.Equal(DateTime.MinValue, res);
        }

        [Theory]
        [InlineData("2021")]
        [InlineData("21")]
        [InlineData("Oct 2021")]
        [InlineData("2110")]
        [InlineData("Oct 21")]
        public void YearConverterTest(string input)
        {
            DateConverter dateConverterTest = new DateConverter();
            var res = dateConverterTest.InputYearConvert(input);
            Assert.NotEqual(DateTime.MinValue, res);
        }

        [Theory]
        [InlineData("abc")]
        [InlineData("")]
        [InlineData("1")]
        [InlineData("*(#$*(*a")]
        public void YearConverterTestFail(string input)
        {
            DateConverter dateConverterTest = new DateConverter();
            var res = dateConverterTest.InputYearConvert(input);
            Assert.Equal(DateTime.MinValue, res);
        }

    }
}
