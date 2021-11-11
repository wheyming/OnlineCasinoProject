using CasinoWebAPI.Models;
using System;
using System.Collections.Generic;

namespace CasinoWebAPI.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    internal interface IReportManager
    {
        /// <summary>
        /// 
        /// </summary>
        void ReportListInitialize();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="report"></param>
        void UpdateReportList(Report report);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        double GenerateFinancialReportDay(DateTime date);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="monthyear"></param>
        /// <returns></returns>
        List<double> GenerateFinancialReportMonth(DateTime monthyear);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        List<double> GenerateFinancialReportYear(DateTime year);
    }
}
