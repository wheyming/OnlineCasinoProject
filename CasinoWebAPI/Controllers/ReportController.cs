using CasinoWebAPI.Interfaces;
using CasinoWebAPI.Models;
using CasinoWebAPI.Utility;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CasinoWebAPI.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    internal class ReportController : IReportManager
    {
        private List<Report> _reportList;
        private readonly IFileManager _fileHandling;
        /// <summary>
        /// 
        /// </summary>
        public void ReportListInitialize()
        {
            GetLatestFinancialReports();
        }
        /// <summary>
        /// 
        /// </summary>
        private void GetLatestFinancialReports()
        {
            if (!Directory.Exists("FinancialReport"))
            {
                Directory.CreateDirectory("FinancialReport");
            }
            foreach (string dirPath in Directory.GetDirectories("FinancialReport"))
            {
                foreach (string filePath in Directory.GetFiles(dirPath))
                {
                    List<Report> fileReports = JsonConvert.DeserializeObject<List<Report>>(_fileHandling.ReadAllText(filePath));
                    if (fileReports != null) _reportList = _reportList.Concat(fileReports).ToList();
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="report"></param>
        public void UpdateReportList(Report report)
        {
            _reportList.Add(report);
        }
        /// <summary>
        /// 
        /// </summary>
        public ReportController()
        {
            _fileHandling = new FileManager();
            _reportList = new List<Report>();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="monthYear"></param>
        /// <returns></returns>
        public List<double> GenerateFinancialReportMonth(DateTime monthYear)
        {
            List<double> monthlyFinancialReport = new List<double>(new double[32]);
            try
            {
                List<Report> filteredReports = _reportList.Where(x => x.Date.Year == monthYear.Year && x.Date.Month == monthYear.Month).ToList();
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public double GenerateFinancialReportDay(DateTime date)
        {
            double dailyFinancialReport = new double();
            try
            {
                List<Report> filteredReports = _reportList.Where(x => x.Date.ToShortDateString() == date.ToShortDateString()).ToList();
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
