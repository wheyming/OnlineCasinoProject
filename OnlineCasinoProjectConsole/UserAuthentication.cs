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
        bool duplicatebool;

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
        public UserNameResultType checkUsername(string username)
        {
            UserNameResultType type = UserNameResultType.None;
            try
            {
                duplicatebool = false;
                if (username.Length < 6 || username.Length > 24)
                {
                    type = UserNameResultType.UserNameLengthtIncorrect;
                }
                foreach (char character in username)
                {
                    if (char.IsWhiteSpace(character))
                    {
                        type = UserNameResultType.UserNameContainsSpace;
                        break;
                    }
                }
                foreach (User gambleruser in JsonConvert.DeserializeObject<List<User>>(_fileHandling.readAllText("Gambler.json")))
                {
                    if (Equals(gambleruser.username, username))
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
        public bool checkID(string idNumber)
        {
            try
            {
                duplicatebool = false;
                if (idNumber.Length != 9 || (!Equals(char.ToUpper(idNumber[0]), 'S') && !Equals(char.ToUpper(idNumber[0]), 'T')) || !Char.IsLetter(idNumber[8]))
                {
                    duplicatebool = true;
                    Console.WriteLine("Invalid ID Number");
                }
                if (JsonConvert.DeserializeObject<List<User>>(_fileHandling.readAllText("Gambler.json")) == null)
                {
                }
                else
                {
                    gamblerList = JsonConvert.DeserializeObject<List<User>>(_fileHandling.readAllText("Gambler.json"));
                    foreach (User gambleruser in gamblerList)
                    {
                        if (Equals(gambleruser.idNumber, idNumber))
                        {
                            Console.WriteLine("Duplicate idNumber.");
                            duplicatebool = true;
                            break;
                        }
                    }
                }
                return duplicatebool;
            }
            catch (IOException)
            {
                Console.WriteLine("Unable to find file.");
                duplicatebool = true;
                return duplicatebool;
            }
            catch (NullReferenceException)
            {
                Console.WriteLine("Null input.");
                duplicatebool = true;
                return duplicatebool;
            }
        }

        /// <summary>
        /// 8 Numbers
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <returns> bool: To check if we input is valid. </returns>
        public bool checkPhoneNumber(string phoneNumber)
        {
            try
            {
                duplicatebool = false;
                if (phoneNumber.Length != 8)
                {
                    Console.WriteLine("Invalid phone number");
                    duplicatebool = true;
                }
                foreach (char character in phoneNumber)
                {
                    if (char.IsDigit(character))
                    {
                        continue;
                    }
                    else
                    {
                        duplicatebool = true;
                        break;
                    }
                }
                if (JsonConvert.DeserializeObject<List<User>>(_fileHandling.readAllText("Gambler.json")) == null)
                {
                }
                else
                {
                    foreach (User gambleruser in JsonConvert.DeserializeObject<List<User>>(_fileHandling.readAllText("Gambler.json")))
                    {
                        if (Equals(gambleruser.phoneNumber, phoneNumber))
                        {
                            Console.WriteLine("Duplicate Phone Number.");
                            duplicatebool = true;
                            break;
                        }
                    }
                }
                return duplicatebool;
            }
            catch (IOException)
            {
                Console.WriteLine("Unable to find file.");
                duplicatebool = true;
                return duplicatebool;
            }
            catch (NullReferenceException)
            {
                Console.WriteLine("Null input.");
                duplicatebool = true;
                return duplicatebool;
            }
        }

        /// <summary>
        /// At least 1 upper, lower, number and special character
        /// No 3 consecutive same
        /// </summary>
        /// <param name="password"></param>
        /// <returns> bool: To check if we input is valid. </returns>
        public bool checkPassword(string password)
        {
            try
            {
                char[] specialchar = new char[] { '!', '@', '#', '$', '%', '^', '&', '*', '(', ')', '+', '=', '_', '-', '{', '}', '[', ']', ':', ';', '"', '\'', '?', '<', '>', ',', '.' };
                duplicatebool = false;

                if (password.Length < 6 || password.Length > 24)
                {
                    Console.WriteLine("Please create a password between 6 to 24 characters.");
                    duplicatebool = true;
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
                    duplicatebool = true;
                }
                if (k == 0)
                {
                    Console.WriteLine("Please create a password with at least one lowercase letter.");
                    duplicatebool = true;
                }
                if (l == 0)
                {
                    Console.WriteLine("Please create a password with at least one digit.");
                    duplicatebool = true;
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
                    Console.WriteLine("Please create a password with at least one special character.");
                    duplicatebool = true;
                }
                for (int i = 0; i < (password.Length - 2); i++)
                {
                    if (Equals(password[i], password[i + 1]) && Equals(password[i + 1], password[i + 2]))
                    {
                        Console.WriteLine("Please create a password with maximum of 2 repeated characters.");
                        duplicatebool = true;
                    }
                }
                return duplicatebool;
            }
            catch (NullReferenceException)
            {
                Console.WriteLine("Null input.");
                duplicatebool = true;
                return duplicatebool;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="idNumber"></param>
        /// <param name="phoneNumber"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public string signup(string username, string idNumber, string phoneNumber, string password)
        {
            try
            {
                if (JsonConvert.DeserializeObject<List<User>>(_fileHandling.readAllText("Gambler.json")) == null)
                {
                    User gambler = new User(username, idNumber, phoneNumber, password);
                    gamblerList.Add(gambler);
                    string gamblerListStr = JsonConvert.SerializeObject(gamblerList);
                    _fileHandling.writeAllText("Gambler.json", gamblerListStr);
                    return gamblerListStr;
                }
                else
                {
                    gamblerList = JsonConvert.DeserializeObject<List<User>>(_fileHandling.readAllText("Gambler.json"));
                    User gambler = new User(username, idNumber, phoneNumber, password);
                    gamblerList.Add(gambler);
                    string gamblerListStr = JsonConvert.SerializeObject(gamblerList);
                    _fileHandling.writeAllText("Gambler.json", gamblerListStr);
                    return gamblerListStr;
                }
            }
            catch (IOException)
            {
                Console.WriteLine("File does not exist, contact administrator.");
                return "";
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool login(string username, string password)
        {
            try
            {
                if (JsonConvert.DeserializeObject<List<User>>(_fileHandling.readAllText("Gambler.json")) == null)
                {
                    Console.WriteLine("Invalid username or password.");
                    loginBool = false;
                }
                else
                {
                    foreach (User gambler in JsonConvert.DeserializeObject<List<User>>(_fileHandling.readAllText("Gambler.json")))
                    {
                        if (Equals(gambler.username, username))
                        {
                            if (Equals(gambler.password, password))
                            {
                                CurrentUser = gambler;
                                break;
                            }
                        }
                        else
                        {
                            loginBool = false;
                        }
                    }
                }
                if (loginBool == false)
                {
                    Console.WriteLine("Invalid username or password.");
                }
                return loginBool;
            }
            catch (IOException)
            {
                Console.WriteLine("File does not exist, contact administrator.");
                loginBool = false;
                return loginBool;
            }
        }
    
    }
}
