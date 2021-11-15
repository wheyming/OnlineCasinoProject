using Casino.Common;
using Casino.WebAPI.EntityFramework;
using Casino.WebAPI.Interfaces;
using Casino.WebAPI.Models;
using Casino.WebAPI.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Casino.WebAPI.Controllers
{
    [RoutePrefix("api/Gambling")]
    /// <summary>
    /// 
    /// </summary>
    public class GamblingController : ApiController, IGamblingManager
    {
        private string _connectionString;
        private readonly IRandomNumberGenerator _customRandom;
        public GamblingController()
        {
            _customRandom = new RandomNumberGenerator();
#if DEBUG
            _connectionString = "DebugCasinoDBConnectionString";
#else
                _connectionString = "ReleaseCasinoDBConnectionString";
#endif
        }

        [HttpGet]
        [Route("playslot")]
        /// <summary>
        /// 
        /// </summary>
        /// <param name="betAmount"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        public (IList<int>, double, SlotsResultType, double, DateTime) PlaySlot(double betAmount, string username)
        {
            IList<int> rolledNumber = new List<int>();
            PrizeModule prizeModule;
            using (CasinoContext casinoContext = new CasinoContext(_connectionString))
            {
                prizeModule = casinoContext.PrizeModule.Where(x => x.Identifier == 1).FirstOrDefault();
            }
            if (prizeModule.IsPrizeEnabled == false)
            {
                rolledNumber = _customRandom.RollRandomNumberPrizeNotActivated();
            }
            else
            {
                rolledNumber = _customRandom.RollRandomNumberPrizeActivated();
            }
            (double, SlotsResultType) winningsAndResultType = CalculateWinningsSlot(rolledNumber, betAmount);
            DateTime resultTime = StoreWinningsInfo(winningsAndResultType.Item1, betAmount, username);
            return (rolledNumber, winningsAndResultType.Item1, winningsAndResultType.Item2, betAmount, resultTime);
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
        private DateTime StoreWinningsInfo(double payout, double betAmount, string username)
        {
            DateTime storeWinningsTime = DateTime.Now;
            Report report = new Report(betAmount, payout, storeWinningsTime);
            using (CasinoContext casinoContext = new CasinoContext(_connectionString))
            {
                casinoContext.Reports.Add(report);
                casinoContext.SaveChanges();
            }
            return storeWinningsTime;
        }
    }
}
