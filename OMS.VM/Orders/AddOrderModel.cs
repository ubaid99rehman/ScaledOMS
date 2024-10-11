using DevExpress.Mvvm;
using DevExpress.Mvvm.Native;
using OMS.Core.Models;
using OMS.Core.Models.Stocks;
using OMS.Core.Models.User;
using OMS.Core.Services.AppServices;
using OMS.Core.Services.MarketServices.RealtimeServices;
using OMS.Enums;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Permissions;
using System.Windows.Controls;

namespace OMS.ViewModels
{
    public class AddOrderModel : ViewModelBase
    {
        //Services
        private IStockDataService StockDataService;
        private IOrderService OrderService;
        private IAccountService AccountService;
        private IUserService UserService;
        private IPermissionService PermissionService;

        #region Private Members
        private Order _selectedOrder;
        private string _selectedStockSymbol;
        private string _orderType;
        private IStock _selectedStock;
        private ObservableCollection<string> _orderTypes;
        private decimal _quantity;
        private decimal _stockPrice;
        private decimal _total;
        private ObservableCollection<int> _accounts;
        private ObservableCollection<string> _stockSymbols;
        private IUser CurrentUser;
        private bool _CanAddOrder;
        #endregion

        #region Public Members
        public Order SelectedOrder
        {
            get => _selectedOrder;
            set
            {
                SetProperty(ref _selectedOrder, value, nameof(SelectedOrder));
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
                        UpdateStock();
                        ClearFields();
                    }
                }
            }

        }
        public string SelectedOrderType
        {
            get => _orderType;
            set
            {
                if (SetProperty(ref _orderType, value, nameof(OrderType)))
                {
                    if (SelectedOrder !=null)
                    {
                        OrderType type;
                        if (Enum.TryParse(value, out type))
                        {
                            SelectedOrder.OrderType = (int)type;
                            SelectedOrder.Order_Types = type;
                        }
                    }
                }
            }

        }
        public IStock SelectedStock
        {
            get { return _selectedStock; }
            set
            {
                SetProperty(ref _selectedStock, value, nameof(SelectedStock));
            }
        }
        public ObservableCollection<string> OrderTypes
        {
            get { return _orderTypes; }
            set
            {
                SetProperty(ref _orderTypes, value, nameof(OrderTypes));
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
                        UpdateTotal();
                    }
                }

            }
        }
        public decimal StockPrice
        {
            get => SelectedStock?.LastPrice ?? 0;
            set
            {
                SetProperty(ref _stockPrice, value, nameof(StockPrice));
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
        public ObservableCollection<int> AccountsList
        {
            get { return _accounts; }
            set { SetProperty(ref _accounts, value, nameof(AccountsList)); }
        }
        public ObservableCollection<string> StockSymbols
        {
            get => _stockSymbols;
            set
            {
                SetProperty(ref _stockSymbols, value, nameof(StockSymbols));
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
        #endregion

        //Constructor
        public AddOrderModel(IOrderService _orderService,
            IStockDataService _stockDataService, 
            IAccountService _accountService,
            IUserService userService,
            IPermissionService permissionService)
        {
            OrderService = _orderService;
            AccountService = _accountService;
            StockDataService = _stockDataService;
            UserService = userService;
            PermissionService = permissionService;
            SelectedStock = new Stock();
            SelectedOrder = new Order();
            InitData();
            SetUser();
            SetAddOrderPermission();
        }

        private void SetAddOrderPermission()
        {
            CanAddOrder = PermissionService.HaveUserCreatePermission("AddOrderView");
        }

        //Methods
        private void InitData()
        {
            OrderTypes = Enum.GetValues(typeof(OrderType)).Cast<OrderType>().Select(e =>
            e.ToString()).ToObservableCollection();
            AccountsList = AccountService.GetAccountsList();
            StockSymbols = StockDataService.GetStockSymbols();
            SelectedStockSymbol = StockSymbols.First();
            SelectedStock = StockDataService.GetStock(SelectedStockSymbol);
        }
        private void UpdateStock()
        {
            SelectedStock = StockDataService.GetStock(SelectedStockSymbol);
        }
        private void UpdateTotal()
        {
            Total = SelectedStock.LastPrice * Quantity;
        }
        private void SetUser()
        {
            CurrentUser = UserService.GetUser();
        }

        //Public Methods
        public void ClearFields()
        {
            SelectedOrder = new Order();
            SelectedOrder.AccountID = AccountsList.First();
            SelectedOrder.OrderType = (int)OrderType.Buy;
            Quantity = 0;
            Total = 0;
        }
        public bool AddOrder()
        {
            OrderType type;
            if (Enum.TryParse(SelectedOrderType, out type))
            {
                SelectedOrder.OrderType = (int)type;
            }
            else
            {
                return false;
            }
           
            if (SelectedOrder.Quantity <=0)
            {
                return false; 
            }
            if (SelectedOrder.Total <= 0)
            {
                return false;
            }
            if (CurrentUser != null && CurrentUser.UserID > 0)
            {
                SelectedOrder.AddedBy = CurrentUser.UserID;
            }
            else
            {
                return false;
            }
            if(SelectedOrder.AccountID <= 0)
            {
                return false ;
            }
            SelectedOrder.Symbol = SelectedStockSymbol;
            SelectedOrder.OrderGuid = Guid.NewGuid();
            SelectedOrder.Quantity = (int)Quantity;
            SelectedOrder.Price= StockPrice;
            SelectedOrder.Total = Quantity * StockPrice;
            SelectedOrder.Status = (int)OrderStatus.New;
            SelectedOrder.Order_Statuses = OrderStatus.New;
            SelectedOrder.OrderDate = DateTime.Now;
            SelectedOrder.CreatedDate = DateTime.Now;
            SelectedOrder.LastUpdatedDate = DateTime.Now;
            SelectedOrder.ExpirationDate = DateTime.Now;

            return OrderService.Add(SelectedOrder);
        }
    }
}
