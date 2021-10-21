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
    public class FinancialReport
    {
        public double betAmount { get; set; }

        public double payout { get; set; }

        public FinancialReport(double betAmount, double payout)
        {
            this.betAmount = betAmount;
            this.payout = payout;
        }

        private IFileHandling _fileHandling;

        public FinancialReport(IFileHandling fileHandling)
        {
            _fileHandling = fileHandling;
        }

        public FinancialReport() // Required to prevent Json Exception in Gambling.storeWinningsInfo()
        {
        }

        public double generateFinancialReportMonth(DateTime yearmonth)
        {
            double monthTotal = 0;
            try
            {
                string[] fileArr = _fileHandling.directoryGetFiles("FinancialReport\\" + yearmonth.ToString("yyMM"));
                foreach (string fileName in fileArr)
                {
                    double dayTotal = 0;
                    List<FinancialReport> dayCompile = JsonConvert.DeserializeObject<List<FinancialReport>>(_fileHandling.readAllText(fileName));
                    foreach (FinancialReport FR in dayCompile)
                    {
                        dayTotal -= FR.payout;
                        dayTotal += FR.betAmount;
                    }
                    Console.WriteLine($"{ DateTime.ParseExact(fileName.Substring(fileName.IndexOf(".json") - 8, 8), "yyyyMMdd", CultureInfo.CurrentCulture).ToString("dd MMM yyyy")} earnings: ${dayTotal}");
                    monthTotal += dayTotal;
                }
                Console.WriteLine($"\nTotal earnings for {yearmonth.ToString("MMM")} {yearmonth.ToString("yyyy")} is ${monthTotal}.\n");
            }
            catch (IOException)
            {
                Console.WriteLine("File/Files for the particular month cannot be found.");
            }
            return monthTotal;
        }

        public double generateFinancialReportYear(DateTime year)
        {
            double yearTotal = 0;
            try
            {
                int monthCount = 12;

                for (int i = 1; i < 13; i++)
                {
                    double monthTotal = 0;
                    if (!_fileHandling.directoryExists("FinancialReport\\" + year.ToString("yy") + i.ToString("00")))
                    {
                        monthCount -= 1;
                        continue;
                    }
                    string[] fileArr = _fileHandling.directoryGetFiles("FinancialReport\\" + year.ToString("yy") + i.ToString("00"));
                    string monthFolder = "FinancialReport\\" + year.ToString("yy") + i.ToString("00");
                    foreach (string fileName in fileArr)
                    {
                        List<FinancialReport> dayCompile = JsonConvert.DeserializeObject<List<FinancialReport>>(_fileHandling.readAllText(fileName));
                        foreach (FinancialReport FR in dayCompile)
                        {
                            monthTotal -= FR.payout;
                            monthTotal += FR.betAmount;
                        }
                    }
                    Console.WriteLine($"{ (DateTime.ParseExact(monthFolder.Substring(monthFolder.IndexOf("FinancialReport\\") + 16, 4), "yyMM", CultureInfo.CurrentCulture)).ToString("MMM yyyy")} earnings: ${monthTotal}");
                    yearTotal += monthTotal;
                }
                if (monthCount != 0)
                {
                    Console.WriteLine($"\nTotal earnings for the year of {year.ToString("yyyy")} is ${yearTotal}.\n");
                }
                else
                {
                    Console.WriteLine("Files for the particular year does not exist.");
                }
            }
            catch (IOException)
            {
                Console.WriteLine("File/Files for the particular year cannot be found.");
            }
            return yearTotal;
        }

        public double generateFinancialReportDay(DateTime date)
        {
            string fileName = "FinancialReport\\" + date.ToString("yyMM") + "\\" + date.ToString("yyyyMMdd") + ".json";
            double dayTotal = 0;
            try
            {

                List<FinancialReport> dayCompile = JsonConvert.DeserializeObject<List<FinancialReport>>(_fileHandling.readAllText(fileName));
                foreach (FinancialReport FR in dayCompile)
                {
                    dayTotal -= FR.payout;
                    dayTotal += FR.betAmount;
                }
                Console.WriteLine($"\nTotal earnings for {date.ToString("dd MMM yyyy")} is ${dayTotal}\n");
            }
            catch (IOException)
            {
                Console.WriteLine("File for this date cannot be found.");
            }
            return dayTotal;
        }
    }
}
