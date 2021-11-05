using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading;

namespace OnlineCasinoProjectConsole
{
    public class Gambling: IGambling
    {

        [JsonProperty]
        List<Report> amountList = new List<Report>();

        private IFileHandling _fileHandling;
        private ICustomRandom _customRandom;
        private ICasinoConfiguration _config;
        private IFinancialReport _financialReport;

        public Gambling(IFileHandling fileHandling, ICustomRandom customRandom, ICasinoConfiguration config, IFinancialReport financialReport)
        {
            _fileHandling = fileHandling;
            _customRandom = customRandom;
            _config = config;
            _financialReport = financialReport;
        }

        // int values are in ASCII so that when converted to char will be 0 to 9.
        public string PlaySlot(double betAmount, string username)
        {
            int[] slotnumbers = new int[] { 48, 49, 50, 51, 52, 53, 54, 56, 57 };
            char firstNum = Convert.ToChar(_customRandom.randomInt1(48, 57));
            char secondNum = Convert.ToChar(_customRandom.randomInt2(48, 57));
            char thirdNum;

            if (_config.IsPrizeEnabled == false && firstNum == '7' && secondNum == '7')
            {
                thirdNum = Convert.ToChar(slotnumbers[_customRandom.randomIntMax(slotnumbers.Length)]);
            }
            else
            {
                thirdNum = Convert.ToChar(_customRandom.randomInt3(48, 57));
            }
            Console.Write(firstNum);
            Thread.Sleep(500);
            Console.Write('.');
            Thread.Sleep(500);
            Console.Write(secondNum);
            Thread.Sleep(500);
            Console.Write('.');
            Thread.Sleep(500);
            Console.Write(thirdNum);
            string numberCombined = Convert.ToString(firstNum) + Convert.ToString(secondNum) + Convert.ToString(thirdNum);
            StoreWinningsInfo(CalculateWinningsSlot(numberCombined, betAmount), betAmount, username);
            return numberCombined;
        }

        private double CalculateWinningsSlot(string number, double betAmount)
        {
            double winnings;
            if ((number[0] == '7') && (number[1] == '7') && (number[2] == '7'))
            {
                winnings = betAmount * 7;
                Console.WriteLine($"\nJACKPOT!! Congratulations. Your winnings is: {winnings}");
            }
            else if ((number[0] == number[1]) && (number[1] == number[2]))
            {
                winnings = betAmount * 3;
                Console.WriteLine($"\nTRIPLE!! Congratulations. Your winnings is: {winnings}");
            }
            else if ((number[0] == number[1]) || (number[1] == number[2]))
            {
                winnings = betAmount * 2;
                Console.WriteLine($"\nDOUBLE!! Congratulations. Your winnings is: {winnings}");
            }
            else
            {
                winnings = 0;
                Console.WriteLine($"\nUnfortunately, you did not win anything. Thank you for playing.");
            }
            return winnings;
        }

        public void StoreWinningsInfo(double payout, double betAmount, string username)
        {
            DateTime storeWinningsTime = DateTime.Now;
            Report report = new Report(betAmount, payout, storeWinningsTime);
            if (!_fileHandling.directoryExists("Users\\" + username + "\\" + storeWinningsTime.ToString("yyMM")))
            {
                _fileHandling.createDirectory("Users\\" + username + "\\" + storeWinningsTime.ToString("yyMM"));
            }
            if (!_fileHandling.directoryExists("FinancialReport\\" + storeWinningsTime.ToString("yyMM")))
            {
                _fileHandling.createDirectory("FinancialReport\\" + storeWinningsTime.ToString("yyMM"));
            }


            if (_fileHandling.fileExists("Users\\" + username + "\\" + storeWinningsTime.ToString("yyMM") + "\\" + storeWinningsTime.ToString("yyyyMMdd") + ".json"))
            {
                amountList = JsonConvert.DeserializeObject<List<Report>>(_fileHandling.readAllText("Users\\" + username + "\\" + storeWinningsTime.ToString("yyMM") + "\\" + storeWinningsTime.ToString("yyyyMMdd") + ".json"));
            }
            else
            {
                amountList.Clear();
            }
            _financialReport.UpdateReportList(report);

            amountList.Add(report);
            string jsonAmountList = JsonConvert.SerializeObject(amountList);
            _fileHandling.writeAllText("Users\\" + username + "\\" + storeWinningsTime.ToString("yyMM") + "\\" + storeWinningsTime.ToString("yyyyMMdd") + ".json", jsonAmountList);


            if (_fileHandling.fileExists("FinancialReport\\" + storeWinningsTime.ToString("yyMM") + "\\" + storeWinningsTime.ToString("yyyyMMdd") + ".json"))
            {
                amountList = JsonConvert.DeserializeObject<List<Report>>(_fileHandling.readAllText("FinancialReport\\" + storeWinningsTime.ToString("yyMM") + "\\" + storeWinningsTime.ToString("yyyyMMdd") + ".json"));
            }
            else
            {
                amountList.Clear();
            }
            amountList.Add(report);
            jsonAmountList = JsonConvert.SerializeObject(amountList);
            _fileHandling.writeAllText("FinancialReport\\" + storeWinningsTime.ToString("yyMM") + "\\" + storeWinningsTime.ToString("yyyyMMdd") + ".json", jsonAmountList);
        }
    }
}
