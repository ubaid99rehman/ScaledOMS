using DevExpress.Mvvm;
using DevExpress.Mvvm.Native;
using OMS.Core.Models;
using OMS.Core.Models.Stocks;
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
        IStockDataService StockDataService;
        IOrderService OrderService;
        IAccountService AccountService;

        #region Private Members
        private Order _selectedOrder;
        private string _selectedStockSymbol;
        private IStock _selectedStock;
        private ObservableCollection<string> _orderTypes;
        private decimal _quantity;
        private decimal _stockPrice;
        private decimal _total;
        private ObservableCollection<int> _accounts;
        private ObservableCollection<string> _stockSymbols;
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
                SelectedOrder.Price = value;
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
        #endregion

        //Constructor
        public AddOrderModel(IOrderService _orderService,
            IStockDataService _stockDataService, IAccountService _accountService)
        {
            OrderService = _orderService;
            AccountService = _accountService;
            StockDataService = _stockDataService;
            SelectedStock = new Stock();
            SelectedOrder = new Order();
            InitData();
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

        //Public Methods
        public void ClearFields()
        {
            SelectedOrder = new Order();
            SelectedOrder.OrderType = null;
            Quantity = 0;
            Total = 0;
        }
        public bool AddOrder()
        {
            SelectedOrder.Quantity = (int)Quantity;
            SelectedOrder.Total = Quantity * StockPrice;
            SelectedOrder.Price= StockPrice;
            SelectedOrder.OrderID = 010124;
            SelectedOrder.Status = OrderStatus.New;
            SelectedOrder.OrderDate = DateTime.Now;
            SelectedOrder.LasUpdatedDate = DateTime.Now;
            SelectedOrder.AddedBy = 1;
            SelectedOrder.Symbol = SelectedStockSymbol;

            return OrderService.Add(SelectedOrder);
        }
    }
}
