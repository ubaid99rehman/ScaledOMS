﻿using Microsoft.Extensions.DependencyInjection;
using OMS.Logging;
using OMS.ViewModels;
using System.Windows;

namespace OMS
{
    public partial class LoadingWindow : Window
    {
        IBootStrapper BootStrapper;

        #region Constructor
        public LoadingWindow(IBootStrapper bootStrapper)
        {
            InitializeComponent();
            BootStrapper = bootStrapper;
            //Adding Datacontext
            this.DataContext = AppServiceProvider.GetServiceProvider().GetRequiredService<LoadingViewModel>();
            ((LoadingViewModel)this.DataContext).AuthenticationCompleted += LoadingWindow_AuthenticationCompleted;
        }
        #endregion

        #region Methods
        private void LoadingWindow_AuthenticationCompleted()
        {
            if (this.DataContext is LoadingViewModel viewModel)
            {
                if (viewModel.IsAuthenticated)
                {
                    LoadServices(); 
                    NavigateMainWindow();
                }
                else
                {
                    MessageBox.Show(viewModel.AuthMessage, "Login Failure", MessageBoxButton.OK);
                    ShowLogin();
                }
            }
        }

        private void ShowPrgressBar()
        {
            LoginPanel.Visibility = Visibility.Collapsed;
            LoadingPanel.Visibility = Visibility.Visible;
        }
        private void ShowLogin()
        {
            LoginPanel.Visibility = Visibility.Visible;
            LoadingPanel.Visibility = Visibility.Collapsed;
        } 

        private void LoadServices()
        {
            PART_Status.Text = "Loading Services....";
            LogHelper.LogInfo("Loading Services Data....");
            BootStrapper.LoadData();
            LogHelper.LogInfo("Services Data Loaded.");
        }
        private void NavigateMainWindow()
        {
            var mainWindow = AppServiceProvider.GetServiceProvider().GetRequiredService<MainWindow>();
            this.Visibility = Visibility.Hidden;
            mainWindow.Show();
            this.Close();
        }
        #endregion

        #region Click Event Handler
        private void Login_Click(object sender, RoutedEventArgs e)
        {
            ShowPrgressBar();
            if (this.DataContext is LoadingViewModel viewModel)
            {
                viewModel.Username = txtUsername.Text;
                viewModel.Password = txtPassword.Password.ToString();
                viewModel.Login().GetAwaiter();
            }
        } 
        #endregion
    }
}
