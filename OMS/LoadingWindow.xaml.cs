using Microsoft.Extensions.DependencyInjection;
using OMS.Core.Logging;
using OMS.ViewModels;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace OMS
{
    public partial class LoadingWindow : Window
    {
        IBootStrapper BootStrapper;
        ILogHelper Logger;
        LoadingViewModel model;

        //Constructor
        public LoadingWindow(IBootStrapper bootStrapper, ILogHelper logHelper)
        {
            Logger = logHelper;
            InitializeComponent();
            BootStrapper = bootStrapper;
            //Adding Datacontext
            model = AppServiceProvider.GetServiceProvider().GetRequiredService<LoadingViewModel>();
            this.DataContext = model;
            ((LoadingViewModel)this.DataContext).AuthenticationCompleted += LoadingWindow_AuthenticationCompleted;
        }

        //Methods
        private async void LoadingWindow_AuthenticationCompleted()
        {
            if (model.IsAuthenticated)
            {
                PART_Status.Text = "Loading Services....";
                await ShowPrgressBar();
                await LoadServices();
                PART_Status.Text = "Loading Window....";
                await Task.Delay(100);
                NavigateMainWindow();
            }
            else
            {
                MessageBox.Show(model.AuthMessage, "Login Failure", MessageBoxButton.OK);
                await ShowLogin();
            }
        }
        private async Task LoadServices()
        {
            Logger.LogInfo("Loading Services Data....");
            await BootStrapper.LoadData();
            Logger.LogInfo("Services Data Loaded.");
        }
        private void NavigateMainWindow()
        {
            var mainWindow = AppServiceProvider.GetServiceProvider().GetRequiredService<MainWindow>();
            mainWindow.Show();
            this.Close();
        }
        private async Task ShowPrgressBar()
        {
            LoginPanel.Visibility = Visibility.Collapsed;
            LoadingPanel.Visibility = Visibility.Visible;
            await Task.Delay(100);
        }
        private async Task ShowLogin()
        {
            LoginPanel.Visibility = Visibility.Visible;
            LoadingPanel.Visibility = Visibility.Collapsed;
            txtPassword.IsEnabled = true;
            txtUsername.IsEnabled = true;
            btnLogin.IsEnabled = true;  
            await Task.Delay(100); 
        } 
        //Login Button Click 
        private async void Login_Click(object sender, RoutedEventArgs e)
        {
            //await ShowPrgressBar();
            model.Username = txtUsername.Text;
            model.Password = txtPassword.Password.ToString();
            txtPassword.IsEnabled = false;
            txtUsername.IsEnabled = false;
            btnLogin.IsEnabled = false;
            await Task.Delay(100);
            App.Current.Dispatcher.Invoke((Action)(() => 
            {
                model.Login().GetAwaiter(); 
            }));
        }
    }
}
