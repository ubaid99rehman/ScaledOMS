using DevExpress.Xpf.Docking;
using Microsoft.Extensions.DependencyInjection;
using OMS.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace OMS.Home
{
    public partial class DashboardView : UserControl
    {
        public DashboardView()
        {
            InitializeComponent();
            this.DataContext = AppServiceProvider.GetServiceProvider().GetRequiredService<DashboardViewModel>();
        }

        private void UnitsRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            LayoutPanel panel = new LayoutPanel();
        }

        private void VolumeRadioButton_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}
