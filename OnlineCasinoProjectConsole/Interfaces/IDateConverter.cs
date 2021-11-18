using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCasinoProjectConsole.Interfaces
{
    public interface IDateConverter
    {
        DateTime InputDayConvert(string input3_5);
        DateTime InputMonthConvert(string input3_6);
        DateTime InputYearConvert(string input3_7);
    }
}
