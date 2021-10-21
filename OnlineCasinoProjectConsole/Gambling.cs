using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OnlineCasinoProjectConsole
{
    public class Gambling
    {
        char firstNum;
        char secondNum;
        char thirdNum;
        double winnings;

        [JsonProperty]
        List<FinancialReport> amountList = new List<FinancialReport>();

        private IFileHandling _fileHandling;
        private ICustomRandom _customRandom;

        public Gambling(IFileHandling fileHandling, ICustomRandom customRandom)
        {
            _fileHandling = fileHandling;
            _customRandom = customRandom;
        }

        // int values are in ASCII so that when converted to char will be 0 to 9.
        public string playSlot(double betAmount, string username, bool prizeModuleBool)
        {            
            int[] slotnumbers = new int[] { 48, 49, 50, 51, 52, 53, 54, 56, 57 };
            firstNum = Convert.ToChar(_customRandom.randomInt1(48, 57));
            secondNum = Convert.ToChar(_customRandom.randomInt2(48, 57));

            if (prizeModuleBool == false && firstNum == '7' && secondNum == '7')
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
            storeWinningsInfo(calculateWinningsSlot(numberCombined, betAmount), betAmount, username);
            return numberCombined;
        }

        private double calculateWinningsSlot(string number, double betAmount)
        {
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

        public void storeWinningsInfo(double payout, double betAmount, string username)
        {
            DateTime storeWinningsTime = DateTime.Now;
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
                amountList = JsonConvert.DeserializeObject<List<FinancialReport>>(_fileHandling.readAllText("Users\\" + username + "\\" + storeWinningsTime.ToString("yyMM") + "\\" + storeWinningsTime.ToString("yyyyMMdd") + ".json"));
            }
            else
            {
                amountList.Clear();
            }
            FinancialReport financialReport = new FinancialReport(betAmount, payout);
            amountList.Add(financialReport);
            string jsonAmountList = JsonConvert.SerializeObject(amountList);
            _fileHandling.writeAllText("Users\\" + username + "\\" + storeWinningsTime.ToString("yyMM") + "\\" + storeWinningsTime.ToString("yyyyMMdd") + ".json", jsonAmountList);

            if (_fileHandling.fileExists("FinancialReport\\" + storeWinningsTime.ToString("yyMM") + "\\" + storeWinningsTime.ToString("yyyyMMdd") + ".json"))
            {
                amountList = JsonConvert.DeserializeObject<List<FinancialReport>>(_fileHandling.readAllText("FinancialReport\\" + storeWinningsTime.ToString("yyMM") + "\\" + storeWinningsTime.ToString("yyyyMMdd") + ".json"));
            }
            else
            {
                amountList.Clear();
            }
            amountList.Add(financialReport);
            jsonAmountList = JsonConvert.SerializeObject(amountList);
            _fileHandling.writeAllText("FinancialReport\\" + storeWinningsTime.ToString("yyMM") + "\\" + storeWinningsTime.ToString("yyyyMMdd") + ".json", jsonAmountList);
        }
    }
}
