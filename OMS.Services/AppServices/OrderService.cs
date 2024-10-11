using DevExpress.Mvvm.Native;
using OMS.Core.Models.Orders;
using OMS.Core.Models.User;
using OMS.Core.Services.AppServices;
using OMS.Core.Services.Cache;
using OMS.DataAccess.Repositories.AppRepositories;
using OMS.Enums;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;

namespace OMS.Services.AppServices
{
    public class OrderService : IOrderService
    {
        #region Services
        ICacheService CacheService;
        IUserService UserService;
        IOrderRepository OrderRepository;
        #endregion

        #region Constructor
        public OrderService(IOrderRepository orderRepository, IUserService userService,
            ICacheService cacheService)
        {
            CacheService = cacheService;
            OrderRepository = orderRepository;
            UserService = userService;
        }
        #endregion

        //Cached Orders
        public bool LoadOrders()
        {
            ObservableCollection<IOrder> Orders = OrderRepository.GetAll().ToObservableCollection<IOrder>();
            if (Orders != null && Orders.Count > 0) 
            {
                CacheService.Set("Orders", Orders);
                return true;
            }
            return false;
        }
        public ObservableCollection<IOrder> GetAll()
        {
            if (CacheService.ContainsKey("Orders"))
            {
                return CacheService.Get<ObservableCollection<IOrder>>("Orders");
            }

            bool ordersLoaded = LoadOrders();
            if (ordersLoaded)
            {
                return CacheService.Get<ObservableCollection<IOrder>>("Orders");
            }
            return new ObservableCollection<IOrder>();
        }
        //Get Orders
        public ICollectionView GetOpenOrders()
        {
            var orders = GetAll();
            int id = GetUser().UserID;
            var collectionViewSource = new CollectionViewSource { Source = orders };
            ICollectionViewLiveShaping liveShapingView = collectionViewSource.View as ICollectionViewLiveShaping;
            if (liveShapingView != null)
            {
                if (liveShapingView.CanChangeLiveSorting)
                {
                    liveShapingView.IsLiveSorting = true;
                    liveShapingView.LiveSortingProperties.Add(nameof(IOrder.OrderDate)); 
                    collectionViewSource.SortDescriptions.Add(new SortDescription("OrderDate", ListSortDirection.Descending));
                }

                collectionViewSource.View.Filter = o =>
                {
                    var order = o as IOrder;
                    return order != null && order.Status == (int)OrderStatus.New && order.AddedBy == id;
                };
            }
            return collectionViewSource.View;
        }
        public ICollectionView GetCancelledOrders()
        {
            var orders = GetAll();
            int id = GetUser().UserID;
            var collectionViewSource = new CollectionViewSource { Source = orders };
            ICollectionViewLiveShaping liveShapingView = collectionViewSource.View as ICollectionViewLiveShaping;
            if (liveShapingView != null)
            {
                if (liveShapingView.CanChangeLiveSorting)
                {
                    liveShapingView.IsLiveSorting = true;
                    liveShapingView.LiveSortingProperties.Add(nameof(IOrder.OrderDate));
                }

                collectionViewSource.View.Filter = o =>
                {
                    var order = o as IOrder;
                    return order != null && order.Status == (int)OrderStatus.Cancelled && order.AddedBy == id;
                };
                collectionViewSource.SortDescriptions.Add(new SortDescription("OrderDate", ListSortDirection.Descending));
            }
            return collectionViewSource.View;
        }
        public ICollectionView GetFulfilledOrders()
        {
            var orders = GetAll();
            int id = GetUser().UserID;
            var collectionViewSource = new CollectionViewSource { Source = orders };
            ICollectionViewLiveShaping liveShapingView = collectionViewSource.View as ICollectionViewLiveShaping;
            if (liveShapingView != null)
            {
                if (liveShapingView.CanChangeLiveSorting)
                {
                    liveShapingView.IsLiveSorting = true;
                    liveShapingView.LiveSortingProperties.Add(nameof(IOrder.OrderDate));
                }

                collectionViewSource.View.Filter = o =>
                {
                    var order = o as IOrder;
                    return order != null && order.Status == (int)OrderStatus.Fulfilled && order.AddedBy == id;
                };
                collectionViewSource.SortDescriptions.Add(new SortDescription("OrderDate", ListSortDirection.Descending));
            }
            return collectionViewSource.View;
        }
        public ICollectionView GetOrdersByUser(int userId)
        {
            var orders = GetAll();
            var collectionViewSource = new CollectionViewSource { Source = orders };
            ICollectionViewLiveShaping liveShapingView = collectionViewSource.View as ICollectionViewLiveShaping;
            if (liveShapingView != null)
            {
                if (liveShapingView.CanChangeLiveSorting)
                {
                    liveShapingView.IsLiveSorting = true;
                    liveShapingView.LiveSortingProperties.Add(nameof(IOrder.OrderDate));
                    collectionViewSource.SortDescriptions.Add(new SortDescription("OrderDate", ListSortDirection.Descending));
                }

                collectionViewSource.View.Filter = o =>
                {
                    var order = o as IOrder;
                    return order != null && order.AddedBy == userId;
                };
            }
            return collectionViewSource.View;
        }
        public ICollectionView GetOrdersByAccount(int accountId)
        {
            var orders = GetAll();
            var collectionViewSource = new CollectionViewSource { Source = orders };
            ICollectionViewLiveShaping liveShapingView = collectionViewSource.View as ICollectionViewLiveShaping;
            if (liveShapingView != null)
            {
                if (liveShapingView.CanChangeLiveSorting)
                {
                    liveShapingView.IsLiveSorting = true;
                    liveShapingView.LiveSortingProperties.Add(nameof(IOrder.OrderDate));
                }

                collectionViewSource.View.Filter = o =>
                {
                    var order = o as IOrder;
                    return order != null && order.AccountID == accountId;
                };
                collectionViewSource.SortDescriptions.Add(new SortDescription("OrderDate", ListSortDirection.Descending));
            }
            return collectionViewSource.View;
        }
        public ICollectionView GetOrdersByStock(string stockSymbol)
        {
            var orders = GetAll().Where(o=> o.Order_Statuses == OrderStatus.New && 
            o.Symbol ==stockSymbol).OrderByDescending(o=> o.OrderDate).Take(100);
            int id = GetUser().UserID;
            var collectionViewSource = new CollectionViewSource { Source = orders };
            ICollectionViewLiveShaping liveShapingView = collectionViewSource.View as ICollectionViewLiveShaping;
            if (liveShapingView != null)
            {
                if (liveShapingView.CanChangeLiveSorting)
                {
                    liveShapingView.IsLiveSorting = true;
                    liveShapingView.LiveSortingProperties.Add(nameof(IOrder.OrderDate));
                }

                collectionViewSource.View.Filter = o =>
                {
                    var order = o as IOrder;
                    return order != null && order.Symbol == stockSymbol && order.AddedBy == id;
                };
                collectionViewSource.SortDescriptions.Add(new SortDescription("OrderDate", ListSortDirection.Descending));
            }
            return collectionViewSource.View;
        }
        
