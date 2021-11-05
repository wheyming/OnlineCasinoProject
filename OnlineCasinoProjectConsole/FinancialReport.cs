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
    public class FinancialReport : IFinancialReport
    {

        private List<Report> _reportList;
        private IFileHandling _fileHandling;

        public void ReportListInitialize()
        {
            GetLatestFinancialReports();
        }

        private void GetLatestFinancialReports()
        {
            foreach (string dirPath in Directory.GetDirectories("FinancialReport"))
            {
                foreach (string filePath in Directory.GetFiles(dirPath))
                {
                    List<Report> fileReports = JsonConvert.DeserializeObject<List<Report>>(_fileHandling.readAllText(filePath));
                    _reportList = _reportList.Concat(fileReports).ToList();
                }
            }
        }

        public void UpdateReportList(Report report)
        {
            _reportList.Add(report);
        }


        public FinancialReport(IFileHandling fileHandling)
        {
            _fileHandling = fileHandling;
            _reportList = new List<Report>();
        }

        public List<double> GenerateFinancialReportMonth(DateTime monthyear)
        {
            List<double> monthlyFinancialReport = new List<double>(new double[32]);
            try
            {
                List<Report> filteredReports = _reportList.Where(x => x.Date.Year == monthyear.Year && x.Date.Month == monthyear.Month).ToList();
                foreach (Report report in filteredReports)
                {
                    monthlyFinancialReport[report.Date.Day] += report.BetAmount - report.Payout;
                    monthlyFinancialReport[0] += report.BetAmount - report.Payout;
                }
            }
            catch (IOException)
            {
            }
            return monthlyFinancialReport;
        }

        public List<double> GenerateFinancialReportYear(DateTime year)
        {
            List<double> yearlyFinancialReport = new List<double>(new double[13]);
            try
            {
                List<Report> filteredReports = _reportList.Where(x => x.Date.Year == year.Year).ToList();
                foreach (Report report in filteredReports)
                {
                    yearlyFinancialReport[report.Date.Month] += (report.BetAmount - report.Payout);
                    yearlyFinancialReport[0] += report.BetAmount - report.Payout;
                }
            }
            catch (IOException)
            {
            }
            return yearlyFinancialReport;
        }

        public double GenerateFinancialReportDay(DateTime date)
        {
            double dailyFinancialReport = new double();
            try
            {
                List<Report> filteredReports = _reportList.Where(x => x.Date == date).ToList();
                foreach (Report report in filteredReports)
                {
                    dailyFinancialReport += (report.BetAmount - report.Payout);
                }
            }
            catch (IOException)
            {
            }
            return dailyFinancialReport;
        }
    }
}
