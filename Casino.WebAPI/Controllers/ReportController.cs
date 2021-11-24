using Casino.WebAPI.EntityFramework;
using Casino.WebAPI.Interfaces;
using Casino.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Casino.WebAPI.Controllers
{
    /// <summary>
    /// Controller to generate Reports for Owner.
    /// </summary>
    [RoutePrefix("api/FinancialReport")]
    public class ReportController : ApiController, IReportManager
    {
        private readonly ICasinoContext _casinoContext;
        private readonly string _connectionString;
        public ReportController()
        {
#if DEBUG
            _connectionString = "DebugCasinoDBConnectionString";
#else
            _connectionString = "ReleaseCasinoDBConnectionString";
#endif
            _casinoContext = new CasinoContext(_connectionString);
        }

        public ReportController(ICasinoContext casinoContext)
        {
            _casinoContext = casinoContext;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="monthYear"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("month")]
        public List<double> GenerateFinancialReportMonth(DateTime monthYear)
        {
            List<double> monthlyFinancialReport = new List<double>(new double[32]);
            List<Report> filteredReports = _casinoContext.Reports.Where(x => x.Date.Year == monthYear.Year && x.Date.Month == monthYear.Month).ToList();
            foreach (Report report in filteredReports)
            {
                monthlyFinancialReport[report.Date.Day] += report.BetAmount - report.Payout;
                monthlyFinancialReport[0] += report.BetAmount - report.Payout;
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
            List<double> yearlyFinancialReport = new List<double>(new double[13]);
            List<Report> filteredReports = _casinoContext.Reports.Where(x => x.Date.Year == year.Year).ToList();
            foreach (Report report in filteredReports)
            {
                yearlyFinancialReport[report.Date.Month] += (report.BetAmount - report.Payout);
                yearlyFinancialReport[0] += report.BetAmount - report.Payout;
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
            double dailyFinancialReport = new double();
            List<Report> filteredReports = _casinoContext.Reports.Where(x => x.Date.Year == date.Year && x.Date.Month == date.Month && x.Date.Day == date.Day).ToList();
            foreach (Report report in filteredReports)
            {
                dailyFinancialReport += (report.BetAmount - report.Payout);
            }
            return dailyFinancialReport;
        }
    }
}
