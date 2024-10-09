using DevExpress.Mvvm;
using OMS.Core.Models;
using OMS.Core.Services.MarketServices.RealtimeServices;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Threading;

namespace OMS.ViewModels
{
    public class OrderBookModel : ViewModelBase
    {
        //Service
        IMarketOrderService MarketOrderService;

        private string _selectedStockSymbol;
        public string SelectedStockSymbol
        {
            get => _selectedStockSymbol;
            set
            {
                if (SetProperty(ref _selectedStockSymbol, value, nameof(SelectedStockSymbol)))
                {
                    // Fetch the updated orders when the symbol changes
                    if (!string.IsNullOrEmpty(SelectedStockSymbol))
                    {
                        StockSellingOrders = MarketOrderService.GetSellOrders(_selectedStockSymbol);
                        StockBuyingOrders = MarketOrderService.GetBuyOrders(_selectedStockSymbol);
                    }
                }
            }
        }

        private ICollectionView _stockBuyingOrders;
        public ICollectionView StockBuyingOrders
        {
            get => _stockBuyingOrders;
            set => SetProperty(ref _stockBuyingOrders, value, nameof(StockBuyingOrders)); 
        }

        private ICollectionView _stockSellingOrders;
        public ICollectionView StockSellingOrders
        {
            get => _stockSellingOrders;
            set => SetProperty(ref _stockSellingOrders, value, nameof(StockSellingOrders)); 
        }

        //Constructor
        public OrderBookModel(IMarketOrderService marketOrderService)
        {
            MarketOrderService = marketOrderService;
        }
    }
}
