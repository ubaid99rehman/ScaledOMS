using DevExpress.Mvvm;
using OMS.Core.Core.Models.User;
using OMS.Core.Models.User;
using OMS.Core.Services.AppServices;

namespace OMS.VM.Settings
{
    public class ProfileModel : ViewModelBase
    {
        //Service
        IUserService UserService;

        //Private Members
        private string _email;
        private string _password;
        private IUser _user;
        
        //Public Members
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

        //Cosntructor
        public ProfileModel(IUserService userService) 
        {
            UserService = userService;
            LoadUser();
        }

        //Methods
        private void LoadUser()
        {
            var user = UserService.GetUser();
            if (user == null)
            {
                user = new User();
                user.UserID = 1;
            }
            _user = user;
            Password = _user.Password;
            Email = _user.Email;
        }
        public bool UpdateUser(out string message)
        {
            _user.Password = Password;
            _user.Email = Email;
            bool isUpdated = UserService.Update(_user);
            if(isUpdated)
            {
                message = "User Updated!";
                return true;
            }
            message = "Unable to Update User!";
            return false;
        }
    }
}
