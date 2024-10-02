﻿using DevExpress.Mvvm;
using OMS.Core.Logging;
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
        //Services
        private IAuthService AuthService;
        private ILogHelper Logger;
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
        public LoadingViewModel(IAuthService authService, ILogHelper logHelper)
        {
            Logger = logHelper;
            IsAuthenticated = false;
            AuthService = authService;
        }

        //Methods
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
            Logger.LogInfo("Authenticating User: " + Username);
            var user = AuthService.Authenticate(new UserCredentials(this.Username,this.Password));
            
            if (user != null)
            {
                IsAuthenticated = true;
                AuthMessage = "Authenticated!";
                AuthenticationComplete();
                Logger.LogInfo("User: " + Username + " Authenticated.");
            }
            else
            {
                IsAuthenticated = false;
                AuthMessage = "User or Password Not Matched!";
                AuthenticationComplete();
                Logger.LogInfo("User: " + Username + " is not Authenticated.");
            }
        } 
        public void AuthenticationComplete()
        {
            AuthenticationCompleted?.Invoke();   
        }
    }
}
