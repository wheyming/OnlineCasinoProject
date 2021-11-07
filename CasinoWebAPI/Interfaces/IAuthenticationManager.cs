using CasinoWebAPI.Common;
using CasinoWebAPI.Models;

namespace CasinoWebAPI.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    internal interface IAuthenticationManager
    {
        /// <summary>
        /// 
        /// </summary>
        User CurrentUser { get; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        UserNameResultType CheckUserName(string username);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="idNumber"></param>
        /// <returns></returns>
        IdResultType CheckId(string idNumber);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        PhoneNumberResultType CheckPhoneNumber(string phoneNumber);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        PasswordResultType CheckPassword(string password);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="idNumber"></param>
        /// <param name="phoneNumber"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        bool SignUp(string username, string idNumber, string phoneNumber, string password);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        bool Login(string username, string password);
        /// <summary>
        /// 
        /// </summary>
        void Logout();
    }
}
