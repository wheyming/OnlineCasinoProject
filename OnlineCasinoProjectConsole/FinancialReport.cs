using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCasinoProjectConsole
{
    class FinancialReport
    {
        public double betAmount { get; set; }
        public double payout { get; set; }
        public FinancialReport(double betAmount, double payout)
        {
            this.betAmount = betAmount;
            this.payout = payout;
        }
        public FinancialReport()
        {
        }

        public void generateFinancialReportMonth(DateTime month)
        {
            double monthTotal = 0;
            string[] fileArr = Directory.GetFiles("FinancialReport\\2110");
            foreach (string fileName in fileArr)
            {
                List<FinancialReport> monthCompile = JsonConvert.DeserializeObject<List<FinancialReport>>(File.ReadAllText(fileName));
                foreach(FinancialReport FR in monthCompile)
                {
                    monthTotal -= FR.payout;
                    monthTotal += FR.betAmount;
                }
                Console.WriteLine(monthTotal);            
            }           
        }

        public void generateFinancialReportYear(DateTime year)
        {

        }

        public void generateFinancialReportDay()
        {

        }
    }
}
