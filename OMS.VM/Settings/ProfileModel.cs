using DevExpress.Mvvm;
using OMS.Core.Core.Models.User;
using OMS.Core.Services.AppServices;

namespace OMS.VM.Settings
{
    public class ProfileModel : ViewModelBase
    {
        IUserService UserService;

        private string _email;
        private string _password;
        public string Password
        {
            get { return _password; }
            set
            {
                SetProperty(ref _password, value, nameof(Password));

            }
        }
        public string Email
        {
            get { return _email; }
            set
            {
                SetProperty(ref _email, value, nameof(Email));

            }
        }

        private User _user;
        

        public ProfileModel(IUserService userService) 
        {
            UserService = userService;
            _user = UserService.GetUser();
            Password = _user.Password;   
            Email = _user.Email;
        }

        public bool UpdateUser(out string message)
        {
            _user.Password = Password;
            _user.Email = Email;
            bool isupdated = UserService.UpdateUser(_user);
            if(isupdated)
            {
                message = "User Updated!";
                return true;
            }
            message = "Unable to Update User!";
            return false;

        }
    }
}
