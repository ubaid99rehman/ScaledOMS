using DevExpress.Mvvm;
using OMS.Core.Services.AppServices;
using OMS.Core.Services.MarketServices.RealtimeServices;
using OMS.VM.Trades;
using System.Collections.ObjectModel;
using System.Linq;

namespace OMS.ViewModels
{
    public class StockMarketModel : ViewModelBase
    {
        //Service
        private IStockDataService _stockService;

        #region Private Members
        private StockDetailViewModel _stockDetailsModel;
        private OrderHistoryViewModel _orderHistoryModel;
        private OrdersListModel _openOrdersModel;
        private TradeViewModel _tradeModel;
        private string _selectedStockSymbol;
        private ObservableCollection<string> _stockSymbols;
        private StockChartModel _stockChartViewModel;
        private OrderBookModel _orderBookModel;
        private TradeBookModel _tradeBookModel;
        #endregion

        #region Public Members
        public StockDetailViewModel StockDetailsModel
        {
            get { return _stockDetailsModel; }
            set { SetProperty(ref _stockDetailsModel, value, nameof(StockDetailsModel)); }
        }
        public OrderHistoryViewModel OrderHistoryModel
        {
            get => _orderHistoryModel;
            set
            {
                SetProperty(ref _orderHistoryModel, value, nameof(OrderHistoryModel));
            }
        }
        public OrdersListModel OpenOrdersModel
        {
            get => _openOrdersModel;
            set
            {
                SetProperty(ref _openOrdersModel, value, nameof(OpenOrdersModel));
            }
        }
        public TradeViewModel TradeHistoryModel
        {
            get => _tradeModel;
            set
            {
                SetProperty(ref _tradeModel, value, nameof(TradeHistoryModel));
            }
        }
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
        public ObservableCollection<string> StockSymbols
        {
            get { return _stockSymbols; }
            set { SetProperty(ref _stockSymbols, value, nameof(StockSymbols)); }
        }
        public StockChartModel StockChartViewModel
        {
            get { return _stockChartViewModel; }
            set
            {
                SetProperty(ref _stockChartViewModel, value, nameof(StockChartViewModel));
            }
        }
        public OrderBookModel OrderBookModel
        {
            get => _orderBookModel;
            set
            {
                SetProperty(ref _orderBookModel, value, nameof(OrderBookModel));
            }
        }
        public TradeBookModel TradeBookModel
        {
            get => _tradeBookModel;
            set
            {
                SetProperty(ref _tradeBookModel, value, nameof(TradeBookModel));
            }
        } 
        #endregion

        //Constructor
        public StockMarketModel(IStockDataService stockDataService, IMarketOrderService marketOrderService,
            IMarketTradeService marketTradeService,IStockTradeDataService stockTradeDataService,
            IOrderService orderService,IAccountService accountService)
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

        //Private Methods
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
