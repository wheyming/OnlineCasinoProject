namespace OnlineCasinoProjectConsole
{
    public interface IUserAuthentication
    {
        User CurrentUser { get; }
        UserNameResultType CheckUserName(string username);
        IdResultType CheckId(string idNumber);
        PhoneNumberResultType CheckPhoneNumber(string phoneNumber);
        PasswordResultType CheckPassword(string password);
        bool SignUp(string username, string idNumber, string phoneNumber, string password);
        bool Login(string username, string password);
        void Logout();
    }
}
