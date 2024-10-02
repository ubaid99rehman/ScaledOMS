using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using OMS.Core.Models;
using OMS.Core.Models.Orders;
using OMS.Core.Services.AppServices;
using OMS.Core.Services.MarketServices.RealtimeServices;
using OMS.Enums;
using System.Collections.ObjectModel;
using System.Linq;

namespace OMS.ViewModels
{
    public class OrdersListModel : ViewModelBase
    {
        //Services
        IOrderService OrderService;
        IStockDataService StockDataService;
        IAccountService AccountService;

        #region Private Members
        private decimal _quantity;
        private int _account;
        private decimal _price;
        private decimal _total;
        private ObservableCollection<int> _accounts;
        private ObservableCollection<IOrder> _orders;
        private IOrder _selectedOrder;
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
        public ObservableCollection<IOrder> Orders
        {
            get { return OrderService.GetOpenOrders(); }
            set { SetProperty(ref _orders, value, nameof(Orders)); }

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
        #endregion

        //Constructor
        public OrdersListModel(IOrderService _orderService,
            IStockDataService _stockDataService, IAccountService _accountService)
        {
            OrderService = _orderService;
            AccountService = _accountService;
            StockDataService = _stockDataService;
            SelectedOrder = new Order();
            _orders = new ObservableCollection<IOrder>();
            _accounts = new ObservableCollection<int>();
            //Loads Accounts and Orders List
            InitData();
            OrderService.DataUpdated += UpdateData;
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
            UpdateData();
            SelectedOrder = Orders.FirstOrDefault();
        }
        private void UpdateData()
        {
            Orders = OrderService.GetOpenOrders();
        }

        //Public Methods
        public void CancelOrder(out bool isCancelled, out string message)
        {
            isCancelled = false;
            message = "Cannot Update Order!";
            if (SelectedOrder != null && SelectedOrder.OrderID >= 0)
            {
                if (SelectedOrder.Status == OrderStatus.Cancelled)
                {
                    isCancelled = false;
                    message = "Order is Already Cancelled!";
                }

                if (SelectedOrder.Status == OrderStatus.Fulfilled)
                {
                    isCancelled = false;
                    message = "Cannot Cancel Fulfilled Order";
                }
                else
                {
                    OrderService.CancelOrder(SelectedOrder, out message);
                    if (message.Equals("Updated!"))
                    {
                        isCancelled = true;
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

            if (SelectedOrder != null && SelectedOrder.OrderID >= 0)
            {
                if (SelectedOrder.Status == OrderStatus.Cancelled)
                {
                    isUpdated = false;
                    message = "Cannot Update Cancelled Order!";
                }

                if (SelectedOrder.Status == OrderStatus.Fulfilled)
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
