using DevExpress.Mvvm.POCO;
using DevExpress.Xpf.Grid;
using Microsoft.Extensions.DependencyInjection;
using OMS.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace OMS.Orders
{

    public partial class ManageOrderView : UserControl
    {
        public ManageOrderView()
        {
            InitializeComponent();
            this.DataContext = AppServiceProvider.GetServiceProvider().GetRequiredService<OrderModel>();
        }

        private void btnUpdateOrder_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAddOrder_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnCancelOrder_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
