using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using OnlineCasinoProjectConsole;

namespace OnlineCasinoTesting
{
    public class DateConverterTest
    {


        //
        // Test day input convert
        //
        [Theory]
        [InlineData("21 October 2021")]
        [InlineData("21102021")]
        [InlineData("201021")]
        [InlineData("250111")]
        public void dayConverterTest(string input)
        {
            DateConverter dateConverterTest = new DateConverter();
            var res = dateConverterTest.inputDayConvert(input);
            Assert.NotEqual(DateTime.MinValue, res);
        }

        [Theory]
        [InlineData("abc")]
        [InlineData("")]
        [InlineData("1")]
        [InlineData("*(#$*(*a")]
        public void dayConverterTestFail(string input)
        {
            DateConverter dateConverterTest = new DateConverter();
            var res = dateConverterTest.inputDayConvert(input);
            Assert.Equal(DateTime.MinValue, res);
        }


        //
        // Test month input convert
        //
        [Theory]
        [InlineData("October 2021")]
        [InlineData("102021")]
        [InlineData("1021")]
        [InlineData("Oct 2021")]
        [InlineData("2110")]
        [InlineData("Oct 21")]
        [InlineData("21 Oct 21")]
        [InlineData("22 October 21")]
        public void monthConverterTest(string input)
        {
            DateConverter dateConverterTest = new DateConverter();
            var res = dateConverterTest.inputMonthConvert(input);
            Assert.NotEqual(DateTime.MinValue, res);
        }

        [Theory]
        [InlineData("abc")]
        [InlineData("")]
        [InlineData("1")]
        [InlineData("*(#$*(*a")]
        public void monthConverterTestFail(string input)
        {
            DateConverter dateConverterTest = new DateConverter();
            var res = dateConverterTest.inputMonthConvert(input);
            Assert.Equal(DateTime.MinValue, res);
        }


        //
        // Test year input convert
        //
        [Theory]
        [InlineData("2021")]
        [InlineData("21")]
        [InlineData("Oct 2021")]
        [InlineData("2110")]
        [InlineData("Oct 21")]
        public void yearConverterTest(string input)
        {
            DateConverter dateConverterTest = new DateConverter();
            var res = dateConverterTest.inputYearConvert(input);
            Assert.NotEqual(DateTime.MinValue, res);
        }

        [Theory]
        [InlineData("abc")]
        [InlineData("")]
        [InlineData("1")]
        [InlineData("*(#$*(*a")]
        public void yearConverterTestFail(string input)
        {
            DateConverter dateConverterTest = new DateConverter();
            var res = dateConverterTest.inputYearConvert(input);
            Assert.Equal(DateTime.MinValue, res);
        }

    }
}
