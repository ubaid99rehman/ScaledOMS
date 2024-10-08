using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.Native;
using OMS.Core.Models;
using OMS.Core.Models.Orders;
using OMS.Core.Models.Stocks;
using OMS.Core.Models.User;
using OMS.Core.Services.AppServices;
using OMS.Core.Services.MarketServices.RealtimeServices;
using OMS.Enums;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace OMS.ViewModels
{
    public class OrderModel : ViewModelBase
    {
        //Services
        IStockDataService StockDataService;
        IOrderService OrderService;
        IAccountService AccountService;
        private IUserService UserService;
        private IPermissionService PermissionService;

        #region Private Members
        private StockDetailViewModel _stockDetailsModel;
        private ObservableCollection<int> _accounts;
        private ObservableCollection<IOrder> _orders;
        private ObservableCollection<IOrder> _stockOrders;
        private ObservableCollection<string> _stockSymbols;
        private int _selectedAccount;
        private IOrder _selectedOrder;
        private IOrder _lastOrder;
        private IStock _selectedStock;
        private string _selectedStockSymbol;
        private OrderType _orderType;
        private decimal _quantity;
        private decimal _total;
        private bool _CanAddOrder;
        private bool _CanUpdateOrder;
        private bool _CanCancelOrder;
        private IUser CurrentUser;
        #endregion

        #region Public Members
        public StockDetailViewModel StockDetailsModel
        {
            get { return _stockDetailsModel; }
            set { SetProperty(ref _stockDetailsModel, value, nameof(StockDetailsModel)); }
        }
        public ObservableCollection<int> Accounts
        {
            get { return _accounts; }
            set
            {
                SetProperty(ref _accounts, value, nameof(Accounts));
            }
        }
        public ObservableCollection<IOrder> Orders
        {
            get { return _orders; }
            set { SetProperty(ref _orders, value, nameof(Orders)); }
        }
        public ObservableCollection<IOrder> StockOrders
        {
            get { return _stockOrders; }
            set { SetProperty(ref _stockOrders, value, nameof(StockOrders)); }
        }
        public ObservableCollection<string> StockSymbols
        {
            get { return _stockSymbols; }
            set { SetProperty(ref _stockSymbols, value, nameof(StockSymbols)); }
        }
        public ObservableCollection<string> OrderTypes { get; }
        public int SelectedAccount
        {
            get { return _selectedAccount; }
            set
            {
                SetProperty(ref _selectedAccount, value, nameof(SelectedAccount));
            }
        }
        public IOrder SelectedOrder
        {
            get => _selectedOrder;
            set
            {
                if (SetProperty(ref _selectedOrder, value, nameof(SelectedOrder)))
                {
                    if (value is IOrder order && order.Total >= 1 && !string.IsNullOrEmpty(order.Symbol))
                    {
                        if (order.OrderID >= 1 && order.Quantity >= 1)
                        {
                            SelectedOrder.OrderID = order.OrderID;
                            Quantity = (decimal)order.Quantity;

                            Total = (decimal)order.Total;

                            SelectedAccount = (int)order.AccountID;
                            OrderType = (OrderType)order.OrderType;
                        }
                    }
                }
            }
        }
        public IOrder LastOrder
        {
            get => _lastOrder;
            set => SetProperty(ref _lastOrder, value, nameof(LastOrder));
        }
        public IStock SelectedStock
        {
            get { return _selectedStock; }
            set
            {
                SetProperty(ref _selectedStock, value, nameof(SelectedStock));
            }
        }
        public string SelectedStockSymbol
        {
            get => _selectedStockSymbol;
            set
            {
                if (SetProperty(ref _selectedStockSymbol, value, nameof(SelectedStockSymbol)))
                {
                    if (!string.IsNullOrEmpty(SelectedStock.Symbol))
                    {
                        UpdateData();
                    }
                }
            }

        }
        public OrderType OrderType
        {
            get { return _orderType; }
            set
            {
                SetProperty(ref _orderType, value, nameof(OrderType));
            }
        }
        public decimal Quantity
        {
            get { return _quantity; }
            set
            {
                if (SetProperty(ref _quantity, value, nameof(Quantity)))
                {
                    SelectedOrder.Quantity = (int)value;
                    if ((int)value > 1)
                    {
                        if(SelectedStock !=null && SelectedStock.LastPrice > 0)
                        {
                            Total = (decimal)(SelectedOrder.Quantity*SelectedStock.LastPrice);
                        }
                    }
                }

            }
        }
        public decimal Total
        {
            get { return _total; }
            set
            {
                SetProperty(ref _total, value, nameof(Total));
                SelectedOrder.Total = value;
            }
        }
        public bool CanAddOrder
        {
            get => _CanAddOrder;
            set
            {
                SetProperty(ref _CanAddOrder, value, nameof(CanAddOrder));
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
        public OrderModel(IStockDataService stockDataService, IOrderService orderService, 
            IAccountService accountService, IUserService userService,
            IPermissionService permissionService)
        {
            _stockDetailsModel = new StockDetailViewModel(stockDataService);
            SelectedOrder = new Order();
            SelectedStock = new Stock();

            #region Services Initialization
            StockDataService = stockDataService;
            AccountService = accountService;
            OrderService = orderService;
            OrderService.DataUpdated += FetchUpdatedOrders;
            PermissionService = permissionService;
            UserService  = userService;
            #endregion
            #region Initialized Collections
            Accounts = new ObservableCollection<int>();
            Orders = new ObservableCollection<IOrder>();
            StockSymbols = new ObservableCollection<string>();
            OrderTypes = Enum.GetValues(typeof(OrderType)).Cast<OrderType>().Select(e =>
            e.ToString()).ToObservableCollection();
            #endregion
            SetPermissions();
            InitData();
            SetUser();
        }

        //Private Methods
        private void FetchUpdatedOrders(int id)
        {
            if(id == CurrentUser.UserID)
            {
                Orders = OrderService.GetOpenOrders();
            }
        }
        private void FetchOrders()
        {
            Orders = OrderService.GetOpenOrders();
        }
        private void InitData()
        {
            //Stocks related Data
            StockSymbols = StockDataService.GetStockSymbols();
            SelectedStockSymbol = StockSymbols.FirstOrDefault();
            SelectedStock = StockDataService.GetStock(StockSymbols.FirstOrDefault());

            UpdateData();

            //Accounts Data
            Accounts = AccountService.GetAccountsList();
            //Orders
            LastOrder = OrderService.GetLastOrderByUser();
            StockOrders = OrderService.GetOrdersByStock(SelectedStockSymbol);
            FetchOrders();

        }
        private void UpdateData()
        {
            SelectedStock = StockDataService.GetStock(_selectedStockSymbol);
            StockDetailsModel.Symbol = SelectedStockSymbol;
            LastOrder = OrderService.GetLastOrderByUser();
            StockOrders = OrderService.GetOrdersByStock(_selectedStockSymbol);
            FetchOrders();
            SelectedOrder = new Order();
            SelectedOrder.Price = SelectedStock.LastPrice;
            Total = SelectedStock.LastPrice;
        }
        private void ClearFields()
        {
            SelectedOrder = new Order();
            Quantity = 1;
            Total = SelectedStock.LastPrice;
            OrderType = OrderType.Buy;
        }
        private void UpdateTotal()
        {
            Total = SelectedStock.LastPrice * Quantity;
        }
        private void SetPermissions()
        {
            CanAddOrder = PermissionService.HaveUserCreatePermission("ManageOrderView");
            CanUpdateOrder = PermissionService.HaveUserUpdatePermission("ManageOrderView");
            CanCancelOrder = PermissionService.HaveUserCancelPermission("ManageOrderView");
        }
        private void SetUser()
        {
            CurrentUser = UserService.GetUser();
        }

        //Public Methods
        public void AddOrder(out bool isAdded, out string message)
        {
            isAdded = false;
            message = string.Empty;
            var newOrder = new Order
            {
                OrderGuid = Guid.NewGuid(),
                AccountID = SelectedAccount,
                AddedBy = CurrentUser.UserID,
                OrderType = (int)OrderType,
                Status = (int)OrderStatus.New,
                Order_Types = OrderType,
                Order_Statuses = OrderStatus.New,
                Symbol = SelectedStockSymbol,
                Quantity = (int)Quantity,
                Price = SelectedStock.LastPrice,
                Total = Total,
                CreatedDate = DateTime.Now,
                OrderDate = DateTime.Now,
                LasUpdatedDate = DateTime.Now,
                ExpirationDate = DateTime.Now
            };
            if (isValidOrder(newOrder))
            {
                isAdded = OrderService.Add(newOrder);
                if (isAdded)
                {
                    message = "Order Added Successfully!";
                    UpdateData();
                    ClearFields();
                    LastOrder = OrderService.GetLastOrderByUser();
                }
                else
                {
                    message = "Error Occured While Adding Order!";
                }
            }
        }
        public void CancelOrder(out bool isCancelled, out string message)
        {
            isCancelled = false;
            message = "Cannot Update Order!";
            if (SelectedOrder.OrderID >= 0)
            {
                if (SelectedOrder.Order_Statuses != OrderStatus.Cancelled
                    || SelectedOrder.Order_Statuses != OrderStatus.Fulfilled)
                {
                    OrderService.CancelOrder(SelectedOrder, out message);
                    if (message.Equals("Updated!"))
                    {
                        isCancelled = true;
                        UpdateData();
                    }
                }
            }
        }
        [Command]
        public void ClearOrder()
        {
            ClearFields();
        }
        public bool isValidOrder(IOrder SelectedOrder)
        {
            if (SelectedOrder != null && !string.IsNullOrEmpty(SelectedOrder.Symbol) &&
               SelectedOrder.Quantity > 0 && SelectedOrder.Price > 0 && SelectedOrder.AccountID != 0)
            {
                return true;
            }
            return false;
        }
        public void UpdateOrder(out bool isUpdated, out string message)
        {
            isUpdated = false;
            message = "";

            if (!(SelectedOrder.OrderID >= 0))
            {
                isUpdated = OrderService.Update(SelectedOrder);
                if (isUpdated)
                {
                    UpdateData();
                }
            }
        }
    }
}
