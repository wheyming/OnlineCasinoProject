using Casino.WebAPI.Controllers;
using Casino.WebAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Casino.WebAPI.UnitTest
{
    public class InitializeControllerTest
    {
        private IInitializeManager _initializeController;

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
