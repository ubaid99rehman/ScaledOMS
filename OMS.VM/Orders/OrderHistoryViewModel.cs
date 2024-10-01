using DevExpress.Mvvm;
using OMS.Core.Services.AppServices;
using System.Collections.ObjectModel;
using OMS.Core.Models.Orders;

namespace OMS.ViewModels
{
    public class OrderHistoryViewModel : ViewModelBase
    {
        IOrderService OrderService;

        private ObservableCollection<IOrder> orders;
        public ObservableCollection<IOrder> Orders
        {
            get => orders ?? (orders = new ObservableCollection<IOrder>());
            set
            {
                SetProperty(ref orders, value, nameof(Orders));
            }
        }

        public OrderHistoryViewModel(IOrderService orderService)
        {
            OrderService = orderService;
            LoadOrders();
        }

        private void LoadOrders()
        {
            Orders = OrderService.GetAll();
        }
    }
}
