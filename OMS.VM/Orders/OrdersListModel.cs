using DevExpress.Mvvm;
using OMS.Core.Models;
using OMS.Core.Models.Orders;
using OMS.Core.Services.AppServices;
using OMS.Core.Services.MarketServices.RealtimeServices;
using OMS.Enums;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace OMS.ViewModels
{
    public class OrdersListModel : ViewModelBase
    {
        //Services
        private IOrderService OrderService;
        private IStockDataService StockDataService;
        private IAccountService AccountService;
        private IPermissionService PermissionService;

        #region Private Members
        private decimal _quantity;
        private int _account;
        private decimal _price;
        private decimal _total;
        private ObservableCollection<int> _accounts;
        private ObservableCollection<IOrder> _orders;
        private IOrder _selectedOrder;
        private bool _CanUpdateOrder;
        private bool _CanCancelOrder;
        #endregion

        #region Public Members
        public decimal OrderUpdatedQuantity
        {
            get { return _quantity; }
            set
            {
                SetProperty(ref _quantity, value, nameof(OrderUpdatedQuantity));
                OrderUpdatedTotal = OrderUpdatedQuantity * StockCurrentPrice;
            }
        }
        public int OrderUpdatedAccount
        {
            get { return _account; }
            set
            {
                SetProperty(ref _account, value, nameof(OrderUpdatedAccount));
            }
        }
        public decimal StockCurrentPrice
        {
            get => _price;
            set
            {
                SetProperty(ref _price, value, nameof(StockCurrentPrice));
            }
        }
        public decimal OrderUpdatedTotal
        {
            get => _total;
            set
            {
                SetProperty(ref _total, value, nameof(OrderUpdatedTotal));
            }
        }
        public ObservableCollection<int> AccountsList
        {
            get { return _accounts; }
            set { SetProperty(ref _accounts, value, nameof(AccountsList)); }
        }
        public ICollectionView Orders
        {
            get { return OrderService.GetOpenOrders(); }

        }
        public IOrder SelectedOrder
        {
            get => _selectedOrder;
            set
            {
                SetProperty(ref _selectedOrder, value, nameof(SelectedOrder));

                if (SelectedOrder == null)
                {
                    SelectedOrder = new Order();
                }
                if (SelectedOrder != null && SelectedOrder.OrderID >= 1)
                {
                    UpdateCurrentOrder();
                }
            }
        }
        public bool CanUpdateOrder
        {
            get => _CanUpdateOrder;
            set
            {
                SetProperty(ref _CanUpdateOrder, value, nameof(CanUpdateOrder));
            }
        }
        public bool CanCancelOrder
        {
            get => _CanCancelOrder;
            set
            {
                SetProperty(ref _CanCancelOrder, value, nameof(CanCancelOrder));
            }
        }
        #endregion

        //Constructor
        public OrdersListModel(IOrderService _orderService, IPermissionService permissionService,
            IStockDataService _stockDataService, IAccountService _accountService)
        {
            OrderService = _orderService;
            AccountService = _accountService;
            StockDataService = _stockDataService;
            PermissionService = permissionService;
            SelectedOrder = new Order();
            _orders = new ObservableCollection<IOrder>();
            _accounts = new ObservableCollection<int>();
            //Loads Accounts and Orders List
            InitData();
            //OrderService.DataUpdated += FetchOrders;
            //Set Permissions
            SetPermissions();
            ICollectionView openOrdersView = OrderService.GetOpenOrders();
            int filteredCount = openOrdersView.Cast<IOrder>().Count();
        }

        private void SetPermissions()
        {
            CanUpdateOrder = PermissionService.HaveUserUpdatePermission("OpenOrdersView");
            CanCancelOrder = PermissionService.HaveUserCancelPermission("OpenOrdersView");
        }

        //Private Methods
        private void UpdateOrderTotal()
        {
            OrderUpdatedTotal = OrderUpdatedQuantity * StockCurrentPrice;
            SelectedOrder.Total = OrderUpdatedTotal;
        }
        private void UpdateCurrentOrder()
        {
            OrderUpdatedAccount = (int)SelectedOrder.AccountID;
            OrderUpdatedQuantity = (int)SelectedOrder.Quantity;
            OrderUpdatedTotal = (decimal)SelectedOrder.Total;
            StockCurrentPrice = (decimal)SelectedOrder.Price;
        }
        private void InitData()
        {
            AccountsList = AccountService.GetAccountsList();
            FetchOrders(0);
            IOrder order = Orders?.Cast<IOrder>().FirstOrDefault();

            if (order != null) 
            {
                SelectedOrder = order;
            }
            SelectedOrder = new Order();
        }
        private void FetchOrders(int id)
        {
            //Orders = OrderService.GetOpenOrders();
        }

        //Public Methods
        public void CancelOrder(out bool isCancelled, out string message)
        {
            isCancelled = false;
            SelectedOrder.LasUpdatedDate = DateTime.Now;
            message = "Cannot Update Order!";
            if (SelectedOrder != null && SelectedOrder.OrderID >= 0)
            {
                if (SelectedOrder.Order_Statuses == OrderStatus.Cancelled)
                {
                    isCancelled = false;
                    message = "Order is Already Cancelled!";
                }
                if (SelectedOrder.Order_Statuses == OrderStatus.Fulfilled)
                {
                    isCancelled = false;
                    message = "Cannot Cancel Fulfilled Order";
                }
                else
                {
                    bool result = OrderService.CancelOrder(SelectedOrder);
                    if (result)
                    {
                        message = "Order Cabcelled Successfully!";
                        isCancelled = true;
                    }
                    else
                    {
                        isCancelled = false;
                        message = "Problem Occured While Cancelling Order!";
                    }

                }
            }
        }
        public void UpdateOrder(out bool isUpdated, out string message)
        {
            isUpdated = false;
            message = "Cannot Update Order!";
            SelectedOrder.AccountID = OrderUpdatedAccount;
            SelectedOrder.Price = StockCurrentPrice;
            SelectedOrder.Quantity = (int)OrderUpdatedQuantity;
            SelectedOrder.Total = OrderUpdatedTotal;
            SelectedOrder.LasUpdatedDate = DateTime.Now;
            if (SelectedOrder != null && SelectedOrder.OrderID >= 0)
            {
                if (SelectedOrder.Order_Statuses == OrderStatus.Cancelled)
                {
                    isUpdated = false;
                    message = "Cannot Update Cancelled Order!";
                }
                if (SelectedOrder.Order_Statuses == OrderStatus.Fulfilled)
                {
                    isUpdated = false;
                    message = "Cannot Update Fulfilled Order";
                }
                else
                {
                    bool result = OrderService.Update(SelectedOrder);
                    if (result)
                    {
                        isUpdated = true;
                        message = "Order Updated Successfully!";
                    }
                }
            }
        }
    }
}
