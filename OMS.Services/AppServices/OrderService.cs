using DevExpress.Mvvm.Native;
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
        public event Action<int> DataUpdated;

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
        public ObservableCollection<IOrder> GetOpenOrders()
        {
            var orders = GetAll();
            int id = GetUser().UserID;
            var openOrders = orders.Where(order => order.Order_Statuses == OrderStatus.New && 
            order.AddedBy == id).ToObservableCollection<IOrder>();
            return openOrders;
        }
        public ObservableCollection<IOrder> GetCancelledOrders()
        {
            var orders = GetAll();
            int id = GetUser().UserID;
            var cancelledOrders = orders.Where(order => order.Order_Statuses == OrderStatus.Cancelled &&
            order.AddedBy == id).ToObservableCollection<IOrder>();
            return cancelledOrders;
        }
        public ObservableCollection<IOrder> GetFulfilledOrders()
        {
            var orders = GetAll();
            int id = GetUser().UserID;
            var cancelledOrders = orders.Where(order => order.Order_Statuses == OrderStatus.Fulfilled &&
            order.AddedBy == id).ToObservableCollection<IOrder>();
            return cancelledOrders;
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
            int id= GetUser().UserID;
            return GetAll().Where(order => order.Symbol == stockSymbol && order.AddedBy == id).ToObservableCollection<IOrder>();
        }
        public ObservableCollection<IOrder> GetOpenOrdersByStock(string stockSymbol)
        {
            return GetAll().Where(order => order.Order_Statuses == OrderStatus.New).Where(o => o.Symbol == stockSymbol).ToObservableCollection<IOrder>();
        }
        public IOrder GetLastOrderByUser()
        {
            int id = GetUser().UserID;
            return GetAll().Where(order => order.AddedBy == id)
                .OrderByDescending(order => order.CreatedDate)
                .FirstOrDefault();
        }
        public bool Add(IOrder entity)
        {
            GetAll().Add(entity);
            IOrder order = OrderRepository.Add(entity);
            if (order !=null && order.OrderID > 0)
            {
                int id = GetUser().UserID;
                IOrder newOrder = GetAll().Where(o => o.OrderGuid == entity.OrderGuid).First();
                if(order != null && order.OrderID > 0)
                {
                    newOrder.OrderID = order.OrderID;
                }
                else
                {
                    GetAll().Remove(entity);
                }
                DataUpdated?.Invoke(id);
                return true;
            }
            return false;
        }
        public bool Update(IOrder entity)
        {
            IOrder updatedOrder = GetAll().Where(o => o.OrderID == entity.OrderID).First();
            updatedOrder.LasUpdatedDate = DateTime.Now;
            updatedOrder.Quantity = entity.Quantity;
            updatedOrder.Total = entity.Total;
            updatedOrder.AccountID = entity.AccountID;

            IOrder order = OrderRepository.Update(updatedOrder);
            if (order != null && order.OrderID > 0)
            {
                int id = GetUser().UserID;
                DataUpdated?.Invoke(id);
                return true;
            }
            return false;
        }
        public void CancelOrder(IOrder selectedOrder, out string message)
        {
            message = string.Empty;
            selectedOrder.Order_Statuses = OrderStatus.Cancelled;
            selectedOrder.Status = (int)OrderStatus.Cancelled;

            IOrder cancelledOrder = GetAll().Where(o=> o.OrderID == selectedOrder.OrderID).First();
            cancelledOrder.Order_Statuses = OrderStatus.Cancelled;
            cancelledOrder.Status = (int)OrderStatus.Cancelled;

            IOrder order = OrderRepository.Update(cancelledOrder);
            if (order != null && order.Status == (int)OrderStatus.Cancelled)
            {
                //FetchOrders(); 
                int id = GetUser().UserID;
                DataUpdated?.Invoke(id);
                message = "Order Cancelled Successfully!";
            }
        }

        private IUser GetUser()
        { 
            return UserService.GetUser();
        }
    }
}
