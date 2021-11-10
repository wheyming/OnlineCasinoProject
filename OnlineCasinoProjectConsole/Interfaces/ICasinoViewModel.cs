using System.Collections.Generic;

namespace OnlineCasinoProjectConsole.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    internal interface ICasinoViewModel
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        string CheckUserName(string userName);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="idNumber"></param>
        /// <returns></returns>
        string CheckIdNumber(string idNumber);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        string CheckPhoneNumber(string phoneNumber);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="passWord"></param>
        /// <returns></returns>
        IList<string> CheckPassword(string passWord);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="idNumber"></param>
        /// <param name="phoneNumber"></param>
        /// <param name="passWord"></param>
        /// <returns></returns>
        string SignUp(string userName, string idNumber, string phoneNumber, string passWord);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="passWord"></param>
        /// <returns></returns>
        (string, bool) CheckLogin(string userName, string passWord);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="inputPrizeSetting"></param>
        void SetPrizeModuleStatus(int inputPrizeSetting);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inputDay"></param>
        /// <returns></returns>
        string SeeDayFinancialReport(string inputDay);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inputMonth"></param>
        /// <returns></returns>
        IList<string> SeeMonthFinancialReport(string inputMonth);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inputYear"></param>
        /// <returns></returns>
        IList<string> SeeYearFinancialReport(string inputYear);
        /// <summary>
        /// 
        /// </summary>
        void LogOut();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="betAmount"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        (IList<int>, string) PlaySlot(int betAmount, string userName);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="value"></param>
        void ParseInputString(string input, out int? value);
    }
}
