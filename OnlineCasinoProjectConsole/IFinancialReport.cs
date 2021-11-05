using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCasinoProjectConsole
{
    public interface IFinancialReport
    {
        void ReportListInitialize();
        void UpdateReportList(Report report);
        double GenerateFinancialReportDay(DateTime date);
        List<double> GenerateFinancialReportMonth(DateTime monthyear);
        List<double> GenerateFinancialReportYear(DateTime year);
    }
}
