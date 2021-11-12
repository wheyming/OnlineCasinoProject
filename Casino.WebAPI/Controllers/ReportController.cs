using Casino.WebAPI.EntityFramework;
using Casino.WebAPI.Interfaces;
using Casino.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web.Http;

namespace Casino.WebAPI.Controllers
{
    [RoutePrefix("api/FinancialReport")]
    /// <summary>
    /// 
    /// </summary>
    public class ReportController : ApiController, IReportManager
    {
        private List<Report> _reportList;


        /// <summary>
        /// 
        /// </summary>
        private void GetLatestFinancialReports()
        {
            using (CasinoContext casinoContext = new CasinoContext())
            {
                var getReportList = casinoContext.Reports.ToListAsync<Report>();
                getReportList.Wait();
                _reportList = getReportList.Result;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        internal ReportController()
        {
            _reportList = new List<Report>();
        }

        [HttpGet]
        [Route("month")]
        /// <summary>
        /// 
        /// </summary>
        /// <param name="monthYear"></param>
        /// <returns></returns>
        public List<double> GenerateFinancialReportMonth(DateTime monthYear)
        {
            GetLatestFinancialReports();
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

        [HttpGet]
        [Route("year")]
        /// <summary>
        /// 
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public List<double> GenerateFinancialReportYear(DateTime year)
        {
            GetLatestFinancialReports();
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

        [HttpGet]
        [Route("day")]
        /// <summary>
        /// 
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public double GenerateFinancialReportDay(DateTime date)
        {
            GetLatestFinancialReports();
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
