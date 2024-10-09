using DevExpress.Mvvm;
using OMS.Core.Logging;
using OMS.Core.Models.User;
using OMS.Core.Services.AppServices;
using System;
using System.Threading.Tasks;

namespace OMS.ViewModels
{
    public class LoadingViewModel : ViewModelBase
    {
        //Services
        private ILogHelper Logger;
        private IAuthService AuthService;
        private IUserService UserService;

        //Events
        public event Action AuthenticationCompleted;

        #region Private Member
        private bool _isAuthenticated;
        private string _authMessage;
        private string _username;
        public bool IsAuthenticated
        {
            get { return _isAuthenticated; }
            set { SetProperty(ref _isAuthenticated, value, nameof(IsAuthenticated)); }

        }
        private string _password;
        #endregion

        //Public Members
        public string AuthMessage
        {
            get { return _authMessage; }
            set { SetProperty(ref _authMessage, value, nameof(AuthMessage)); }
        }
        public string Username
        {
            get => _username;
            set
            {
                SetProperty(ref _username, value, nameof(Username));
            }
        }
        public string Password
        {
            get => _password;
            set
            {
                SetProperty(ref _password, value, nameof(Password));
            }
        }

        //Constructor
        public LoadingViewModel(IAuthService authService, 
            ILogHelper logHelper, 
            IUserService userService)
        {
            Logger = logHelper;
            IsAuthenticated = false;
            AuthService = authService;
            UserService = userService;
        }

        //Methods
        public async Task Login()
        {
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
            await AuthenticateAsync();
        }
        public async Task AuthenticateAsync()
        {
            Logger.LogInfo("Authenticating User: " + Username);
            UserCredentials credentials = new UserCredentials(this.Username, this.Password);

            // Run authentication in a background task to avoid blocking the UI thread.
            var user = await Task.Run(() => AuthService.Authenticate(credentials));

            if (user != null)
            {
                IsAuthenticated = true;
                AuthMessage = "Authenticated!";
                UserService.SetUser(user);
                Logger.LogInfo("User: " + Username + " Authenticated.");
            }
            else
            {
                IsAuthenticated = false;
                AuthMessage = "User or Password Not Matched!";
                Logger.LogInfo("User: " + Username + " is not Authenticated.");
            }

            // Notify authentication completion
            AuthenticationComplete();
        }

        public void AuthenticationComplete()
        {
            AuthenticationCompleted?.Invoke();   
        }
    }
}
