using Microsoft.Extensions.DependencyInjection;
using OMS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace OMS.Orders
{
    /// <summary>
    /// Interaction logic for OpenOrdersView.xaml
    /// </summary>
    public partial class OpenOrdersView : UserControl
    {
        public OpenOrdersView()
        {
            InitializeComponent();
            this.DataContext = AppServiceProvider.GetServiceProvider().GetRequiredService<OrdersListModel>();
        }

        private void btnCancelOrder_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnUpdateOrder_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Show_EditForm(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
