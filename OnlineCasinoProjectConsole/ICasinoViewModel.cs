using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCasinoProjectConsole
{
    internal interface ICasinoViewModel
    {
        string CheckUserName(string userName);
        string CheckIDNumber(string idNumber);
        string CheckPhoneNumber(string phoneNumber);
        IList<string> CheckPassword(string passWord);
        string SignUp(string userName, string idNumber, string phoneNumber, string passWord);
        (string, bool) CheckLogin(string userName, string passWord);
        void SetPrizeModuleStatus(int inputPrizeSetting);
        string SeeDayFinancialReport(DateTime inputDateTime);
        IList<string> SeeMonthFinancialReport(DateTime inputDateTime);
        IList<string> SeeYearFinancialReport(DateTime inputDateTime);
        void LogOut();
        (IList<int>, string) playSlot(int betAmount, string userName);
    }
}
