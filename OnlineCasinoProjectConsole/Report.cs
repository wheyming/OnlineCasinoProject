using Newtonsoft.Json;
using System;

namespace OnlineCasinoProjectConsole
{
    public class Report
    {
        [JsonProperty]
        public double BetAmount { get; private set; }
        [JsonProperty]
        public double Payout { get; private set; }
        [JsonProperty]
        public DateTime Date { get; private set; }

        public Report()
        { }

        public Report(double betAmount, double payout, DateTime date)
        {
            BetAmount = betAmount;
            Payout = payout;
            Date = date;
        }
    }
}
