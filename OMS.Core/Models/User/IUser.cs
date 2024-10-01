
namespace OMS.Core.Models.User
{
    public interface IUser
    {
        int UserID { get; set; }
        string UserName { get; set; }
        string Email { get; set; }
        bool UserStatus { get; set; }
        string Password { get; set; }
    }
}