        //Single Order
        public IOrder GetById(int key)
        {
            return GetAll().Where(o => o.OrderID == key).FirstOrDefault();
        }
        public IOrder GetLastOrderByUser()
        {
            int id = GetUser().UserID;
            return GetAll().Where(order => order.AddedBy == id)
                .OrderByDescending(order => order.CreatedDate)
                .FirstOrDefault();
        }

        #region Orders Uodate Methods 
        public bool Add(IOrder entity)
        {
            GetAll().Add(entity);
            IOrder order = OrderRepository.Add(entity);
            if (order != null && order.OrderID > 0)
            {
                int id = GetUser().UserID;
                IOrder newOrder = GetAll().Where(o => o.OrderGuid == entity.OrderGuid).First();
                if (order != null && order.OrderID > 0)
                {
                    newOrder.OrderID = order.OrderID;
                }
                else
                {
                    GetAll().Remove(entity);
                }
                return true;
            }
            return false;
        }
        public bool Update(IOrder entity)
        {
            IOrder updatedOrder = GetAll().Where(o => o.OrderID == entity.OrderID).First();
            updatedOrder.LastUpdatedDate = DateTime.Now;
            updatedOrder.Quantity = entity.Quantity;
            updatedOrder.Total = entity.Total;
            updatedOrder.AccountID = entity.AccountID;

            IOrder order = OrderRepository.Update(updatedOrder);
            if (order != null && order.OrderID > 0)
            {
                return true;
            }
            return false;
        }
        public bool CancelOrder(IOrder selectedOrder)
        {
            selectedOrder.Order_Statuses = OrderStatus.Cancelled;
            selectedOrder.Status = (int)OrderStatus.Cancelled;

            IOrder cancelledOrder = GetAll().Where(o => o.OrderID == selectedOrder.OrderID).First();
            cancelledOrder.Order_Statuses = OrderStatus.Cancelled;
            cancelledOrder.Status = (int)OrderStatus.Cancelled;

            IOrder order = OrderRepository.Update(cancelledOrder);
            if (order != null && order.Status == (int)OrderStatus.Cancelled)
            {
                return true;
            }
            return false;
        } 
        #endregion

        //Current User 
        private IUser GetUser()
        { 
            return UserService.GetUser();
        }
    }
}
