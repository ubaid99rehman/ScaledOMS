using DevExpress.Xpf.Core;
using Microsoft.Extensions.DependencyInjection;
using OMS.Cache;
using OMS.Core.Services.Cache;
using OMS.ViewModels;
using System.Windows;

namespace OMS
{
    public partial class AddOrder : ThemedWindow
    {
        //Constructor
        public AddOrder()
        {
            InitializeComponent();
            this.DataContext = AppServiceProvider.GetServiceProvider().GetRequiredService<AddOrderModel>();
        }

        #region Button CLick Events
        private void btnAddOrder_Click(object sender, RoutedEventArgs e)
        {
            if (this.DataContext is AddOrderModel model)
            {
                bool isAdded = model.AddOrder();
                if (isAdded)
                {
                    MessageBox.Show("Order Added Successfully", "Order Added", MessageBoxButton.OK);
                    this.Close();

                }
                else
                {
                    MessageBox.Show("Cannot Add Order.", "Order Failure", MessageBoxButton.OK);
                }
            }
        }
        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        } 
        #endregion
    }
}
