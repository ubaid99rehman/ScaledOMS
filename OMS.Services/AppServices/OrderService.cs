using DevExpress.Mvvm.Native;
using OMS.Core.Core.Models.User;
using OMS.Core.Models.Orders;
using OMS.Core.Models.User;
using OMS.Core.Services.AppServices;
using OMS.Core.Services.Cache;
using OMS.DataAccess.Repositories.AppRepositories;
using OMS.Enums;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace OMS.Services.AppServices
{
    public class OrderService : IOrderService
    {
        ICacheService CacheService;
        IOrderRepository OrderRepository;
        IUserService UserService;
        public event Action DataUpdated;

        //Constructor
        public OrderService(IOrderRepository accountRepository, IUserService userService,
            ICacheService cacheService)
        {
            CacheService = cacheService;
            OrderRepository = accountRepository;
            UserService = userService;
        }

        //Public Data Access Methods Implementation
        public ObservableCollection<IOrder> GetAll()
        {
            if (CacheService.ContainsKey("Orders"))
            {
                return CacheService.Get<ObservableCollection<IOrder>>("Orders");
            }

            ObservableCollection<IOrder> Orders = OrderRepository.GetAll().ToObservableCollection<IOrder>();
            CacheService.Set("Orders", Orders);
            return Orders;
        }
        public IOrder GetById(int key)
        {
            return GetAll().Where(o => o.OrderID == key).FirstOrDefault();
        }
        public bool Update(IOrder entity)
        {
            bool result = OrderRepository.Update(entity);
            if (result)
            {
                FetchOrders();
                DataUpdated?.Invoke();
            }
            return result;
        }
        public ObservableCollection<IOrder> GetOpenOrders()
        {
            var orders = GetAll();
            var openOrders = orders.Where(order => order.Order_Statuses == OrderStatus.New).ToObservableCollection<IOrder>();
            return openOrders;
        }
        public ObservableCollection<IOrder> GetCancelledOrders()
        {
            return GetAll().Where(order => order.Order_Statuses == OrderStatus.Cancelled).ToObservableCollection<IOrder>();
        }
        public ObservableCollection<IOrder> GetFulfilledOrders()
        {
            return GetAll().Where(order => order.Order_Statuses == OrderStatus.Fulfilled).ToObservableCollection<IOrder>();
        }
        public ObservableCollection<IOrder> GetOrdersByUser(int userId)
        {
            return GetAll().Where(order => order.AddedBy == userId).ToObservableCollection<IOrder>();
        }
        public ObservableCollection<IOrder> GetOrdersByAccount(int accountId)
        {
            return GetAll().Where(order => order.AccountID == accountId).ToObservableCollection<IOrder>();
        }
        public ObservableCollection<IOrder> GetOrdersByStock(string stockSymbol)
        {
            IUser user = UserService.GetUser();
            if (user == null)
            {
                user = new User();
                user.UserID = 1;
            }

            return GetAll().Where(order => order.Symbol == stockSymbol && order.AddedBy == user.UserID).ToObservableCollection<IOrder>();
        }
        public ObservableCollection<IOrder> GetOpenOrdersByStock(string stockSymbol)
        {
            return GetAll().Where(order => order.Order_Statuses == OrderStatus.New).Where(o => o.Symbol == stockSymbol).ToObservableCollection<IOrder>();
        }
        public IOrder GetLastOrderByUser()
        {
            IUser user = UserService.GetUser();
            if (user == null)
            {
                user = new User();
                user.UserID = 1;
            }

            return GetAll().Where(order => order.AddedBy == user.UserID)
                .OrderByDescending(order => order.CreatedDate)
                .FirstOrDefault();
        }
        public void CancelOrder(IOrder selectedOrder, out string message)
        {
            message = string.Empty;
            selectedOrder.Order_Statuses = OrderStatus.Cancelled;
            selectedOrder.Status = (int)OrderStatus.Cancelled;
            bool result = OrderRepository.Update(selectedOrder);
            if (result)
            {
                FetchOrders(); 
                DataUpdated?.Invoke();
                message = "Order Cancelled Successfully!";
            }
        }
        public bool Add(IOrder entity)
        {
            bool result = OrderRepository.Add(entity);
            if (result)
            {
                FetchOrders();
                DataUpdated?.Invoke();
            }
            return result;
        } 

        //Private Data Loading Methods
        private void FetchOrders()
        {
            ObservableCollection<IOrder> Orders = OrderRepository.GetAll().ToObservableCollection<IOrder>();
            //Recent Orders After New Order
            CacheService.Set("Orders", Orders);
        }
    }
}
