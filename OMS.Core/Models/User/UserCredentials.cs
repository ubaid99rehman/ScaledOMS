
namespace OMS.Core.Models.User
{
    public class UserCredentials
    {
        public string Username;
        public string Password;
        //Constructor
        public UserCredentials(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }
}
