using Casino.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Casino.WebAPI.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class Report
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty]
        public double BetAmount { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty]
        public double Payout { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty]
        public DateTime Date { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="betAmount"></param>
        /// <param name="payout"></param>
        /// <param name="date"></param>
        public Report(double betAmount, double payout, DateTime date)
        {
            BetAmount = betAmount;
            Payout = payout;
            Date = date;
        }
    }
}
