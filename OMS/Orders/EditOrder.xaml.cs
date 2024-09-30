using DevExpress.Xpf.Core;
using OMS.Core.Models;
using OMS.ViewModels;
using System.Windows;

namespace OMS
{
    public partial class EditOrder : ThemedWindow
    {
        Order SelectedOrder;

        public EditOrder()
        {
            InitializeComponent();
        }

        private void btnCancelOrder_Click(object sender, RoutedEventArgs e)
        {
            if (this.DataContext is OrdersListModel model)
            {
                model.CancelOrder(out bool isCancelled, out string message);
                if (isCancelled)
                {
                    MessageBox.Show("Order Cancelled Successfully!", "Order Cancelled", MessageBoxButton.OK);
                    this.Close();
                }
                else
                {
                    ThemedMessageBox.Show(message, "Order Cancelled", MessageBoxButton.OK);
                }
            }
        }

        private void btnUpdateOrder_Click(object sender, RoutedEventArgs e)
        {
            if (this.DataContext is OrdersListModel model)
            {
                model.UpdateOrder(out bool isUpdated, out string message);
                if (isUpdated)
                {
                    MessageBox.Show("Order Updated Successfully!", "Order Updated", MessageBoxButton.OK);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Cannot Update Order!", "Order Update", MessageBoxButton.OK);
                }

            }
        }

    }
}
