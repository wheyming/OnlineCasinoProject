using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
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

        public void generateFinancialReportMonth(DateTime yearmonth)
        {
            try
            {
                double monthTotal = 0;
                string[] fileArr = Directory.GetFiles("FinancialReport\\" + yearmonth.ToString("yyMM"));
                foreach (string fileName in fileArr)
                {
                    double dayTotal = 0;
                    List<FinancialReport> dayCompile = JsonConvert.DeserializeObject<List<FinancialReport>>(File.ReadAllText(fileName));
                    foreach (FinancialReport FR in dayCompile)
                    {
                        dayTotal -= FR.payout;
                        dayTotal += FR.betAmount;
                    }
                    Console.WriteLine($"{ DateTime.ParseExact(fileName.Substring(fileName.IndexOf(".json") - 8, 8), "yyyyMMdd", CultureInfo.CurrentCulture).ToString("dd MMM yyyy")} earnings: ${dayTotal}");
                    monthTotal += dayTotal;
                }
                Console.WriteLine($"Total earnings for {yearmonth.ToString("MMM")} {yearmonth.ToString("yyyy")} is ${monthTotal}.");
            }
            catch (IOException)
            {
                Console.WriteLine("File/Files for the particular month cannot be found.");
            }
        }

        public void generateFinancialReportYear(DateTime year)
        {
            try
            {
                double yearTotal = 0;
                for (int i = 1; i < 13; i++)
                {
                    double monthTotal = 0;
                    if (!Directory.Exists("FinancialReport\\" + year.ToString("yy") + i.ToString("00")))
                    {
                        continue;
                    }
                    string[] fileArr = Directory.GetFiles("FinancialReport\\" + year.ToString("yy") + i.ToString("00"));
                    string monthFolder = "FinancialReport\\" + year.ToString("yy") + i.ToString("00");
                    foreach (string fileName in fileArr)
                    {
                        List<FinancialReport> dayCompile = JsonConvert.DeserializeObject<List<FinancialReport>>(File.ReadAllText(fileName));
                        foreach (FinancialReport FR in dayCompile)
                        {
                            monthTotal -= FR.payout;
                            monthTotal += FR.betAmount;
                        }
                    }
                    Console.WriteLine($"{ (DateTime.ParseExact(monthFolder.Substring(monthFolder.IndexOf("FinancialReport\\") + 16, 4), "yyMM", CultureInfo.CurrentCulture)).ToString("MMM yyyy")} earnings: ${monthTotal}");
                    yearTotal += monthTotal;
                }
                Console.WriteLine($"Total earnings for the year of {year.ToString("yyyy")} is ${yearTotal}.");
            }
            catch (IOException)
            {
                Console.WriteLine("File/Files for the particular year cannot be found.");
            }
        }

        public void generateFinancialReportDay(DateTime date)
        {
            string fileName = "FinancialReport\\" + date.ToString("yyMM") + "\\" + date.ToString("yyyyMMdd") + ".json";
            try
            {
                double dayTotal = 0;
                List<FinancialReport> dayCompile = JsonConvert.DeserializeObject<List<FinancialReport>>(File.ReadAllText(fileName));
                foreach (FinancialReport FR in dayCompile)
                {
                    dayTotal -= FR.payout;
                    dayTotal += FR.betAmount;
                }
                Console.WriteLine($"Total earnings for {date.ToString("dd MMM yyyy")} is ${dayTotal}");
            }
            catch (IOException)
            {
                Console.WriteLine("File for this date cannot be found.");
            }
        }
    }
}
