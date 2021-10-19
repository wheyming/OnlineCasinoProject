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
    class Gambling
    {
        char firstNum;
        char secondNum;
        char thirdNum;
        double winnings;
        List<FinancialReport> amountList = new List<FinancialReport>();

        // int values are in ASCII so that when converted to char will be 0 to 9.
        public void playSlot(double betAmount, string username)
        {
            int[] slotnumbers = new int[] { 48, 49, 50, 51, 52, 53, 54, 56, 57 };
            Random rand = new Random();
            firstNum = Convert.ToChar(rand.Next(48, 57));
            secondNum = Convert.ToChar(rand.Next(48, 57));

            if (Owner.prizeModuleBool == false && firstNum == '7' && secondNum == '7')
            {
                thirdNum = Convert.ToChar(slotnumbers[rand.Next(slotnumbers.Length)]);
            }
            else
            {
                thirdNum = Convert.ToChar(rand.Next(48, 57));
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
        }

        private double calculateWinningsSlot(string number, double betAmount)
        {
            if ((number[0] == 7) && (number[1] == 7) && (number[2] == 7))
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
            if (!Directory.Exists("Users\\" + username + "\\" + storeWinningsTime.ToString("yyMM")))
            {
                Directory.CreateDirectory("Users\\" + username + "\\" + storeWinningsTime.ToString("yyMM"));
            }
            if (!Directory.Exists("FinancialReport\\" + storeWinningsTime.ToString("yyMM")))
            {
                Directory.CreateDirectory("FinancialReport\\" + storeWinningsTime.ToString("yyMM"));
            }


            if (File.Exists("Users\\" + username + "\\" + storeWinningsTime.ToString("yyMM") + "\\" + storeWinningsTime.ToString("yyyyMMdd") + ".json"))
            {
                amountList = JsonConvert.DeserializeObject<List<FinancialReport>>(File.ReadAllText("Users\\" + username + "\\" + storeWinningsTime.ToString("yyMM") + "\\" + storeWinningsTime.ToString("yyyyMMdd") + ".json"));
            }
            else
            {
                amountList.Clear();
            }
            FinancialReport financialReport = new FinancialReport(betAmount, payout);
            amountList.Add(financialReport);
            string jsonAmountList = JsonConvert.SerializeObject(amountList);
            File.WriteAllText("Users\\" + username + "\\" + storeWinningsTime.ToString("yyMM") + "\\" + storeWinningsTime.ToString("yyyyMMdd") + ".json", jsonAmountList);

            if (File.Exists("FinancialReport\\" + storeWinningsTime.ToString("yyMM") + "\\" + storeWinningsTime.ToString("yyyyMMdd") + ".json"))
            {
                amountList = JsonConvert.DeserializeObject<List<FinancialReport>>(File.ReadAllText("FinancialReport\\" + storeWinningsTime.ToString("yyMM") + "\\" + storeWinningsTime.ToString("yyyyMMdd") + ".json"));
            }
            else
            {
                amountList.Clear();
            }
            amountList.Add(financialReport);
            jsonAmountList = JsonConvert.SerializeObject(amountList);
            File.WriteAllText("FinancialReport\\" + storeWinningsTime.ToString("yyMM") + "\\" + storeWinningsTime.ToString("yyyyMMdd") + ".json", jsonAmountList);
        }
    }
}
