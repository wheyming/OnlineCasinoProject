using Casino.Common;
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
    [RoutePrefix("api/authentication")]
    /// <summary>
    /// 
    /// </summary>
    public class AuthenticationController : ApiController, IAuthenticationManager
    {
        private string _connectionString;
        public AuthenticationController()
        {
#if DEBUG
                _connectionString = "DebugCasinoDBConnectionString";
#else
                _connectionString = "ReleaseCasinoDBConnectionString";
#endif
        }
        List<User> userList;

        /// <summary>
        /// 
        /// </summary>
        public User CurrentUser { get; private set; }

        [HttpGet]
        [Route("checkusername")]
        /// <summary>
        /// Criteria to check:
        /// Between 6 - 24
        /// No spaces
        /// </summary>
        /// <param name="username"></param>
        /// <returns> UserNameResultType: Returns CheckUserName Return Type. </returns>
        public UserNameResultType CheckUserName(string username)
        {
            UserNameResultType type = UserNameResultType.None;
            if (username == null)
            {
                type = UserNameResultType.UserNameNullError;
            }
            else if (username.Length < 6 || username.Length > 24)
            {
                type = UserNameResultType.UserNameLengthIncorrect;
            }
            else
            {
                foreach (char character in username)
                {
                    if (char.IsWhiteSpace(character))
                    {
                        type = UserNameResultType.UserNameContainsSpace;
                        break;
                    }
                }
                User user = null;
                using (CasinoContext casinoContext = new CasinoContext(_connectionString))
                {
                    user = casinoContext.Users.Where(x => x.UserName.ToLower() == username.ToLower()).FirstOrDefault();
                }
                if (user != null)
                {
                    type = UserNameResultType.DuplicateUser;
                }
            }
            return type;
        }
        [HttpGet]
        [Route("checkid")]
        /// <summary>
        /// 'S' or 'T' for first alphabet
        /// Length is within 9
        /// Final is an alphabet
        /// </summary>
        /// <param name="idNumber"></param>
        /// <returns> bool: To check if we input is valid. </returns>
        public IdResultType CheckId(string idNumber)
        {
            IdResultType type = IdResultType.None;
            if (idNumber == null)
            {
                type = IdResultType.IdNullError;
            }
            else if (idNumber.Length != 9 || (!Equals(char.ToUpper(idNumber[0]), 'S') && !Equals(char.ToUpper(idNumber[0]), 'T')) || !char.IsLetter(idNumber[8]))
            {
                type = IdResultType.IdIncorrect;
            }
            else
            {
                User user = null;
                using (CasinoContext casinoContext = new CasinoContext(_connectionString))
                {
                    user = casinoContext.Users.Where(x => x.IDNumber.ToLower() == idNumber.ToLower()).FirstOrDefault();
                }
                if (user != null)
                {
                    type = IdResultType.DuplicateId;
                }
            }
            return type;
        }
        [HttpGet]
        [Route("checkphonenumber")]
        /// <summary>
        /// 8 Numbers
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <returns> bool: To check if we input is valid. </returns>
        public PhoneNumberResultType CheckPhoneNumber(string phoneNumber)
        {
            PhoneNumberResultType type = PhoneNumberResultType.None;
            if (phoneNumber == null)
            {
                type = PhoneNumberResultType.PhoneNumberNullError;
            }
            else if (phoneNumber.Length != 8)
            {
                type = PhoneNumberResultType.PhoneNumberIncorrect;
            }
            else
            {
                foreach (char character in phoneNumber)
                {
                    if (char.IsDigit(character))
                    {
                    }
                    else
                    {
                        type = PhoneNumberResultType.PhoneNumberIncorrect;
                        break;
                    }
                }
                User user = null;
                using (CasinoContext casinoContext = new CasinoContext(_connectionString))
                {
                    user = casinoContext.Users.Where(x => x.PhoneNumber.ToLower() == phoneNumber.ToLower()).FirstOrDefault();
                }
                if (user != null)
                {
                    type = PhoneNumberResultType.DuplicatePhoneNumber;
                }
            }
            return type;
        }
        [HttpGet]
        [Route("checkpassword")]
        /// <summary>
        /// At least 1 upper, lower, number and special character
        /// No 3 consecutive same
        /// </summary>
        /// <param name="password"></param>
        /// <returns> bool: To check if we input is valid. </returns>
        public PasswordResultType CheckPassword(string password)
        {
            PasswordResultType type = PasswordResultType.None;
            char[] specialChars = new char[] { '!', '@', '#', '$', '%', '^', '&', '*', '(', ')', '+', '=', '_', '-', '{', '}', '[', ']', ':', ';', '"', '\'', '?', '<', '>', ',', '.' };

            if (password == null)
            {
                type = PasswordResultType.PasswordNullError;
            }
            else
            {
                if (password.Length < 6 || password.Length > 24)
                {
                    type |= PasswordResultType.IncorrectPasswordLength;
                }
                int j = 0;
                int k = 0;
                int l = 0;
                foreach (var t in password)
                {
                    if (char.IsUpper(t))
                    {
                        j++;
                    }
                    if (char.IsLower(t))
                    {
                        k++;
                    }
                    if (char.IsDigit(t))
                    {
                        l++;
                    }
                }
                if (j == 0)
                {
                    type |= PasswordResultType.PasswordNoUpperCaseLetter;
                }
                if (k == 0)
                {
                    type |= PasswordResultType.PasswordNoLowerCaseLetter;
                }
                if (l == 0)
                {
                    type |= PasswordResultType.PasswordNoDigits;
                }
                bool q = true;
                foreach (char c in password)
                {
                    foreach (var t in specialChars)
                    {
                        if (c == t)
                        {
                            q = false;
                            break;
                        }
                    }
                }
                if (q)
                {
                    type |= PasswordResultType.PasswordThreeRepeatedCharacters;
                }
                for (int i = 0; i < (password.Length - 2); i++)
                {
                    if (Equals(password[i], password[i + 1]) && Equals(password[i + 1], password[i + 2]))
                    {
                        type |= PasswordResultType.PasswordThreeRepeatedCharacters;
                    }
                }
            }
            return type;
        }
        [HttpGet]
        [Route("signup")]
        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="idNumber"></param>
        /// <param name="phoneNumber"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool SignUp(string username, string idNumber, string phoneNumber, string password)
        {
            try
            {
                using (CasinoContext casinoContext = new CasinoContext(_connectionString))
                {
                    User gambler = new User(username, idNumber, phoneNumber, password);
                    casinoContext.Users.Add(gambler);
                    casinoContext.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        [HttpGet]
        [Route("login")]
        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public (bool?, bool?) Login(string username, string password)
        {
            bool? loginSuccess = false;
            bool? isOwner;
            try
            {
                using (CasinoContext casinoContext = new CasinoContext(_connectionString))
                {
                    var getUserList = casinoContext.Users.ToListAsync();
                    getUserList.Wait();
                    userList = getUserList.Result;
                }
                if (userList != null)
                {
                    foreach (User user in userList)
                        if (Equals(user.UserName, username))
                            if (Equals(user.Password, password))
                            {
                                CurrentUser = user;
                                loginSuccess = true;
                                break;
                            }
                }
                if (CurrentUser != null)
                    isOwner = CurrentUser.IsOwner;
                else
                    isOwner = null;
            }
            catch (IOException)
            {
                loginSuccess = null;
                isOwner = null;
            }
            return (loginSuccess, isOwner);
        }
        [HttpGet]
        [Route("logout")]
        /// <summary>
        /// 
        /// </summary>
        public void Logout()
        {
            CurrentUser = null;
        }
    }
}
