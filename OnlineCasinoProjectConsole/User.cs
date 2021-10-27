using Newtonsoft.Json;

namespace OnlineCasinoProjectConsole
{
    public class User
    {

        [JsonProperty]
        public string username { get; private set; }
        [JsonProperty]
        public string idNumber { get; private set; }
        [JsonProperty]
        public string phoneNumber { get; private set; }
        [JsonProperty]
        public string password { get; private set; }
        [JsonProperty]
        public bool IsOwner { get; private set; }

        public User(string username, string idNumber, string phoneNumber, string password)
        {
            this.username = username;
            this.idNumber = idNumber;
            this.phoneNumber = phoneNumber;
            this.password = password;
            IsOwner = false;
        }
        public User()
        {
        }
    }
}
