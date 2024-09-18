using Microsoft.Extensions.DependencyInjection;
using OMS.ViewModels;
using System.Threading.Tasks;
using System.Windows;

namespace OMS
{
    public partial class LoadingWindow : Window
    {
        public LoadingWindow()
        {
            InitializeComponent();
            this.DataContext = AppServiceProvider.GetServiceProvider().GetRequiredService<LoadingViewModel>();
        }

        private async void Login_Click(object sender, RoutedEventArgs e)
        {
            // Show loading panel
            LoginPanel.Visibility = Visibility.Collapsed;
            LoadingPanel.Visibility = Visibility.Visible;

            bool isAuthenticated = false;

            if (this.DataContext is LoadingViewModel viewModel)
            {
                viewModel.Username = txtUsername.Text;
                viewModel.Password = txtPassword.Password.ToString();

                isAuthenticated = await Task.Run(() => viewModel.Login());

                if (isAuthenticated)
                {
                    // Login success
                    var mainWindow = AppServiceProvider.GetServiceProvider().GetRequiredService<MainWindow>();
                    mainWindow.Show();
                    this.Close();
                }
                else
                {
                    if (string.IsNullOrEmpty(viewModel.AuthMessage))
                    {
                        viewModel.AuthMessage = "Invalid User or Password!";
                    }

                    MessageBox.Show(viewModel.AuthMessage, "Failed Login Attempt", MessageBoxButton.OK);

                    LoginPanel.Visibility = Visibility.Visible;
                    LoadingPanel.Visibility = Visibility.Collapsed;
                }
            }
        }

    }
}
