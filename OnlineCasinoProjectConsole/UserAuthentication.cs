using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCasinoProjectConsole
{
    class UserAuthentication
    {
        bool loginBool;
        static bool duplicatebool;
        static List<Gambler> gamblerList = new List<Gambler>();
        public string checkDuplicateID(string idNumber)
        {
            do
            {
                duplicatebool = false;
                foreach (Gambler gambleruser in gamblerList)
                {
                    if (Equals(gambleruser.idNumber, idNumber))
                    {
                        Console.WriteLine("Duplicate idNumber.");
                        duplicatebool = true;
                    }
                }
            } while (duplicatebool == true);
            return idNumber;
        }
        public string checkDuplicateUsername()
        {
            string username;
            do
            {
                duplicatebool = false;
                Console.WriteLine("Please input username.");
                username = Console.ReadLine();
                foreach (Gambler gambleruser in gamblerList)
                {
                    if (Equals(gambleruser.username, username))
                    {
                        Console.WriteLine("Duplicate username.");
                        duplicatebool = true;
                    }
                }
            } while (duplicatebool == true);
            return username;
        }
        public string checkDuplicatePhoneNumber()
        {
            string phoneNumber;
            do
            {
                duplicatebool = false;
                Console.WriteLine("Please input phone number.");
                phoneNumber = Console.ReadLine();
                foreach (Gambler gambleruser in gamblerList)
                {
                    if (Equals(gambleruser.phoneNumber, phoneNumber))
                    {
                        Console.WriteLine("Duplicate Phone Number.");
                        duplicatebool = true;
                    }
                }
            } while (duplicatebool == true);
            return phoneNumber;
        }

        public void signup(string username, string password, string idNumber, string phoneNumber)
        {
            if (JsonConvert.DeserializeObject<List<Gambler>>(File.ReadAllText("Gambler.json")) == null)
            {
                Gambler gambler = new Gambler(username, password, idNumber, phoneNumber);
                gamblerList.Add(gambler);
                string gamblerListStr = JsonConvert.SerializeObject(gamblerList);
                File.WriteAllText("Gambler.json", gamblerListStr);
            }
            else
            {
                gamblerList = JsonConvert.DeserializeObject<List<Gambler>>(File.ReadAllText("Gambler.json"));
                Gambler gambler = new Gambler(username, password, idNumber, phoneNumber);
                gamblerList.Add(gambler);
                string gamblerListStr = JsonConvert.SerializeObject(gamblerList);
                File.WriteAllText("Gambler.json", gamblerListStr);
            }
        }

        public bool login(string username, string password)
        {
            if (JsonConvert.DeserializeObject<List<Gambler>>(File.ReadAllText("Gambler.json")) == null)
            {
                Console.WriteLine("No data found.");
                loginBool = false;
            }
            else
            {
                gamblerList = JsonConvert.DeserializeObject<List<Gambler>>(File.ReadAllText("Gambler.json"));
                foreach(Gambler gambler in gamblerList)
                {
                    if(Equals(gambler.username, username))
                    {
                        if(Equals(gambler.password, password))
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
            return loginBool;
        }
    }
}
