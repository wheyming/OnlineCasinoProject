using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCasinoProjectConsole
{
    public class UserAuthentication
    {
        bool loginBool;
        bool duplicatebool;
        List<Gambler> gamblerList = new List<Gambler>();

        private IFileHandling _fileHandling;

        public UserAuthentication(IFileHandling fileHandling)
        {
            _fileHandling = fileHandling;
        }

        /// <summary>
        /// Criteria to check:
        /// Between 6 - 24
        /// No spaces
        /// </summary>
        /// <param name="username"></param>
        /// <returns> bool: To check if we input is valid. </returns>
        public bool checkUsername(string username)
        {
            try
            {
                duplicatebool = false;
                if (username.Length < 6 || username.Length > 24)
                {
                    Console.WriteLine("Please create a username between 6 to 24 characters.");
                    duplicatebool = true;
                }
                foreach (char character in username)
                {
                    if (char.IsWhiteSpace(character))
                    {
                        Console.WriteLine("Please create a username without space.");
                        duplicatebool = true;
                        break;
                    }
                }
                if (JsonConvert.DeserializeObject<List<Gambler>>(_fileHandling.readAllText("Gambler.json")) == null)
                {
                }
                else
                {
                    gamblerList = JsonConvert.DeserializeObject<List<Gambler>>(_fileHandling.readAllText("Gambler.json"));
                    foreach (Gambler gambleruser in gamblerList)
                    {
                        if (Equals(gambleruser.username, username))
                        {
                            Console.WriteLine("Duplicate username.");
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
                if (JsonConvert.DeserializeObject<List<Gambler>>(_fileHandling.readAllText("Gambler.json")) == null)
                {
                }
                else
                {
                    gamblerList = JsonConvert.DeserializeObject<List<Gambler>>(_fileHandling.readAllText("Gambler.json"));
                    foreach (Gambler gambleruser in gamblerList)
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
                if (JsonConvert.DeserializeObject<List<Gambler>>(_fileHandling.readAllText("Gambler.json")) == null)
                {
                }
                else
                {
                    gamblerList = JsonConvert.DeserializeObject<List<Gambler>>(_fileHandling.readAllText("Gambler.json"));
                    foreach (Gambler gambleruser in gamblerList)
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


        public string signup(string username, string idNumber, string phoneNumber, string password)
        {
            try
            {
                if (JsonConvert.DeserializeObject<List<Gambler>>(_fileHandling.readAllText("Gambler.json")) == null)
                {
                    Gambler gambler = new Gambler(username, idNumber, phoneNumber, password);
                    gamblerList.Add(gambler);
                    string gamblerListStr = JsonConvert.SerializeObject(gamblerList);
                    _fileHandling.writeAllText("Gambler.json", gamblerListStr);
                    return gamblerListStr;
                }
                else
                {
                    gamblerList = JsonConvert.DeserializeObject<List<Gambler>>(_fileHandling.readAllText("Gambler.json"));
                    Gambler gambler = new Gambler(username, idNumber, phoneNumber, password);
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


        public bool login(string username, string password)
        {
            try
            {
                if (JsonConvert.DeserializeObject<List<Gambler>>(_fileHandling.readAllText("Gambler.json")) == null)
                {
                    Console.WriteLine("Invalid username or password.");
                    loginBool = false;
                }
                else
                {
                    gamblerList = JsonConvert.DeserializeObject<List<Gambler>>(_fileHandling.readAllText("Gambler.json"));
                    foreach (Gambler gambler in gamblerList)
                    {
                        if (Equals(gambler.username, username))
                        {
                            if (Equals(gambler.password, password))
                            {
                                loginBool = true;
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


        //linQ
        //var user = gamblerList.First(x => x.username == username);
        //if(user != null)
        //    {
        //    }
    }
}
