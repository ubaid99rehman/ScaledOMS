using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using OMS.Core.Models;
using OMS.Core.Services.AppServices;
using OMS.Core.Services.MarketServices.RealtimeServices;
using OMS.Enums;
using System.Collections.ObjectModel;
using System.Linq;

namespace OMS.ViewModels
{
    public class OrdersListModel : ViewModelBase
    {
        IOrderService OrderService;
        IStockDataService StockDataService;
        IAccountService AccountService;

        private int _quantity;
        public int OrderUpdatedQuantity
        {
            get { return _quantity; }
            set
            {
                SetProperty(ref _quantity, value, nameof(OrderUpdatedQuantity));
            }
        }

        private int _account;
        public int OrderUpdatedAccount
        {
            get { return _account; }
            set
            {
                SetProperty(ref _account, value, nameof(OrderUpdatedAccount));
            }
        }

        private decimal _price;
        public decimal StockCurrentPrice
        {
            get => _price;
            set
            {
                SetProperty(ref _price, value, nameof(StockCurrentPrice));
            }
        }

        private decimal _total;
        public decimal OrderUpdatedTotal
        {
            get => _total;
            set
            {
                SetProperty(ref _total, value, nameof(OrderUpdatedTotal));
            }
        }

        private bool _isPopupOpen;
        public bool IsPopupOpen
        {
            get => _isPopupOpen;
            set => SetProperty(ref _isPopupOpen, value, nameof(IsPopupOpen));
        }

        private ObservableCollection<int> _accounts;
        public ObservableCollection<int> AccountsList
        {
            get { return _accounts; }
            set { SetProperty(ref _accounts, value, nameof(AccountsList)); }
        }

        private ObservableCollection<Order> _orders;
        public ObservableCollection<Order> Orders
        {
            get { return OrderService.GetOpenOrders(); }
            set { SetProperty(ref _orders, value, nameof(Orders)); }

        }

        private Order _selectedOrder;
        public Order SelectedOrder
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
        
        public OrdersListModel(IOrderService _orderService,
            IStockDataService _stockDataService, IAccountService _accountService)
        {
            OrderService = _orderService;
            AccountService = _accountService;
            StockDataService = _stockDataService;

            _isPopupOpen = false;
            SelectedOrder = new Order();
            _orders = new ObservableCollection<Order>();
            _accounts = new ObservableCollection<int>();
            //Loads Accounts and Orders List
            InitData();
            OrderService.DataUpdated += UpdateData;

        }

        private void UpdateCurrentOrder()
        {
            OrderUpdatedAccount = (int)SelectedOrder.AccountID;
            OrderUpdatedQuantity = (int)SelectedOrder.Quantity;
            OrderUpdatedTotal = (decimal)SelectedOrder.Total;
            StockCurrentPrice = StockDataService.GetStock(SelectedOrder.Symbol).LastPrice;
        }

        private void InitData()
        {
            AccountsList = AccountService.GetAccountsList();
            UpdateData();
            SelectedOrder = Orders.FirstOrDefault();
        }

        [Command]
        public void ShowEditForm()
        {
            IsPopupOpen = true;
        }

        [Command]
        public void CloseEditForm()
        {
            IsPopupOpen = false;
        }

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

        private void UpdateData()
        {
            Orders = OrderService.GetOpenOrders();
        }
    }
}
