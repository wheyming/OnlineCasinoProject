using System.ComponentModel.DataAnnotations;

namespace Casino.WebAPI.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class User
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public string UserName { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public string IDNumber { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public string PhoneNumber { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public string Password { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public bool IsOwner { get; private set; }

        /// <summary>
        /// Default constructor required to prevent error.
        /// </summary>
        public User()
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="idNumber"></param>
        /// <param name="phoneNumber"></param>
        /// <param name="password"></param>
        public User(string username, string idNumber, string phoneNumber, string password)
        {
            UserName = username;
            IDNumber = idNumber;
            PhoneNumber = phoneNumber;
            Password = password;
            IsOwner = false;
        }

        public User(string username, string idNumber, string phoneNumber, string password, bool isOwner)
        {
            UserName = username;
            IDNumber = idNumber;
            PhoneNumber = phoneNumber;
            Password = password;
            IsOwner = isOwner;
        }
    }
}
