using Casino.WebAPI.Controllers;
using Casino.WebAPI.Interfaces;
using Moq;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Xunit;

namespace Casino.WebAPI.UnitTest
{
    public class ConfigurationControllerTest
    {
        private readonly Mock<ICasinoContext> _mockCasinoContext;

        public ConfigurationControllerTest()
        {
            _mockCasinoContext = new Mock<ICasinoContext>(MockBehavior.Strict);
        }

        [Theory]
        [InlineData(1)]
        public void SetPrizeModuleTestActivate(int input)
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

            _mockCasinoContext.Setup(t => t.PrizeModule).Returns(mockPrizeModuleDbSet.Object);
            _mockCasinoContext.Setup(t => t.SaveChanges()).Returns(1);

            var configurationController = new ConfigurationController(_mockCasinoContext.Object);

            var configurationString = configurationController.SetPrizeModuleStatus(input);

            Assert.Equal("PrizeGivingModule has been activated.", configurationString);
        }

        [Theory]
        [InlineData(2)]
        public void SetPrizeModuleTestDeactivate(int input)
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

            _mockCasinoContext.Setup(t => t.PrizeModule).Returns(mockPrizeModuleDbSet.Object);
            _mockCasinoContext.Setup(t => t.SaveChanges()).Returns(1);

            var configurationController = new ConfigurationController(_mockCasinoContext.Object);

            var configurationString = configurationController.SetPrizeModuleStatus(input);

            Assert.Equal("PrizeGivingModule has been deactivated.", configurationString);
        }

        [Theory]
        [InlineData(3)]
        public void SetPrizeModuleTestInvalid(int input)
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

            _mockCasinoContext.Setup(t => t.PrizeModule).Returns(mockPrizeModuleDbSet.Object);
            _mockCasinoContext.Setup(t => t.SaveChanges()).Returns(1);

            var configurationController = new ConfigurationController();

            var configurationString = configurationController.SetPrizeModuleStatus(input);

            Assert.Equal("Invalid input.", configurationString);
        }
    }
}
