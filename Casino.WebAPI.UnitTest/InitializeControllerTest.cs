using Casino.WebAPI.Controllers;
using Casino.WebAPI.Interfaces;
using Xunit;

namespace Casino.WebAPI.UnitTest
{
    public class InitializeControllerTest
    {
        private readonly IInitializeManager _initializeController;

        public InitializeControllerTest()
        {
            _initializeController = new InitializeController();
        }

        [Fact]
        public void InitializeTest()
        {
            _initializeController.Initialize();
        }
    }
}
