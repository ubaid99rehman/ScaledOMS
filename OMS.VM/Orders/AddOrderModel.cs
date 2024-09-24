using DevExpress.Mvvm;
using DevExpress.Mvvm.Native;
using OMS.Core.Models;
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
        IStockDataService StockDataService;
        IOrderService OrderService;
        IAccountService AccountService;

        private Order _selectedOrder;
        public Order SelectedOrder
        {
            get => _selectedOrder;
            set
            {
                SetProperty(ref _selectedOrder, value, nameof(SelectedOrder));
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
                        UpdateStock();
                        ClearFields();
                    }
                }
            }

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

        private ObservableCollection<string> _orderTypes;
        public ObservableCollection<string> OrderTypes
        {
            get { return _orderTypes; }
            set
            {
                SetProperty(ref _orderTypes, value, nameof(OrderTypes));
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
        
        private decimal _stockPrice;
        public decimal StockPrice
        {
            get => SelectedStock?.LastPrice ?? 0;
            set
            {
                SetProperty(ref _stockPrice, value, nameof(StockPrice));
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

        private ObservableCollection<int> _accounts;
        public ObservableCollection<int> AccountsList
        {
            get { return _accounts; }
            set { SetProperty(ref _accounts, value, nameof(AccountsList)); }
        }

        private ObservableCollection<string> _stockSymbols;
        public ObservableCollection<string> StockSymbols
        {
            get => _stockSymbols;
            set
            {
                SetProperty(ref _stockSymbols, value, nameof(StockSymbols));
            }

        }

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

        private void ClearFields()
        {
            SelectedOrder = new Order();
            Quantity = 1;
            Total = SelectedStock.LastPrice;
            SelectedOrder.OrderType = OrderType.Buy;
        }

        private void UpdateTotal()
        {
            Total = SelectedStock.LastPrice * Quantity;
        }

        private void UpdateStock()
        {
            SelectedStock = StockDataService.GetStock(SelectedStockSymbol);
        }

        private void InitData()
        {
            OrderTypes = Enum.GetValues(typeof(OrderType)).Cast<OrderType>().Select(e =>
            e.ToString()).ToObservableCollection();
            AccountsList = AccountService.GetAccountsList();
            StockSymbols = StockDataService.GetStockSymbols();
            SelectedStockSymbol = StockSymbols.First();
            SelectedStock = StockDataService.GetStock(SelectedStockSymbol);
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
