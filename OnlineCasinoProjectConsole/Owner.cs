using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCasinoProjectConsole
{
    class Owner
    {
        [JsonProperty]
        string username { get; set; }
        [JsonProperty]
        string password { get; set; }

        bool loginBool;
        internal static bool prizeModuleBool { get; private set; }
        
        static List<Owner> ownerList = new List<Owner>();

        public bool ownerLogin(string username, string password)
        {
            if (JsonConvert.DeserializeObject<List<Owner>>(File.ReadAllText("Owner.json")) == null)
            {
                Console.WriteLine("Invalid username or password.");
                loginBool = false;
            }
            else
            {
                ownerList = JsonConvert.DeserializeObject<List<Owner>>(File.ReadAllText("Owner.json"));
                foreach (Owner owner in ownerList)
                {
                    if (Equals(owner.username, username))
                    {
                        if (Equals(owner.password, password))
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

        public void setPrizeModule(int givePrize)
        {
            if (givePrize == 1)
            {
                prizeModuleBool = true;
                Console.WriteLine("Prize giving module has been activated.");
            }
            if (givePrize == 2)
            {
                prizeModuleBool = false;
                Console.WriteLine("Prize giving module has been deactivated.");
            }
            else
            {
                Console.WriteLine("Invalid input");
            }
        }

    }
}
