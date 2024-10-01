using DevExpress.Mvvm;
using OMS.Core.Models.User;
using OMS.Core.Services.AppServices;
using OMS.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace OMS.ViewModels
{
    public class LoadingViewModel : ViewModelBase
    {
        private IAuthService AuthService;
        public event Action AuthenticationCompleted;
        public void AuthenticationComplete()
        {
            AuthenticationCompleted?.Invoke();   
        }

        #region Props

        private bool _isAuthenticated;
        public bool IsAuthenticated
        {
            get { return _isAuthenticated; }
            set { SetProperty(ref _isAuthenticated, value, nameof(IsAuthenticated)); }

        }

        private string _authMessage;
        public string AuthMessage
        {
            get { return _authMessage; }
            set { SetProperty(ref _authMessage, value, nameof(AuthMessage)); }
        }

        private string _username;
        public string Username
        {
            get => _username;
            set
            {
                SetProperty(ref _username, value, nameof(Username));
            }
        }

        private string _password;
        public string Password
        {
            get => _password;
            set
            {
                SetProperty(ref _password, value, nameof(Password));
            }
        }

        #endregion

        #region Constructor
        public LoadingViewModel(IAuthService authService)
        {
            IsAuthenticated = false;
            AuthService = authService;
        }
        #endregion

        #region Methods
        public async Task Login()
        {
            await Task.Delay(3000);
            //Check Null/Empty User or Password
            if (string.IsNullOrWhiteSpace(Username))
            {
                AuthMessage = "Username Field Cannot be Empty!";
                IsAuthenticated = false;
                AuthenticationComplete();
            }
            if (string.IsNullOrWhiteSpace(Password))
            {
                AuthMessage = "Password Field Cannot be Empty!";
                IsAuthenticated = false;
                AuthenticationComplete();
            }
            Authenticate();
        }

        public void Authenticate()
        {
            LogHelper.LogInfo("Authenticating User: " + Username);
            var user = AuthService.Authenticate(new UserCredentials(this.Username,this.Password));
            
            if (user != null)
            {
                IsAuthenticated = true;
                AuthMessage = "Authenticated!";
                AuthenticationComplete();
                LogHelper.LogInfo("User: " + Username + " Authenticated.");
            }
            else
            {
                IsAuthenticated = false;
                AuthMessage = "User or Password Not Matched!";
                AuthenticationComplete();
                LogHelper.LogInfo("User: " + Username + " is not Authenticated.");
            }
        } 
        #endregion

    }
}
