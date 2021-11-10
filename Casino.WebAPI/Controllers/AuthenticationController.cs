using Casino.Common;
using Casino.WebAPI.Interfaces;
using Casino.WebAPI.Models;
using Casino.WebAPI.Utility;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Http;

namespace Casino.WebAPI.Controllers
{
    [RoutePrefix("api/authentication")]
    /// <summary>
    /// 
    /// </summary>
    public class AuthenticationController : ApiController,  IAuthenticationManager
    {
        private readonly IFileManager _fileHandling;
        /// <summary>
        /// 
        /// </summary>
        internal AuthenticationController()
        {
            _fileHandling = new FileManager();
        }
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
            try
            {
                if (username.Length < 6 || username.Length > 24)
                {
                    type = UserNameResultType.UserNameLengthIncorrect;
                }
                foreach (char character in username)
                {
                    if (char.IsWhiteSpace(character))
                    {
                        type = UserNameResultType.UserNameContainsSpace;
                        break;
                    }
                }

                List<User> userList = JsonConvert.DeserializeObject<List<User>>(_fileHandling.ReadAllText("User.json"));
                if (userList != null)
                {
                    foreach (User gambler in userList)
                    {
                        if (Equals(gambler.UserName, username))
                        {
                            type = UserNameResultType.DuplicateUser;
                            break;
                        }
                    }
                }
            }
            catch (IOException)
            {
                type = UserNameResultType.UserNameDataAccessError;
            }
            catch (Exception)
            {
                type = UserNameResultType.UnhandledUserError;
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
            try
            {
                if (idNumber.Length != 9 || (!Equals(char.ToUpper(idNumber[0]), 'S') && !Equals(char.ToUpper(idNumber[0]), 'T')) || !Char.IsLetter(idNumber[8]))
                {
                    type = IdResultType.IdIncorrect;
                }

                List<User> userList = JsonConvert.DeserializeObject<List<User>>(_fileHandling.ReadAllText("User.json"));
                if (userList != null)
                {
                    foreach (User gambler in userList)
                    {
                        if (Equals(gambler.IDNumber, idNumber))
                        {
                            type = IdResultType.DuplicateId;
                            break;
                        }
                    }
                }
            }
            catch (IOException)
            {
                type = IdResultType.IdDataAccessError;
            }
            catch (Exception)
            {
                type = IdResultType.UnhandledIdError;
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
            try
            {
                if (phoneNumber.Length != 8)
                {
                    type = PhoneNumberResultType.PhoneNumberIncorrect;
                }
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

                List<User> userList = JsonConvert.DeserializeObject<List<User>>(_fileHandling.ReadAllText("User.json"));
                if (userList != null)
                {
                    foreach (User gambler in userList)
                    {
                        if (Equals(gambler.PhoneNumber, phoneNumber))
                        {
                            type = PhoneNumberResultType.DuplicatePhoneNumber;
                            break;
                        }
                    }
                }
            }
            catch (IOException)
            {
                type = PhoneNumberResultType.PhoneNumberDataAccessError;

            }
            catch (Exception)
            {
                type = PhoneNumberResultType.UnhandledPhoneNumberError;
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
            try
            {
                char[] specialChars = new char[] { '!', '@', '#', '$', '%', '^', '&', '*', '(', ')', '+', '=', '_', '-', '{', '}', '[', ']', ':', ';', '"', '\'', '?', '<', '>', ',', '.' };

                if (password.Length < 6 || password.Length > 24)
                {
                    type |= PasswordResultType.IncorrectPasswordLength;
                }
                int j = 0;
                int k = 0;
                int l = 0;
                foreach (var t in password)
                {
                    if (Char.IsUpper(t))
                    {
                        j++;
                    }
                    if (Char.IsLower(t))
                    {
                        k++;
                    }
                    if (Char.IsDigit(t))
                    {
                        l++;
                    }
                }
                if (j == 0)
                {
                    Console.WriteLine("Please create a password with at least one uppercase letter.");
                    type |= PasswordResultType.PasswordNoUpperCaseLetter;
                }
                if (k == 0)
                {
                    Console.WriteLine("Please create a password with at least one lowercase letter.");
                    type |= PasswordResultType.PasswordNoLowerCaseLetter;
                }
                if (l == 0)
                {
                    Console.WriteLine("Please create a password with at least one digit.");
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
            catch (Exception)
            {
                type |= PasswordResultType.UnhandledPasswordError;
            }
            return type;
        }
        [HttpPost]
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
                List<User> gamblerList = new List<User>();
                if (JsonConvert.DeserializeObject<List<User>>(_fileHandling.ReadAllText("User.json")) == null)
                {
                    User gambler = new User(username, idNumber, phoneNumber, password);
                    gamblerList.Add(gambler);
                    string gamblerListStr = JsonConvert.SerializeObject(gamblerList);
                    _fileHandling.WriteAllText("User.json", gamblerListStr);
                }
                else
                {
                    gamblerList = JsonConvert.DeserializeObject<List<User>>(_fileHandling.ReadAllText("User.json"));
                    User gambler = new User(username, idNumber, phoneNumber, password);
                    gamblerList?.Add(gambler);
                    string gamblerListStr = JsonConvert.SerializeObject(gamblerList);
                    _fileHandling.WriteAllText("User.json", gamblerListStr);
                }
                return true;
            }
            catch (IOException)
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
        public bool Login(string username, string password)
        {
            try
            {
                if (JsonConvert.DeserializeObject<List<User>>(_fileHandling.ReadAllText("User.json")) == null)
                {
                    return false;
                }
                else
                {
                    List<User> userList =
                        JsonConvert.DeserializeObject<List<User>>(_fileHandling.ReadAllText("User.json"));
                    if (userList != null)
                    {
                        foreach (User user in userList)
                        {
                            if (Equals(user.UserName, username))
                            {
                                if (Equals(user.Password, password))
                                {
                                    CurrentUser = user;
                                    return true;
                                }
                            }
                        }
                    }

                    return false;
                }
            }
            catch (IOException)
            {
                return false;
            }
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
        [HttpGet]
        [Route("isowner")]
        /// <summary>
        /// 
        /// </summary>  
        public bool? IsOwner()
        {
            if (CurrentUser != null)
                return CurrentUser.IsOwner;
            else
                return null;
        }
    }
}
