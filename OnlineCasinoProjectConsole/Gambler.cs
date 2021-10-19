using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCasinoProjectConsole
{
    public class Gambler
    { 

        [JsonProperty]
        public string username { get; private set; }
        [JsonProperty]
        public string password { get; private set; }
        [JsonProperty]
        public string phoneNumber { get; private set; }
        [JsonProperty]
        public string idNumber { get; private set; }

        public Gambler(string username, string password, string idNumber, string phoneNumber)
        {
            this.username = username;
            this.password = password;
            this.idNumber = idNumber;
            this.phoneNumber = phoneNumber;
        }
        public Gambler()
        {
        }
    }
}
