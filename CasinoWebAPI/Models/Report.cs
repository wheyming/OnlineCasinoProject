﻿using System;
using Newtonsoft.Json;

namespace CasinoWebAPI.Models
{
    /// <summary>
    /// 
    /// </summary>
    internal class Report
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
        public Report() { }
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