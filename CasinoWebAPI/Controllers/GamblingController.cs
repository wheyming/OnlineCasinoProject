using CasinoWebAPI.Common;
using CasinoWebAPI.Interfaces;
using CasinoWebAPI.Models;
using CasinoWebAPI.Utility;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

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
        public (IList<int>, double, SlotsResultType) PlaySlot(double betAmount, string username)
        {
            IList<int> rolledNumber = new List<int>();
            if (_config.IsPrizeEnabled == false)
            {
                rolledNumber = _customRandom.RollRandomNumberPrizeNotActivated();
            }
            else
            {
                rolledNumber = _customRandom.RollRandomNumberPrizeActivated();
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
        private (double, SlotsResultType) CalculateWinningsSlot(IList<int> number, double betAmount)
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
