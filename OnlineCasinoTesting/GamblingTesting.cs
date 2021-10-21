using Moq;
using OnlineCasinoProjectConsole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace OnlineCasinoTesting
{
    public class GamblingTesting
    {
        private Mock<IFileHandling> _mockFileHandling;
        private Mock<ICustomRandom> _mockCustomRandom;

        public GamblingTesting()
        {
            _mockFileHandling = new Mock<IFileHandling>(MockBehavior.Strict);
            _mockCustomRandom = new Mock<ICustomRandom>(MockBehavior.Strict);
        }

        [Theory]
        [InlineData(2.0, "1", false)]
        public void playSlotTestDouble(double bet, string username, bool prizeModuleBool)
        {
            string gamblerAmountList = "[{\"betAmount\":2.0,\"payout\":4.0},{\"betAmount\":2.0,\"payout\":4.0}]";
            string fRAmountlList = "[{\"betAmount\":2.0,\"payout\":4.0},{\"betAmount\":2.0,\"payout\":4.0}]";
            string fileSimulation1 = "Users\\" + username + "\\" + DateTime.Now.ToString("yyMM");
            string fileSimulation2 = "FinancialReport\\" + DateTime.Now.ToString("yyMM");
            string fileSimulation3 = "Users\\" + username + "\\" + DateTime.Now.ToString("yyMM") + "\\" + DateTime.Now.ToString("yyyyMMdd") + ".json";
            string fileSimulation4 = "FinancialReport\\" + DateTime.Now.ToString("yyMM") + "\\" + DateTime.Now.ToString("yyyyMMdd") + ".json";
            _mockFileHandling.SetupAllProperties();
            _mockFileHandling.Setup(t => t.directoryExists(fileSimulation1)).Returns(true);
            _mockFileHandling.Setup(t => t.createDirectory(fileSimulation1));
            _mockFileHandling.Setup(t => t.fileExists(fileSimulation3)).Returns(true);
            _mockFileHandling.Setup(t => t.directoryExists(fileSimulation2)).Returns(true);
            _mockFileHandling.Setup(t => t.createDirectory(fileSimulation2));
            _mockFileHandling.Setup(t => t.fileExists(fileSimulation4)).Returns(true);
            _mockCustomRandom.Setup(t => t.randomInt1(48, 57)).Returns(55);
            _mockCustomRandom.Setup(t => t.randomInt2(48, 57)).Returns(55);
            _mockCustomRandom.Setup(t => t.randomInt3(48, 57)).Returns(55);
            _mockCustomRandom.Setup(t => t.randomIntMax(9)).Returns(1);
            _mockFileHandling.Setup(t => t.readAllText(fileSimulation3)).Returns("[{\"betAmount\":2.0,\"payout\":4.0}]");
            _mockFileHandling.Setup(t => t.readAllText(fileSimulation4)).Returns("[{\"betAmount\":2.0,\"payout\":4.0}]");
            _mockFileHandling.Setup(t => t.writeAllText(fileSimulation3, gamblerAmountList));
            _mockFileHandling.Setup(t => t.writeAllText(fileSimulation4, fRAmountlList));

            Gambling gambling = new Gambling(_mockFileHandling.Object, _mockCustomRandom.Object);
            var res = gambling.playSlot(bet, username, prizeModuleBool);
            Assert.Equal("771", res);
        }


        [Theory]
        [InlineData(2.0, "1", false)]
        public void playSlotTestTriple(double bet, string username, bool prizeModuleBool)
        {
            string gamblerAmountList = "[{\"betAmount\":2.0,\"payout\":6.0}]";
            string fRAmountlList = "[{\"betAmount\":2.0,\"payout\":6.0}]";
            string fileSimulation1 = "Users\\" + username + "\\" + DateTime.Now.ToString("yyMM");
            string fileSimulation2 = "FinancialReport\\" + DateTime.Now.ToString("yyMM");
            string fileSimulation3 = "Users\\" + username + "\\" + DateTime.Now.ToString("yyMM") + "\\" + DateTime.Now.ToString("yyyyMMdd") + ".json";
            string fileSimulation4 = "FinancialReport\\" + DateTime.Now.ToString("yyMM") + "\\" + DateTime.Now.ToString("yyyyMMdd") + ".json";
            _mockFileHandling.SetupAllProperties();
            _mockFileHandling.Setup(t => t.directoryExists(fileSimulation1)).Returns(false);
            _mockFileHandling.Setup(t => t.createDirectory(fileSimulation1));
            _mockFileHandling.Setup(t => t.fileExists(fileSimulation3)).Returns(false);
            _mockFileHandling.Setup(t => t.directoryExists(fileSimulation2)).Returns(false);
            _mockFileHandling.Setup(t => t.createDirectory(fileSimulation2));
            _mockFileHandling.Setup(t => t.fileExists(fileSimulation4)).Returns(false);
            _mockCustomRandom.Setup(t => t.randomInt1(48, 57)).Returns(48);
            _mockCustomRandom.Setup(t => t.randomInt2(48, 57)).Returns(48);
            _mockCustomRandom.Setup(t => t.randomInt3(48, 57)).Returns(48);
            _mockCustomRandom.Setup(t => t.randomIntMax(9)).Returns(0);
            _mockFileHandling.Setup(t => t.readAllText(fileSimulation3)).Returns("[{\"betAmount\":2.0,\"payout\":4.0}]");
            _mockFileHandling.Setup(t => t.readAllText(fileSimulation4)).Returns("[{\"betAmount\":2.0,\"payout\":4.0}]");
            _mockFileHandling.Setup(t => t.writeAllText(fileSimulation3, gamblerAmountList));
            _mockFileHandling.Setup(t => t.writeAllText(fileSimulation4, fRAmountlList));

            Gambling gambling = new Gambling(_mockFileHandling.Object, _mockCustomRandom.Object);
            var res = gambling.playSlot(bet, username, prizeModuleBool);
            Assert.Equal("000", res);
        }


        [Theory]
        [InlineData(2.0, "1", false)]
        public void playSlotTestNoWin(double bet, string username, bool prizeModuleBool)
        {
            string gamblerAmountList = "[{\"betAmount\":2.0,\"payout\":0.0}]";
            string fRAmountlList = "[{\"betAmount\":2.0,\"payout\":0.0}]";
            string fileSimulation1 = "Users\\" + username + "\\" + DateTime.Now.ToString("yyMM");
            string fileSimulation2 = "FinancialReport\\" + DateTime.Now.ToString("yyMM");
            string fileSimulation3 = "Users\\" + username + "\\" + DateTime.Now.ToString("yyMM") + "\\" + DateTime.Now.ToString("yyyyMMdd") + ".json";
            string fileSimulation4 = "FinancialReport\\" + DateTime.Now.ToString("yyMM") + "\\" + DateTime.Now.ToString("yyyyMMdd") + ".json";
            _mockFileHandling.SetupAllProperties();
            _mockFileHandling.Setup(t => t.directoryExists(fileSimulation1)).Returns(false);
            _mockFileHandling.Setup(t => t.createDirectory(fileSimulation1));
            _mockFileHandling.Setup(t => t.fileExists(fileSimulation3)).Returns(false);
            _mockFileHandling.Setup(t => t.directoryExists(fileSimulation2)).Returns(false);
            _mockFileHandling.Setup(t => t.createDirectory(fileSimulation2));
            _mockFileHandling.Setup(t => t.fileExists(fileSimulation4)).Returns(false);
            _mockCustomRandom.Setup(t => t.randomInt1(48, 57)).Returns(48);
            _mockCustomRandom.Setup(t => t.randomInt2(48, 57)).Returns(49);
            _mockCustomRandom.Setup(t => t.randomInt3(48, 57)).Returns(50);
            _mockCustomRandom.Setup(t => t.randomIntMax(9)).Returns(0);
            _mockFileHandling.Setup(t => t.readAllText(fileSimulation3)).Returns("[{\"betAmount\":2.0,\"payout\":4.0}]");
            _mockFileHandling.Setup(t => t.readAllText(fileSimulation4)).Returns("[{\"betAmount\":2.0,\"payout\":4.0}]");
            _mockFileHandling.Setup(t => t.writeAllText(fileSimulation3, gamblerAmountList));
            _mockFileHandling.Setup(t => t.writeAllText(fileSimulation4, fRAmountlList));

            Gambling gambling = new Gambling(_mockFileHandling.Object, _mockCustomRandom.Object);
            var res = gambling.playSlot(bet, username, prizeModuleBool);
            Assert.Equal("012", res);
        }

        [Theory]
        [InlineData(2.0, "1", true)]
        public void playSlotTestJackPot(double bet, string username, bool prizeModuleBool)
        {
            string gamblerAmountList = "[{\"betAmount\":2.0,\"payout\":14.0}]";
            string fRAmountlList = "[{\"betAmount\":2.0,\"payout\":14.0}]";
            string fileSimulation1 = "Users\\" + username + "\\" + DateTime.Now.ToString("yyMM");
            string fileSimulation2 = "FinancialReport\\" + DateTime.Now.ToString("yyMM");
            string fileSimulation3 = "Users\\" + username + "\\" + DateTime.Now.ToString("yyMM") + "\\" + DateTime.Now.ToString("yyyyMMdd") + ".json";
            string fileSimulation4 = "FinancialReport\\" + DateTime.Now.ToString("yyMM") + "\\" + DateTime.Now.ToString("yyyyMMdd") + ".json";
            _mockFileHandling.SetupAllProperties();
            _mockFileHandling.Setup(t => t.directoryExists(fileSimulation1)).Returns(false);
            _mockFileHandling.Setup(t => t.createDirectory(fileSimulation1));
            _mockFileHandling.Setup(t => t.fileExists(fileSimulation3)).Returns(false);
            _mockFileHandling.Setup(t => t.directoryExists(fileSimulation2)).Returns(false);
            _mockFileHandling.Setup(t => t.createDirectory(fileSimulation2));
            _mockFileHandling.Setup(t => t.fileExists(fileSimulation4)).Returns(false);
            _mockCustomRandom.Setup(t => t.randomInt1(48, 57)).Returns(55);
            _mockCustomRandom.Setup(t => t.randomInt2(48, 57)).Returns(55);
            _mockCustomRandom.Setup(t => t.randomInt3(48, 57)).Returns(55);
            _mockCustomRandom.Setup(t => t.randomIntMax(9)).Returns(0);
            _mockFileHandling.Setup(t => t.readAllText(fileSimulation3)).Returns("[{\"betAmount\":2.0,\"payout\":4.0}]");
            _mockFileHandling.Setup(t => t.readAllText(fileSimulation4)).Returns("[{\"betAmount\":2.0,\"payout\":4.0}]");
            _mockFileHandling.Setup(t => t.writeAllText(fileSimulation3, gamblerAmountList));
            _mockFileHandling.Setup(t => t.writeAllText(fileSimulation4, fRAmountlList));

            Gambling gambling = new Gambling(_mockFileHandling.Object, _mockCustomRandom.Object);
            var res = gambling.playSlot(bet, username, prizeModuleBool);
            Assert.Equal("777", res);
        }

    }
}
