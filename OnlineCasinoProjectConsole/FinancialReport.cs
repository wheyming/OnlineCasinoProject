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
        public void generateFinancialReportMonth(DateTime month)
        {
            double monthTotal = 0;
            string[] fileArr = Directory.GetFiles("FinancialReport\\2110");
            foreach (string fileName in fileArr)
            {
                double monthCompile = JsonConvert.DeserializeObject<double>(File.ReadAllText(fileName));
                monthTotal += monthCompile;
            }
            Console.WriteLine(monthTotal);
        }

        public void generateFinancialReportYear(DateTime year)
        {

        }
    }
}
