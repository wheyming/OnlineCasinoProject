using Newtonsoft.Json;

namespace OnlineCasinoProjectConsole
{
    public class User
    {

        [JsonProperty]
        public string UserName { get; private set; }
        [JsonProperty]
        public string IDNumber { get; private set; }
        [JsonProperty]
        public string PhoneNumber { get; private set; }
        [JsonProperty]
        public string Password { get; private set; }
        [JsonProperty]
        public bool IsOwner { get; private set; }

        public User(string username, string idNumber, string phoneNumber, string password)
        {
            UserName = username;
            IDNumber = idNumber;
            PhoneNumber = phoneNumber;
            Password = password;
            IsOwner = false;
        }
        public User()
        {
        }
    }
}
