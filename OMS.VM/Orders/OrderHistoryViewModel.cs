using DevExpress.Mvvm;
using OMS.Core.Core.Enums;
using OMS.Core.Enums;
using OMS.Core.Models;
using OMS.Core.Services.AppServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.ViewModels
{
    public class OrderHistoryViewModel : ViewModelBase
    {
        IOrderService OrderService;


        private ObservableCollection<Order> orders;
        public ObservableCollection<Order> Orders
        {
            get => orders ?? (orders = new ObservableCollection<Order>());
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
            orders = OrderService.GetAll();
        }

    }
}
