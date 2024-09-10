using OMS.Core.Enums;
using OMS.Core.Models;
using OMS.Core.Services;
using OMS.Core.Services.AppServices;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace OMS.Services.AppServices
{
    public class OrderService : IOrderService
    {
        private static OrderService _instance;
        private static readonly object _lock = new object();
        private ObservableCollection<Order> _orders;

        private string filePath = "C:\\XML\\Orders.xml";
   
        public OrderService()
        {
            _orders = new ObservableCollection<Order>();
        }

        public ObservableCollection<Order> GetAll()
        {
            return _orders;
        }

        public Order GetById(string key)
        {
            return _orders.FirstOrDefault(o => o.OrderID.ToString() == key);
        }

        public bool Delete(Order entity)
        {
            if (_orders.Contains(entity))
            {
                _orders.Remove(entity);
                return true;
            }
            return false;
        }

        public bool Update(Order entity)
        {
            var existingOrder = _orders.FirstOrDefault(o => o.OrderID == entity.OrderID);
            if (existingOrder != null)
            {
                int index = _orders.IndexOf(existingOrder);
                _orders[index] = entity;
                return true;
            }
            return false;
        }

        public bool DeleteById(int id)
        {
            var order = _orders.FirstOrDefault(o => o.OrderID == id);
            return Delete(order);
        }

        public bool UpdateById(int id)
        {
            var order = _orders.FirstOrDefault(o => o.OrderID == id);
            return Update(order);
        }

        public ObservableCollection<Order> GetOpenOrders()
        {
            return new ObservableCollection<Order>(_orders.Where(o => o.Status == OrderStatus.New));
        }

        public ObservableCollection<Order> GetCancelledOrders()
        {
            return new ObservableCollection<Order>(_orders.Where(o => o.Status == OrderStatus.Cancelled));
        }

        public ObservableCollection<Order> GetFulfilledOrders()
        {
            return new ObservableCollection<Order>(_orders.Where(o => o.Status == OrderStatus.Fulfilled));
        }

        public ObservableCollection<Order> GetOrdersByUser(int userId)
        {
            return new ObservableCollection<Order>(_orders.Where(o => o.AddedBy == userId));
        }

        public ObservableCollection<Order> GetOrdersByAccount(int accountId)
        {
            return new ObservableCollection<Order>(_orders.Where(o => o.AccountID == accountId));
        }

        public ObservableCollection<Order> GetOrdersByStock(string stockSymbol)
        {
            return new ObservableCollection<Order>(_orders.Where(o => o.Symbol == stockSymbol));
        }

        public ObservableCollection<Order> GetOpenOrdersByStock(string stockSymbol)
        {
            return new ObservableCollection<Order>(_orders.Where(o => o.Symbol == stockSymbol && o.Status == OrderStatus.New));
        }

        public Order GetLastOrder()
        {
            return _orders.OrderByDescending(o => o.OrderDate).FirstOrDefault();
        }

        public bool AddOrder(Order order, out string message)
        {
            try
            {
                _orders.Add(order);
                message = "Order added successfully.";
                return true;
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return false;
            }
        }

        public void CancelOrder(Order selectedOrder, out string message)
        {
            var order = _orders.FirstOrDefault(o => o.OrderID == selectedOrder.OrderID);
            if (order != null)
            {
                order.Status = OrderStatus.Cancelled;
                message = "Order cancelled successfully.";
            }
            else
            {
                message = "Order not found.";
            }
        }

        public void LoadOrders()
        {
            var serializer = new XmlSerializer(typeof(ObservableCollection<Order>), new XmlRootAttribute("Orders"));
            using (var reader = new StreamReader(filePath))
            {
                _orders = (ObservableCollection<Order>)serializer.Deserialize(reader);
            }
        }

        public bool Add(Order entity)
        {
            _orders.Add(entity);
            if (_orders.Contains(entity))
            {
                return true;
            }

            return false;
        }
    }
}
