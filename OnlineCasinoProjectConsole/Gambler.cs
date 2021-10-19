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
        public string idNumber { get; private set; }
        [JsonProperty]
        public string phoneNumber { get; private set; }
        [JsonProperty]
        public string password { get; private set; }

        public Gambler(string username, string idNumber, string phoneNumber, string password)
        {
            this.username = username;
            this.idNumber = idNumber;
            this.phoneNumber = phoneNumber;
            this.password = password;
        }
        public Gambler()
        {
        }
    }
}
