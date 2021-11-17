using Casino.WebAPI.Controllers;
using Casino.WebAPI.Interfaces;
using Casino.WebAPI.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Casino.WebAPI.UnitTest
{
    public class UtilityClassesTest
    {
        private IRandomNumberGenerator _randomNumberGenerator;
        private IDateTimeGenerator _datetimeGenerator;
        public UtilityClassesTest()
        {
            _randomNumberGenerator = new RandomNumberGenerator();
            _datetimeGenerator = new DateTimeGenerator();
        }

        [Fact]
        public void RollRandomNumberPrizeActivatedTest()
        {
            var result = _randomNumberGenerator.RollRandomNumberPrizeActivated();
            IList<int> minimum = new List<int> { 0, 0, 0 };
            IList<int> maximum = new List<int> { 9, 9, 9 };
            Assert.InRange(result[0], minimum[0], maximum[0]);
            Assert.InRange(result[1], minimum[1], maximum[1]);
            Assert.InRange(result[2], minimum[2], maximum[2]);
        }

        [Fact]
        public void RollRandomNumberPrizeNotActivatedTest()
        {
            var result = _randomNumberGenerator.RollRandomNumberPrizeNotActivated();
            IList<int> minimum = new List<int> { 0, 0, 0 };
            IList<int> maximum = new List<int> { 9, 9, 9 };
            Assert.InRange(result[0], minimum[0], maximum[0]);
            Assert.InRange(result[1], minimum[1], maximum[1]);
            Assert.InRange(result[2], minimum[2], maximum[2]);
        }

        [Fact]
        public void DateTimeGeneratorTest()
        {
            var result = _datetimeGenerator.Now();
            Assert.Equal(DateTime.Now, result);
        }
    }
}
