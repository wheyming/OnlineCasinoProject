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
        bool duplicatebool;
        static List<Gambler> gamblerList = new List<Gambler>();

        public bool checkUsername(string username)
        {
            if (JsonConvert.DeserializeObject<List<Gambler>>(File.ReadAllText("Gambler.json")) == null)
            {
                duplicatebool = false;
            }
            else
            {
                gamblerList = JsonConvert.DeserializeObject<List<Gambler>>(File.ReadAllText("Gambler.json"));
                duplicatebool = false;
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

        public bool checkID(string idNumber)
        {
            if (JsonConvert.DeserializeObject<List<Gambler>>(File.ReadAllText("Gambler.json")) == null)
            {
                duplicatebool = false;
            }
            else
            {
                duplicatebool = false;
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

        public bool checkPhoneNumber(string phoneNumber)
        {
            if (JsonConvert.DeserializeObject<List<Gambler>>(File.ReadAllText("Gambler.json")) == null)
            {
                duplicatebool = false;
            }
            else
            {
                duplicatebool = false;
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

        public void signup(string username, string idNumber, string phoneNumber, string password)
        {
            if (JsonConvert.DeserializeObject<List<Gambler>>(File.ReadAllText("Gambler.json")) == null)
            {
                Gambler gambler = new Gambler(username, idNumber, phoneNumber, password);
                gamblerList.Add(gambler);
                string gamblerListStr = JsonConvert.SerializeObject(gamblerList);
                File.WriteAllText("Gambler.json", gamblerListStr);
            }
            else
            {
                gamblerList = JsonConvert.DeserializeObject<List<Gambler>>(File.ReadAllText("Gambler.json"));
                Gambler gambler = new Gambler(username, idNumber, phoneNumber, password);
                gamblerList.Add(gambler);
                string gamblerListStr = JsonConvert.SerializeObject(gamblerList);
                File.WriteAllText("Gambler.json", gamblerListStr);
            }
        }

        public bool login(string username, string password)
        {
            if (JsonConvert.DeserializeObject<List<Gambler>>(File.ReadAllText("Gambler.json")) == null)
            {
                Console.WriteLine("Invalid username or password.");
                loginBool = false;
            }
            else
            {
                gamblerList = JsonConvert.DeserializeObject<List<Gambler>>(File.ReadAllText("Gambler.json"));
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


        //linQ
        //var user = gamblerList.First(x => x.username == username);
        //if(user != null)
        //    {
        //    }
    }
}
