using DevExpress.Mvvm;
using DevExpress.Mvvm.UI.Native;
using OMS.Core.Services.AppServices;
using OMS.Core.Services.Cache;
using OMS.Core.Services.MarketServices.RealtimeServices;
using OMS.VM.Trades;
using System.Collections.ObjectModel;
using System.Linq;

namespace OMS.ViewModels
{
    public class StockMarketModel : ViewModelBase
    {
        private IStockDataService _stockService;

        private StockDetailViewModel _stockDetailsModel;
        public StockDetailViewModel StockDetailsModel
        {
            get { return _stockDetailsModel; }
            set { SetProperty(ref _stockDetailsModel, value, nameof(StockDetailsModel)); }
        }

        private OrderHistoryViewModel _orderHistoryModel;
        public OrderHistoryViewModel OrderHistoryModel
        {
            get => _orderHistoryModel;
            set
            {
                SetProperty(ref _orderHistoryModel, value, nameof(OrderHistoryModel));
            }
        }

        private OrdersListModel _openOrdersModel;
        public OrdersListModel OpenOrdersModel
        {
            get => _openOrdersModel;
            set
            {
                SetProperty(ref _openOrdersModel, value, nameof(OpenOrdersModel));
            }
        }

        private TradeViewModel _tradeModel;
        public TradeViewModel TradeHistoryModel
        {
            get => _tradeModel;
            set
            {
                SetProperty(ref _tradeModel, value, nameof(TradeHistoryModel));
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
                    UpdateViews();
                }
            }

        }

        private ObservableCollection<string> _stockSymbols;
        public ObservableCollection<string> StockSymbols
        {
            get { return _stockSymbols; }
            set { SetProperty(ref _stockSymbols, value, nameof(StockSymbols)); }
        }

        private StockChartModel _stockChartViewModel;
        public StockChartModel StockChartViewModel
        {
            get { return _stockChartViewModel; }
            set
            {
                SetProperty(ref _stockChartViewModel, value, nameof(StockChartViewModel));
            }
        }

        private OrderBookModel _orderBookModel;
        public OrderBookModel OrderBookModel
        {
            get => _orderBookModel;
            set
            {
                SetProperty(ref _orderBookModel, value, nameof(OrderBookModel));
            }
        }

        private TradeBookModel _tradeBookModel;
        public TradeBookModel TradeBookModel
        {
            get => _tradeBookModel;
            set
            {
                SetProperty(ref _tradeBookModel, value, nameof(TradeBookModel));
            }
        }

        public StockMarketModel(IStockDataService stockDataService, 
            IMarketOrderService marketOrderService,
            IMarketTradeService marketTradeService,
            IStockTradeDataService stockTradeDataService,
            IOrderService orderService,
            IAccountService accountService)
        {
            _stockService = stockDataService;
            _orderBookModel = new OrderBookModel(marketOrderService);
            _tradeBookModel = new TradeBookModel(marketTradeService);
            _stockDetailsModel = new StockDetailViewModel(_stockService);
            _stockChartViewModel = new StockChartModel(stockTradeDataService);
            _orderHistoryModel = new OrderHistoryViewModel(orderService);
            _openOrdersModel = new OrdersListModel(orderService,stockDataService,accountService);
            _tradeModel = new TradeViewModel();
            InitData();
        }

        private void InitData()
        {
            StockSymbols = _stockService.GetStockSymbols();
            SelectedStockSymbol = StockSymbols.FirstOrDefault();
        }
        
        private void UpdateViews()
        {
            if (SelectedStockSymbol != null)
            {
                StockChartViewModel.SelectedStockSymbol = SelectedStockSymbol;
                StockDetailsModel.Symbol = SelectedStockSymbol;
                OrderBookModel.SelectedStockSymbol = SelectedStockSymbol;
                TradeBookModel.SelectedStockSymbol = SelectedStockSymbol;
            }
        }
    }
}
