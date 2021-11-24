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
    /// <summary>
    /// 
    /// </summary>
    [RoutePrefix("api/Gambling")]
    public class GamblingController : ApiController, IGamblingManager
    {
        private readonly ICasinoContext _casinoContext;
        private readonly string _connectionString;
        private readonly IRandomNumberGenerator _customRandom;
        private readonly IDateTimeGenerator _dateTimeGenerator;
        public GamblingController()
        {
            _customRandom = new RandomNumberGenerator();
#if DEBUG
            _connectionString = "DebugCasinoDBConnectionString";
#else
            _connectionString = "ReleaseCasinoDBConnectionString";
#endif
            _casinoContext = new CasinoContext(_connectionString);
            _dateTimeGenerator = new DateTimeGenerator();
        }

        public GamblingController(ICasinoContext casinoContext, IRandomNumberGenerator customRandom, IDateTimeGenerator dateTimeGenerator)
        {
            _casinoContext = casinoContext;
            _customRandom = customRandom;
            _dateTimeGenerator = dateTimeGenerator;
        }

        [HttpGet]
        [Route("playslot")]
        /// <summary>
        /// 
        /// </summary>
        /// <param name="betAmount"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        public (IList<int>, double, SlotsResultType) PlaySlot(double betAmount)
        {
            IList<int> rolledNumber = new List<int>();
            PrizeModule prizeModule;
            prizeModule = _casinoContext.PrizeModule.Where(x => x.Identifier == 1).FirstOrDefault();
            if (prizeModule.IsPrizeEnabled == false)
            {
                rolledNumber = _customRandom.RollRandomNumberPrizeNotActivated();
            }
            else
            {
                rolledNumber = _customRandom.RollRandomNumberPrizeActivated();
            }
            (double, SlotsResultType) winningsAndResultType = CalculateWinningsSlot(rolledNumber, betAmount);
            StoreWinningsInfo(winningsAndResultType.Item1, betAmount);
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
            if ((number[0] == 7) && (number[1] == 7) && (number[2] == 7))
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
        private void StoreWinningsInfo(double payout, double betAmount)
        {
            DateTime storeWinningsTime = _dateTimeGenerator.Now();
            Report report = new Report(betAmount, payout, storeWinningsTime);
            _casinoContext.Reports.Add(report);
            _casinoContext.SaveChanges();
        }
    }
}
