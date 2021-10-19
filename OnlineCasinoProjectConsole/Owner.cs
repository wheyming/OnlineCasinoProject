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

        public void setPrizeModule(string givePrize)
        {
            if (givePrize.ToUpper() == "YES")
            {
                prizeModuleBool = true;
            }
            if (givePrize.ToUpper() == "NO")
            {
                prizeModuleBool = false;
            }
            else
            {
                Console.WriteLine("Invalid input");
            }
        }

    }
}
