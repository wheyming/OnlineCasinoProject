using CasinoWebAPI.Common;
using CasinoWebAPI.Interfaces;
using CasinoWebAPI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using CasinoWebAPI.Utility;

namespace CasinoWebAPI.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    internal class GamblingController : IGamblingManager
    {
        private List<Report> _amountList;
        private readonly IFileManager _fileHandling;
        private readonly IRandomNumberGenerator _customRandom;
        private readonly IConfigurationManager _config;
        private readonly IReportManager _financialReport;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="config"></param>
        /// <param name="financialReport"></param>
        public GamblingController(IConfigurationManager config, IReportManager financialReport)
        {
            _fileHandling = new FileManager();
            _customRandom = new RandomNumberGenerator();
            _amountList = new List<Report>();
            _config = config;
            _financialReport = financialReport;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="betAmount"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        public (int[], double, SlotsResultType) PlaySlot(double betAmount, string username)
        {
            int[] slotNumbers = new int[] { 0, 1, 2, 3, 4, 5, 6, 8, 9 };
            int[] rolledNumber = new int[3];
            rolledNumber[0] = _customRandom.GetRandomNumber(0, 9);
            rolledNumber[1] = _customRandom.GetRandomNumber(0, 9);

            if (_config.IsPrizeEnabled == false && rolledNumber[0] == 7 && rolledNumber[1] == 7)
            {
                rolledNumber[2] = slotNumbers[_customRandom.GetRandomNumber(slotNumbers.Length)];
            }
            else
            {
                rolledNumber[2] = _customRandom.GetRandomNumber(0, 9);
            }
            (double, SlotsResultType) winningsAndResultType = CalculateWinningsSlot(rolledNumber, betAmount);
            StoreWinningsInfo(winningsAndResultType.Item1, betAmount, username);
            return (rolledNumber, winningsAndResultType.Item1, winningsAndResultType.Item2);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="number"></param>
        /// <param name="betAmount"></param>
        /// <returns></returns>
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="payout"></param>
        /// <param name="betAmount"></param>
        /// <param name="username"></param>
        private void StoreWinningsInfo(double payout, double betAmount, string username)
        {
            DateTime storeWinningsTime = DateTime.Now;
            Report report = new Report(betAmount, payout, storeWinningsTime);
            if (!_fileHandling.DirectoryExists("Users\\" + username + "\\" + storeWinningsTime.ToString("yyMM")))
            {
                _fileHandling.CreateDirectory("Users\\" + username + "\\" + storeWinningsTime.ToString("yyMM"));
            }
            if (!_fileHandling.DirectoryExists("FinancialReport\\" + storeWinningsTime.ToString("yyMM")))
            {
                _fileHandling.CreateDirectory("FinancialReport\\" + storeWinningsTime.ToString("yyMM"));
            }


            if (_fileHandling.FileExists("Users\\" + username + "\\" + storeWinningsTime.ToString("yyMM") + "\\" + storeWinningsTime.ToString("yyyyMMdd") + ".json"))
            {
                _amountList = JsonConvert.DeserializeObject<List<Report>>(_fileHandling.ReadAllText("Users\\" + username + "\\" + storeWinningsTime.ToString("yyMM") + "\\" + storeWinningsTime.ToString("yyyyMMdd") + ".json"));
            }
            else
            {
                _amountList.Clear();
            }
            _financialReport.UpdateReportList(report);

            _amountList?.Add(report);
            string jsonAmountList = JsonConvert.SerializeObject(_amountList);
            _fileHandling.WriteAllText("Users\\" + username + "\\" + storeWinningsTime.ToString("yyMM") + "\\" + storeWinningsTime.ToString("yyyyMMdd") + ".json", jsonAmountList);


            if (_fileHandling.FileExists("FinancialReport\\" + storeWinningsTime.ToString("yyMM") + "\\" + storeWinningsTime.ToString("yyyyMMdd") + ".json"))
            {
                _amountList = JsonConvert.DeserializeObject<List<Report>>(_fileHandling.ReadAllText("FinancialReport\\" + storeWinningsTime.ToString("yyMM") + "\\" + storeWinningsTime.ToString("yyyyMMdd") + ".json"));
            }
            else
            {
                _amountList?.Clear();
            }
            _amountList?.Add(report);
            jsonAmountList = JsonConvert.SerializeObject(_amountList);
            _fileHandling.WriteAllText("FinancialReport\\" + storeWinningsTime.ToString("yyMM") + "\\" + storeWinningsTime.ToString("yyyyMMdd") + ".json", jsonAmountList);
        }
    }
}
