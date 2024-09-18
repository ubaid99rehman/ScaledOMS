using Microsoft.Extensions.DependencyInjection;
using OMS.Cache;
using OMS.Core.Services.Cache;
using OMS.ViewModels;
using System.Windows;

namespace OMS.Orders
{
    public partial class AddOrder : Window
    {
        ICacheService CacheService;

        public AddOrder(ICacheService cacheService)
        {
            InitializeComponent();
            CacheService = cacheService;
            this.DataContext = AppServiceProvider.GetServiceProvider().GetRequiredService<AddOrderModel>();
        }

        private void btnAddOrder_Click(object sender, RoutedEventArgs e)
        {
            if(this.DataContext is AddOrderModel model)
            {
                bool isAdded = model.AddOrder();
                if(isAdded)
                {
                    MessageBox.Show("Order Added Successfully", "Order Added", MessageBoxButton.OK);
                    CacheService.Set("NewOrderWindowOpen",false);
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
            CacheService.Set("NewOrderWindowOpen", false);
            this.Close();   
        }
    }
}
