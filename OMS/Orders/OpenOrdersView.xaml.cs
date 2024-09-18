using DevExpress.Xpf.Core;
using DevExpress.Xpf.Editors.Helpers;
using Microsoft.Extensions.DependencyInjection;
using OMS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace OMS.Orders
{

    public partial class OpenOrdersView : UserControl
    {
        public OpenOrdersView()
        {
            InitializeComponent();
            this.DataContext = AppServiceProvider.GetServiceProvider().GetRequiredService<OrdersListModel>();
        }

        private void btnCancelOrder_Click(object sender, RoutedEventArgs e)
        {
            if (this.DataContext is OrdersListModel model)
            {
                model.CancelOrder(out bool isCancelled,out string message);

                model.CloseEditForm();
                if(isCancelled)
                {
                    ThemedMessageBox.Show("Order Cancelled Successfully!", "Order Cancelled", MessageBoxButton.OK);
                }
                else
                {
                    ThemedMessageBox.Show(message);
                }
            }
        }

        private void btnUpdateOrder_Click(object sender, RoutedEventArgs e)
        {
            if (this.DataContext is OrdersListModel model)
            {
                model.UpdateOrder(out bool isUpdated, out string message);
                if(isUpdated)
                {
                    model.CloseEditForm();
                    MessageBox.Show("Order Updated Successfully!", "Order Updated", MessageBoxButton.OK);
                }
                else
                {
                    MessageBox.Show("Cannot Update Order!", "Order Update", MessageBoxButton.OK);
                }

            }
        }

        private void Show_EditForm(object sender, MouseButtonEventArgs e)
        {
            if(this.DataContext is OrdersListModel model)
            {
                model.ShowEditForm();
            }
        }
    }
}
