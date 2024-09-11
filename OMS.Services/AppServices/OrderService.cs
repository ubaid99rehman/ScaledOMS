using DevExpress.Mvvm.Native;
using OMS.Core.Core.Models.User;
using OMS.Core.Enums;
using OMS.Core.Models;
using OMS.Core.Services;
using OMS.Core.Services.AppServices;
using OMS.DataAccess.Repositories;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Xml.Serialization;

namespace OMS.Services.AppServices
{
    public class OrderService : IOrderService
    {
        ICacheService CacheService;
        IOrderRepository OrderRepository;

        public event Action DataUpdated;

        //private string filePath = "C:\\XML\\Orders.xml";

        public OrderService(IOrderRepository accountRepository, 
            ICacheService cacheService)
        {
            CacheService = cacheService;
            OrderRepository = accountRepository;
        }

        public ObservableCollection<Order> GetAll()
        {
            if(CacheService.ContainsKey("Orders"))
            {
                return CacheService.Get<ObservableCollection<Order>>("Orders");
            }

            ObservableCollection<Order> Orders = OrderRepository.GetAll().ToObservableCollection<Order>();
            CacheService.Set("Orders", Orders);
            return Orders;
        }

        public Order GetById(int key)
        {
            return GetAll().Where(o => o.OrderID == key).FirstOrDefault();
        }

        public bool Update(Order entity)
        {
            bool result = OrderRepository.Update(entity);
            if(result)
            {
                GetAll();
                DataUpdated?.Invoke();
            }
            return result;
        }

        public ObservableCollection<Order> GetOpenOrders()
        {
            var orders = GetAll();
            var openOrders = orders.Where(order => order.Status ==OrderStatus.New).ToObservableCollection<Order>();
            return openOrders;
        }

        public ObservableCollection<Order> GetCancelledOrders()
        {
            return GetAll().Where(order => order.Status == OrderStatus.Cancelled).ToObservableCollection<Order>();
        }

        public ObservableCollection<Order> GetFulfilledOrders()
        {
            return GetAll().Where(order => order.Status == OrderStatus.Fulfilled).ToObservableCollection<Order>();
        }

        public ObservableCollection<Order> GetOrdersByUser(int userId)
        {
            return GetAll().Where(order => order.AddedBy == userId).ToObservableCollection<Order>();
        }

        public ObservableCollection<Order> GetOrdersByAccount(int accountId)
        {
            return GetAll().Where(order => order.AccountID == accountId).ToObservableCollection<Order>();
        }

        public ObservableCollection<Order> GetOrdersByStock(string stockSymbol)
        {
            return GetAll().Where(order => order.Symbol == stockSymbol).ToObservableCollection<Order>();
        }

        public ObservableCollection<Order> GetOpenOrdersByStock(string stockSymbol)
        {
            return GetAll().Where(order => order.Status == OrderStatus.New).Where(o => o.Symbol == stockSymbol).ToObservableCollection<Order>();
        }

        public Order GetLastOrderByUser()
        {
            User user = CacheService.Get<User>("CurrentUser");

            return GetAll().Where(order => order.AddedBy == user.UserID)
                .OrderByDescending(order => order.CreatedDate) 
                .FirstOrDefault();
        }

        public void CancelOrder(Order selectedOrder, out string message)
        {
            message = string.Empty;
            selectedOrder.Status = OrderStatus.Cancelled;
            bool result = OrderRepository.Update(selectedOrder);
            if (result)
            {
                GetAll();
                DataUpdated?.Invoke();
                message = "Cancelled";
            }
        }

        public bool Add(Order entity)
        {
            bool result = OrderRepository.Update(entity);
            if (result)
            {
                GetAll();
                DataUpdated?.Invoke();
            }
            return result;
        }

    }
}
