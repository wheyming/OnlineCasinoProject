namespace OnlineCasinoProjectConsole
{
    public interface IUserAuthentication
    {
        User CurrentUser { get; }
        UserNameResultType checkUsername(string username);
        bool checkID(string idNumber);
        bool checkPhoneNumber(string phoneNumber);
        bool checkPassword(string password);
        string signup(string username, string idNumber, string phoneNumber, string password);
        bool login(string username, string password);
    }
}
