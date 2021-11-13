﻿using Casino.WebAPI.EntityFramework;
using Casino.WebAPI.Interfaces;
using Casino.WebAPI.Models;
using System;
using System.Collections.Generic;
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

        /// <summary>
        /// 
        /// </summary>
        internal ReportController()
        {
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
            List<double> monthlyFinancialReport = new List<double>(new double[32]);
            using (CasinoContext casinoContext = new CasinoContext())
            {
                List<Report> filteredReports = casinoContext.Reports.Where(x => x.Date.Year == monthYear.Year && x.Date.Month == monthYear.Month).ToList();
                foreach (Report report in filteredReports)
                {
                    monthlyFinancialReport[report.Date.Day] += report.BetAmount - report.Payout;
                    monthlyFinancialReport[0] += report.BetAmount - report.Payout;
                }
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
            using (CasinoContext casinoContext = new CasinoContext())
            {
                List<Report> filteredReports = casinoContext.Reports.Where(x => x.Date.Year == year.Year).ToList();
                foreach (Report report in filteredReports)
                {
                    yearlyFinancialReport[report.Date.Month] += (report.BetAmount - report.Payout);
                    yearlyFinancialReport[0] += report.BetAmount - report.Payout;
                }
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
            using (CasinoContext casinoContext = new CasinoContext())
            {
                List<Report> filteredReports = casinoContext.Reports.Where(x => x.Date.Year == date.Year && x.Date.Month == date.Month && x.Date.Day == date.Day).ToList();
                foreach (Report report in filteredReports)
                {
                    dailyFinancialReport += (report.BetAmount - report.Payout);
                }
            }
            return dailyFinancialReport;
        }
    }
}
