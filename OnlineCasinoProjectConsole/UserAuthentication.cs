using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace OnlineCasinoProjectConsole
{
    /// <summary>
    /// 
    /// </summary>
    public class UserAuthentication : IUserAuthentication
    {
        private IFileHandling _fileHandling;
        public UserAuthentication(IFileHandling fileHandling)
        {
            _fileHandling = fileHandling;
        }

        public User CurrentUser { get; private set; }

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
                foreach (User gambleruser in JsonConvert.DeserializeObject<List<User>>(_fileHandling.readAllText("User.json")))
                {
                    if (Equals(gambleruser.UserName, username))
                    {
                        type = UserNameResultType.DuplicateUser;
                        break;
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
                foreach (User gambleruser in JsonConvert.DeserializeObject<List<User>>(_fileHandling.readAllText("User.json")))
                {
                    if (Equals(gambleruser.IDNumber, idNumber))
                    {
                        type = IdResultType.DuplicateId;
                        break;
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
                        continue;
                    }
                    else
                    {
                        type = PhoneNumberResultType.PhoneNumberIncorrect;
                        break;
                    }
                }
                foreach (User gambleruser in JsonConvert.DeserializeObject<List<User>>(_fileHandling.readAllText("User.json")))
                {
                    if (Equals(gambleruser.PhoneNumber, phoneNumber))
                    {
                        type = PhoneNumberResultType.DuplicatePhoneNumber;
                        break;
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
                char[] specialchar = new char[] { '!', '@', '#', '$', '%', '^', '&', '*', '(', ')', '+', '=', '_', '-', '{', '}', '[', ']', ':', ';', '"', '\'', '?', '<', '>', ',', '.' };

                if (password.Length < 6 || password.Length > 24)
                {
                    type |= PasswordResultType.IncorrectPasswordLength;
                }
                int j = 0;
                int k = 0;
                int l = 0;
                for (int i = 0; i < password.Length; i++)
                {
                    if (Char.IsUpper(password[i]))
                    {
                        j++;
                    }
                    if (Char.IsLower(password[i]))
                    {
                        k++;
                    }
                    if (Char.IsDigit(password[i]))
                    {
                        l++;
                    }
                    else
                    {
                        continue;
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
                    for (int i = 0; i < specialchar.Length; i++)
                    {
                        if (c == specialchar[i])
                        {
                            q = false;
                            break;
                        }
                    }
                }
                if (q == true)
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
                if (JsonConvert.DeserializeObject<List<User>>(_fileHandling.readAllText("User.json")) == null)
                {
                    User gambler = new User(username, idNumber, phoneNumber, password);
                    gamblerList.Add(gambler);
                    string gamblerListStr = JsonConvert.SerializeObject(gamblerList);
                    _fileHandling.writeAllText("User.json", gamblerListStr);
                }
                else
                {
                    gamblerList = JsonConvert.DeserializeObject<List<User>>(_fileHandling.readAllText("User.json"));
                    User gambler = new User(username, idNumber, phoneNumber, password);
                    gamblerList.Add(gambler);
                    string gamblerListStr = JsonConvert.SerializeObject(gamblerList);
                    _fileHandling.writeAllText("User.json", gamblerListStr);
                }
                return true;
            }
            catch (IOException)
            {
                return false;
            }
        }

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
                if (JsonConvert.DeserializeObject<List<User>>(_fileHandling.readAllText("User.json")) == null)
                {
                    return false;
                }
                else
                {
                    foreach (User user in JsonConvert.DeserializeObject<List<User>>(_fileHandling.readAllText("User.json")))
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
                    return false;
                }
            }
            catch (IOException)
            {
                return false;
            }
        }

        public void Logout()
        {
            CurrentUser = null;
        }

    }
}
