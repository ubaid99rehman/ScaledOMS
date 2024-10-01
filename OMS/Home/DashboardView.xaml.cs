using Microsoft.Extensions.DependencyInjection;
using OMS.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace OMS.Home
{
    public partial class DashboardView : UserControl
    {
        //Constructor
        public DashboardView()
        {
            InitializeComponent();
            this.DataContext = AppServiceProvider.GetServiceProvider().GetRequiredService<DashboardViewModel>();
        }

        #region Stock Holding Chart Raio Button Click Event
        private void UnitsRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (volumeSeries != null && unitsSeries != null)
            {
                volumeSeries.Visible = false;
                unitsSeries.Visible = true;
            }
        }
        private void VolumeRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (volumeSeries != null && unitsSeries != null)
            {
                volumeSeries.Visible = true;
                unitsSeries.Visible = false;
            }
        } 
        #endregion
    }
}
