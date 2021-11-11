using OnlineCasinoProjectConsole;
using Xunit;

namespace OnlineCasinoTesting
{
    public class CustomRandomTesting
    {
        CustomRandom customRandomTest;
        public CustomRandomTesting()
        {
            customRandomTest = new CustomRandom();
        }


        [Theory]
        [InlineData(48, 57)]
        [InlineData(0, 1)]
        public void randomInt1Test(int min, int max)
        {
            var res = customRandomTest.randomInt1(min, max);
            Assert.InRange<int>(res, min, max);
        }

        [Theory]
        [InlineData(48, 57)]
        [InlineData(0, 1)]
        public void randomInt2Test(int min, int max)
        {
            var res = customRandomTest.randomInt2(min, max);
            Assert.InRange<int>(res, min, max);
        }

        [Theory]
        [InlineData(48, 57)]
        [InlineData(0, 1)]
        public void randomInt3Test(int min, int max)
        {
            var res = customRandomTest.randomInt3(min, max);
            Assert.InRange<int>(res, min, max);
        }

        [Theory]
        [InlineData(48)]
        [InlineData(0)]
        public void randomIntMaxTest(int max)
        {
            var res = customRandomTest.randomIntMax(max);
            Assert.InRange<int>(res, 0, max);
        }
    }
}
