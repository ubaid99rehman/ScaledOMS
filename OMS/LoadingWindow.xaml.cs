using Microsoft.Extensions.DependencyInjection;
using OMS.Core.Logging;
using OMS.ViewModels;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace OMS
{
    public partial class LoadingWindow : Window
    {
        private IBootStrapper BootStrapper;
        private ILogHelper Logger;
        private LoadingViewModel model;

        // Constructor
        public LoadingWindow(IBootStrapper bootStrapper, ILogHelper logHelper)
        {
            Logger = logHelper;
            InitializeComponent();
            BootStrapper = bootStrapper;

            // Adding DataContext
            model = AppServiceProvider.GetServiceProvider().GetRequiredService<LoadingViewModel>();
            this.DataContext = model;
            model.AuthenticationCompleted += LoadingWindow_AuthenticationCompleted;
        }

        // Methods
        private async void LoadingWindow_AuthenticationCompleted()
        {
            try
            {
                if (model.IsAuthenticated)
                {
                    PART_Status.Text = "Loading Services....";
                    await ShowProgressBar();
                    await Task.Run(async () => await LoadServices());

                    PART_Status.Text = "Loading Window....";
                    await Task.Delay(100);

                    await NavigateMainWindowAsync();
                }
                else
                {
                    MessageBox.Show(model.AuthMessage, "Login Failure", MessageBoxButton.OK);
                    await ShowLogin();
                }
            }
            catch (Exception ex)
            {
                Logger.LogError("An error occurred during authentication: ", ex);
                MessageBox.Show("An error occurred, please try again later.", "Error", MessageBoxButton.OK);
            }
        }

        private async Task NavigateMainWindowAsync()
        {
            PART_Status.Text = "Loading Window....";
            await Task.Delay(500);

            var mainWindow = AppServiceProvider.GetServiceProvider().GetRequiredService<MainWindow>();
            mainWindow.Show();
            this.Close();
        }

        private async Task LoadServices()
        {
            Logger.LogInfo("Loading Services Data....");
            await BootStrapper.LoadData();
            Logger.LogInfo("Services Data Loaded.");
        }

        private async Task ShowProgressBar()
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

        // Login Button Click 
        private async void Login_Click(object sender, RoutedEventArgs e)
        {
            model.Username = txtUsername.Text;
            model.Password = txtPassword.Password;

            // Disable login controls to prevent user interaction during login.
            txtPassword.IsEnabled = false;
            txtUsername.IsEnabled = false;
            btnLogin.IsEnabled = false;

            // Show the progress bar.
            await ShowProgressBar();

            // Perform the login asynchronously.
            await model.AuthenticateAsync();
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            model.AuthenticationCompleted -= LoadingWindow_AuthenticationCompleted;
        }
    }
}

