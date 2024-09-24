using DevExpress.Mvvm;
using DevExpress.Mvvm.Native;
using OMS.Core.Models;
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
        IStockDataService StockDataService;
        IOrderService OrderService;
        IAccountService AccountService;

        private StockDetailViewModel _stockDetailsModel;
        public StockDetailViewModel StockDetailsModel
        {
            get { return _stockDetailsModel; }
            set { SetProperty(ref _stockDetailsModel, value, nameof(StockDetailsModel)); }
        }

        private ObservableCollection<int> _accounts;
        public ObservableCollection<int> Accounts
        {
            get { return _accounts; }
            set
            {
                SetProperty(ref _accounts, value, nameof(Accounts));
            }
        }

        private ObservableCollection<Order> _orders;
        public ObservableCollection<Order> Orders
        {
            get { return _orders; }
            set { SetProperty(ref _orders, value, nameof(Orders)); }
        }

        private ObservableCollection<Order> _stockOrders;
        public ObservableCollection<Order> StockOrders
        {
            get { return _stockOrders; }
            set { SetProperty(ref _stockOrders, value, nameof(StockOrders)); }
        }

        private ObservableCollection<string> _stockSymbols;
        public ObservableCollection<string> StockSymbols
        {
            get { return _stockSymbols; }
            set { SetProperty(ref _stockSymbols, value, nameof(StockSymbols)); }
        }

        public ObservableCollection<string> OrderTypes { get; }

        private int _selectedAccount;
        public int SelectedAccount
        {
            get { return _selectedAccount; }
            set
            {
                SetProperty(ref _selectedAccount, value, nameof(SelectedAccount));
            }
        }

        private Order _selectedOrder;
        public Order SelectedOrder
        {
            get => _selectedOrder;
            set
            {
                if (SetProperty(ref _selectedOrder, value, nameof(SelectedOrder)))
                {
                    if (value is Order order && order.Total >= 1 && !string.IsNullOrEmpty(order.Symbol))
                    {
                        if (order.OrderID >= 1 && order.Quantity >= 1)
                        {
                            SelectedOrder.OrderID = order.OrderID;
                            Quantity = (decimal)order.Quantity;

                            Total = (decimal)order.Total;

                            SelectedAccount = (int)order.AccountID;
                            OrderType = (OrderType)order.OrderType;

                            SelectedStockSymbol = order.Symbol;
                        }
                    }
                }
            }
        }

        private Order _lastOrder;
        public Order LastOrder
        {
            get => _lastOrder;
            set => SetProperty(ref _lastOrder, value, nameof(LastOrder));
        }

        private Stock _selectedStock;
        public Stock SelectedStock
        {
            get { return _selectedStock; }
            set
            {
                SetProperty(ref _selectedStock, value, nameof(SelectedStock));
            }
        }

        private string _selectedStockSymbol;
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
                        ClearFields();
                    }
                }
            }

        }

        private OrderType _orderType;
        public OrderType OrderType
        {
            get { return _orderType; }
            set
            {
                SetProperty(ref _orderType, value, nameof(OrderType));
            }
        }

        private decimal _quantity;
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

        private decimal _total;
        public decimal Total
        {
            get { return _total; }
            set
            {
                SetProperty(ref _total, value, nameof(Total));
                SelectedOrder.Price = value;
            }
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

        private void UpdateData()
        {
            SelectedStock = StockDataService.GetStock(_selectedStockSymbol);
            StockDetailsModel.Symbol = SelectedStockSymbol;

            LastOrder = OrderService.GetLastOrderByUser();
            StockOrders = OrderService.GetOrdersByStock(_selectedStockSymbol);
            Orders = OrderService.GetOpenOrders();
        }

        #region Constructors
        public OrderModel(IStockDataService stockDataService, IOrderService orderService, IAccountService accountService)
        {

            _stockDetailsModel = new StockDetailViewModel(stockDataService);
            SelectedOrder = new Order();
            SelectedStock = new Stock();


            #region Services Initialization
            StockDataService = stockDataService;
            AccountService = accountService;
            OrderService = orderService;
            #endregion
            
            #region Initialized Collections
            Accounts = new ObservableCollection<int>();
            Orders = new ObservableCollection<Order>();
            StockSymbols = new ObservableCollection<string>();
            OrderTypes = Enum.GetValues(typeof(OrderType)).Cast<OrderType>().Select(e =>
            e.ToString()).ToObservableCollection();
            #endregion
          

            InitData();
        }
        #endregion

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
            Orders = OrderService.GetOpenOrders();

        }

        public void AddOrder(out bool isAdded, out string message)
        {
            isAdded = false;
            message = string.Empty;
            SelectedOrder.AccountID = SelectedAccount;
            SelectedOrder.OrderType = OrderType;
            SelectedOrder.Symbol = SelectedStockSymbol;
            SelectedOrder.Quantity = (int)Quantity;
            SelectedOrder.Price = SelectedStock.LastPrice;
            SelectedOrder.Total = Total;
            SelectedOrder.CreatedDate = DateTime.Now;
            SelectedOrder.OrderDate = DateTime.Now;

            var newOrder = new Order
            {
                OrderID = 99945,
                AccountID = SelectedAccount,
                OrderType = OrderType,
                Symbol = SelectedStockSymbol,
                Quantity = (int)Quantity,
                Price = SelectedStock.LastPrice,
                Total = Total,
                CreatedDate = DateTime.Now,
                OrderDate = DateTime.Now
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
                if (SelectedOrder.Status != OrderStatus.Cancelled
                    || SelectedOrder.Status != OrderStatus.Fulfilled)
                {
                    OrderService.CancelOrder(SelectedOrder, out message);
                    if (message.Equals("Updated!"))
                    {
                        isCancelled = true;
                        UpdateData();
                    }
                }
                else
                {

                }
                UpdateData();
            }
        }

        public void ClearOrder()
        {
            ClearFields();
        }

        public bool isValidOrder(Order SelectedOrder)
        {
            if (
               SelectedOrder != null &&
               !string.IsNullOrEmpty(SelectedOrder.Symbol) &&
               SelectedOrder.Quantity > 0 &&
               SelectedOrder.Price > 0 &&
               SelectedOrder.AccountID != 0
               )
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
