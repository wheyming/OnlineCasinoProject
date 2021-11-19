using Casino.Common;
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
    public class GamblingControllerTest
    {
        private readonly Mock<ICasinoContext> _mockCasinoContext;
        private readonly Mock<IRandomNumberGenerator> _mockCustomRandom;
        private readonly Mock<IDateTimeGenerator> _mockDateTimeGenerator;

        public GamblingControllerTest()
        {
            _mockCasinoContext = new Mock<ICasinoContext>(MockBehavior.Strict);
            _mockCustomRandom = new Mock<IRandomNumberGenerator>(MockBehavior.Strict);
            _mockDateTimeGenerator = new Mock<IDateTimeGenerator>(MockBehavior.Strict);
        }

        [Theory]
        [InlineData(2.0)]
        public void playSlotTestNone(double betAmount)
        {
            var prizeModuleList = new List<Models.PrizeModule>
            {
                new Models.PrizeModule(true)
            }.AsQueryable();

            var mockPrizeModuleDbSet = new Mock<DbSet<Models.PrizeModule>>();
            mockPrizeModuleDbSet.As<IQueryable<Models.PrizeModule>>().Setup(t => t.Provider).Returns(prizeModuleList.Provider);
            mockPrizeModuleDbSet.As<IQueryable<Models.PrizeModule>>().Setup(t => t.Expression).Returns(prizeModuleList.Expression);
            mockPrizeModuleDbSet.As<IQueryable<Models.PrizeModule>>().Setup(t => t.ElementType).Returns(prizeModuleList.ElementType);
            mockPrizeModuleDbSet.As<IQueryable<Models.PrizeModule>>().Setup(t => t.GetEnumerator()).Returns(prizeModuleList.GetEnumerator());

            var mockReportDbSet = new Mock<DbSet<Models.Report>>();
            _mockCasinoContext.Setup(t => t.SaveChanges()).Returns(1);
            _mockCasinoContext.Setup(t => t.PrizeModule).Returns(mockPrizeModuleDbSet.Object);
            _mockCasinoContext.Setup(t => t.Reports).Returns(mockReportDbSet.Object);

            IList<int> rolledNumber = new List<int> { 1, 2, 3 };

            _mockCustomRandom.Setup(t => t.RollRandomNumberPrizeActivated()).Returns(new List<int> { 1, 2, 3 });
            _mockDateTimeGenerator.Setup(t => t.Now()).Returns(new DateTime(2021, 1, 1));

            var fakeController = new GamblingController();

            var gamblingController = new GamblingController(_mockCasinoContext.Object, _mockCustomRandom.Object, _mockDateTimeGenerator.Object);
            var playSlotResult = gamblingController.PlaySlot(betAmount);

            mockReportDbSet.Verify(t => t.Add(It.IsAny<Models.Report>()), Times.Once());
            _mockCasinoContext.Verify(t => t.SaveChanges(), Times.Once());

            Assert.Equal(rolledNumber, playSlotResult.Item1);
            Assert.Equal((0, SlotsResultType.None), (playSlotResult.Item2, playSlotResult.Item3));
        }

        [Theory]
        [InlineData(2.0)]
        public void playSlotTestDouble(double betAmount)
        {
            var prizeModuleList = new List<Models.PrizeModule>
            {
                new Models.PrizeModule(true)
            }.AsQueryable();

            var mockPrizeModuleDbSet = new Mock<DbSet<Models.PrizeModule>>();
            mockPrizeModuleDbSet.As<IQueryable<Models.PrizeModule>>().Setup(t => t.Provider).Returns(prizeModuleList.Provider);
            mockPrizeModuleDbSet.As<IQueryable<Models.PrizeModule>>().Setup(t => t.Expression).Returns(prizeModuleList.Expression);
            mockPrizeModuleDbSet.As<IQueryable<Models.PrizeModule>>().Setup(t => t.ElementType).Returns(prizeModuleList.ElementType);
            mockPrizeModuleDbSet.As<IQueryable<Models.PrizeModule>>().Setup(t => t.GetEnumerator()).Returns(prizeModuleList.GetEnumerator());

            var mockReportDbSet = new Mock<DbSet<Models.Report>>();
            _mockCasinoContext.Setup(t => t.SaveChanges()).Returns(1);
            _mockCasinoContext.Setup(t => t.PrizeModule).Returns(mockPrizeModuleDbSet.Object);
            _mockCasinoContext.Setup(t => t.Reports).Returns(mockReportDbSet.Object);

            IList<int> rolledNumber = new List<int> { 1, 1, 3 };

            _mockCustomRandom.Setup(t => t.RollRandomNumberPrizeActivated()).Returns(new List<int> { 1, 1, 3 });
            _mockDateTimeGenerator.Setup(t => t.Now()).Returns(new DateTime(2021, 1, 1));

            var gamblingController = new GamblingController(_mockCasinoContext.Object, _mockCustomRandom.Object, _mockDateTimeGenerator.Object);
            var playSlotResult = gamblingController.PlaySlot(betAmount);

            mockReportDbSet.Verify(t => t.Add(It.IsAny<Models.Report>()), Times.Once());
            _mockCasinoContext.Verify(t => t.SaveChanges(), Times.Once());

            Assert.Equal(rolledNumber, playSlotResult.Item1);
            Assert.Equal((4.0, SlotsResultType.Double), (playSlotResult.Item2, playSlotResult.Item3));
        }

        [Theory]
        [InlineData(2.0)]
        public void playSlotTestTriple(double betAmount)
        {
            var prizeModuleList = new List<Models.PrizeModule>
            {
                new Models.PrizeModule(false)
            }.AsQueryable();

            var mockPrizeModuleDbSet = new Mock<DbSet<Models.PrizeModule>>();
            mockPrizeModuleDbSet.As<IQueryable<Models.PrizeModule>>().Setup(t => t.Provider).Returns(prizeModuleList.Provider);
            mockPrizeModuleDbSet.As<IQueryable<Models.PrizeModule>>().Setup(t => t.Expression).Returns(prizeModuleList.Expression);
            mockPrizeModuleDbSet.As<IQueryable<Models.PrizeModule>>().Setup(t => t.ElementType).Returns(prizeModuleList.ElementType);
            mockPrizeModuleDbSet.As<IQueryable<Models.PrizeModule>>().Setup(t => t.GetEnumerator()).Returns(prizeModuleList.GetEnumerator());

            var mockReportDbSet = new Mock<DbSet<Models.Report>>();
            _mockCasinoContext.Setup(t => t.SaveChanges()).Returns(1);
            _mockCasinoContext.Setup(t => t.PrizeModule).Returns(mockPrizeModuleDbSet.Object);
            _mockCasinoContext.Setup(t => t.Reports).Returns(mockReportDbSet.Object);

            IList<int> rolledNumber = new List<int> { 1, 1, 1 };

            _mockCustomRandom.Setup(t => t.RollRandomNumberPrizeNotActivated()).Returns(new List<int> { 1, 1, 1 });
            _mockDateTimeGenerator.Setup(t => t.Now()).Returns(new DateTime(2021, 1, 1));

            var gamblingController = new GamblingController(_mockCasinoContext.Object, _mockCustomRandom.Object, _mockDateTimeGenerator.Object);
            var playSlotResult = gamblingController.PlaySlot(betAmount);

            mockReportDbSet.Verify(t => t.Add(It.IsAny<Models.Report>()), Times.Once());
            _mockCasinoContext.Verify(t => t.SaveChanges(), Times.Once());

            Assert.Equal(rolledNumber, playSlotResult.Item1);
            Assert.Equal((6.0, SlotsResultType.Triple), (playSlotResult.Item2, playSlotResult.Item3));
        }

        [Theory]
        [InlineData(2.0)]
        public void playSlotTestJackPot(double betAmount)
        {
            var prizeModuleList = new List<Models.PrizeModule>
            {
                new Models.PrizeModule(true)
            }.AsQueryable();

            var mockPrizeModuleDbSet = new Mock<DbSet<Models.PrizeModule>>();
            mockPrizeModuleDbSet.As<IQueryable<Models.PrizeModule>>().Setup(t => t.Provider).Returns(prizeModuleList.Provider);
            mockPrizeModuleDbSet.As<IQueryable<Models.PrizeModule>>().Setup(t => t.Expression).Returns(prizeModuleList.Expression);
            mockPrizeModuleDbSet.As<IQueryable<Models.PrizeModule>>().Setup(t => t.ElementType).Returns(prizeModuleList.ElementType);
            mockPrizeModuleDbSet.As<IQueryable<Models.PrizeModule>>().Setup(t => t.GetEnumerator()).Returns(prizeModuleList.GetEnumerator());

            var mockReportDbSet = new Mock<DbSet<Models.Report>>();
            _mockCasinoContext.Setup(t => t.SaveChanges()).Returns(1);
            _mockCasinoContext.Setup(t => t.PrizeModule).Returns(mockPrizeModuleDbSet.Object);
            _mockCasinoContext.Setup(t => t.Reports).Returns(mockReportDbSet.Object);

            IList<int> rolledNumber = new List<int> { 7, 7, 7 };

            _mockCustomRandom.Setup(t => t.RollRandomNumberPrizeActivated()).Returns(new List<int> { 7, 7, 7 });
            _mockDateTimeGenerator.Setup(t => t.Now()).Returns(new DateTime(2021, 1, 1));

            var gamblingController = new GamblingController(_mockCasinoContext.Object, _mockCustomRandom.Object, _mockDateTimeGenerator.Object);
            var playSlotResult = gamblingController.PlaySlot(betAmount);

            mockReportDbSet.Verify(t => t.Add(It.IsAny<Models.Report>()), Times.Once());
            _mockCasinoContext.Verify(t => t.SaveChanges(), Times.Once());

            Assert.Equal(rolledNumber, playSlotResult.Item1);
            Assert.Equal((14.0, SlotsResultType.JackPot), (playSlotResult.Item2, playSlotResult.Item3));
        }
    }
}
