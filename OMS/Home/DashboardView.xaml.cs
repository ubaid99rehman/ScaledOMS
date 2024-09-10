using Microsoft.Extensions.DependencyInjection;
using OMS.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace OMS.Home
{
    /// <summary>
    /// Interaction logic for DashboardView.xaml
    /// </summary>
    public partial class DashboardView : UserControl
    {
        public DashboardView()
        {
            InitializeComponent();
            this.DataContext = AppServiceProvider.GetServiceProvider().GetRequiredService<DashboardViewModel>();
        }

        private void UnitsRadioButton_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void VolumeRadioButton_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}
