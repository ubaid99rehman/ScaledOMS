using DevExpress.Mvvm;
using System;
using System.Threading;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace OMS.ViewModels
{
    public class LoadingViewModel : ViewModelBase
    {
        public event Action AuthenticationCompleted;

        public void AuthenticationComplete()
        {
            AuthenticationCompleted?.Invoke();   
        }

        public INavigationService NavigationService 
        { 
            get 
            { 
                return this.GetService<INavigationService>(); 
            }
        }

        private bool _isAuthenticated;
        public bool IsAuthenticated
        {
            get { return _isAuthenticated;}
            set { SetProperty(ref _isAuthenticated, value, nameof(IsAuthenticated)); }
                
        }

        private string _loadingMessage;
        public string LoadingMessage
        {
            get { return _loadingMessage; }
            set { SetProperty(ref _loadingMessage, value, nameof(LoadingMessage));   }
        }

        private string _authMessage;
        public string AuthMessage
        {
            get { return _authMessage; }
            set { SetProperty(ref _authMessage, value, nameof(AuthMessage)); }
        }

        private bool _loadedServices = false;
        public bool LoadedServices
        {
            get => _loadedServices;
            set
            {
                if (_loadedServices != value)
                {
                    _loadedServices = value;
                }
            }
        }

        private string currentView;
        public string CurrentView
        {
            get { return this.currentView; }
            set { SetProperty(ref currentView, value, nameof(CurrentView)); }
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

        public LoadingViewModel() 
        {
            IsAuthenticated = false;
        }

        public void OnViewLoaded()
        {
            CurrentView = "LoginView";
            NavigationService.Navigate("LoginView", this, null, null, false);
        }

        public void Login() 
        {
            if (string.IsNullOrWhiteSpace(Username))
            {
                AuthMessage = "Username Field Cannot be Empty!";
                IsAuthenticated = false;
                AuthenticationComplete();
                return;
            }
            
            if (string.IsNullOrWhiteSpace(Password))
            {
                AuthMessage = "Password Field Cannot be Empty!";
                IsAuthenticated = false;
                AuthenticationComplete();
                return;
            }
            Authenticate();
        }

        public void Authenticate() 
        {
           
        }

        public async Task LoadServices()
        {
            await Task.Run(() =>
            {
                
                // Simulate some delay
                Thread.Sleep(1000);
                LoadedServices = true;
            });
        }

    }
}
