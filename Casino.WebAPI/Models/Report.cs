using System;
using System.ComponentModel.DataAnnotations;

namespace Casino.WebAPI.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class Report
    {
        [Key]
        public int ReportID { get; set; }
        /// <summary>
        /// 
        /// </summary>

        public double BetAmount { get; private set; }
        /// <summary>
        /// 
        /// </summary>

        public double Payout { get; private set; }
        /// <summary>
        /// 
        /// </summary>

        public DateTime Date { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="betAmount"></param>
        /// <param name="payout"></param>
        /// <param name="date"></param>
        public Report(double betAmount, double payout, DateTime date)
        {
            ReportID.GetType();
            BetAmount = betAmount;
            Payout = payout;
            Date = date;
        }
        public Report()
        { }
    }
}
