using DevExpress.Mvvm;
using OMS.Core.Services.MarketServices.RealtimeServices;
using System.ComponentModel;

namespace OMS.ViewModels
{
    public class TradeBookModel : ViewModelBase
    {
        private readonly IMarketTradeService _marketTradeService;

        private string _selectedStockSymbol;
        public string SelectedStockSymbol
        {
            get => _selectedStockSymbol;
            set
            {
                if(_selectedStockSymbol != value)
                {
                    if(SetProperty(ref _selectedStockSymbol, value, nameof(SelectedStockSymbol)))
                    {
                        if (!string.IsNullOrEmpty(SelectedStockSymbol))
                        {
                            StockTrades = _marketTradeService.GetAll(SelectedStockSymbol);
                        }
                    }
                }
            }
        }

        private ICollectionView _stockTrades;
        public ICollectionView StockTrades
        {
            get => _stockTrades;
            set => SetProperty(ref _stockTrades, value, nameof(StockTrades));
        }
        
        //Constructor
        public TradeBookModel(IMarketTradeService marketTradeService)
        {
            _marketTradeService = marketTradeService;
        }
    }
}