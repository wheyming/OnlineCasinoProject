using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace OnlineCasinoProjectConsole
{
    public class Gambling : IGambling
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
        public (int[], double, SlotsResultType) PlaySlot(double betAmount, string username)
        {
            int[] slotnumbers = new int[] { 0, 1, 2, 3, 4, 5, 6, 8, 9 };
            int[] rolledNumber = new int[3];
            rolledNumber[0] = _customRandom.randomInt1(0, 9);
            rolledNumber[1] = _customRandom.randomInt2(0, 9);

            if (_config.IsPrizeEnabled == false && rolledNumber[0] == 7 && rolledNumber[1] == 7)
            {
                rolledNumber[2] = slotnumbers[_customRandom.randomIntMax(slotnumbers.Length)];
            }
            else
            {
                rolledNumber[2] = _customRandom.randomInt3(0, 9);
            }
            (double, SlotsResultType) winningsAndResultType = CalculateWinningsSlot(rolledNumber, betAmount);
            StoreWinningsInfo(winningsAndResultType.Item1, betAmount, username);
            return (rolledNumber, winningsAndResultType.Item1, winningsAndResultType.Item2);
        }

        private (double, SlotsResultType) CalculateWinningsSlot(int[] number, double betAmount)
        {
            double winnings;
            SlotsResultType resultType;
            if ((number[0] == '7') && (number[1] == '7') && (number[2] == '7'))
            {
                winnings = betAmount * 7;
                resultType = SlotsResultType.JackPot;
            }
            else if ((number[0] == number[1]) && (number[1] == number[2]))
            {
                winnings = betAmount * 3;
                resultType = SlotsResultType.Triple;
            }
            else if ((number[0] == number[1]) || (number[1] == number[2]))
            {
                winnings = betAmount * 2;
                resultType = SlotsResultType.Double;
            }
            else
            {
                winnings = 0;
                resultType = SlotsResultType.None;
            }
            return (winnings, resultType);
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
