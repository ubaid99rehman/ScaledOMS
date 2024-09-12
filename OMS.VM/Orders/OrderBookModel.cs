using DevExpress.Mvvm;
using OMS.Core.Models;
using OMS.Core.Services.MarketServices.RealtimeServices;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Threading;

namespace OMS.ViewModels
{
    public class OrderBookModel : ViewModelBase
    {
        const int MaxTradeCount = 100;

        IMarketOrderService MarketOrderService;
        private string _selectedStockSymbol;
        public string SelectedStockSymbol
        {
            get => _selectedStockSymbol;
            set
            {
                if (_selectedStockSymbol != value)
                {
                    if (SetProperty(ref _selectedStockSymbol, value, nameof(SelectedStockSymbol)))
                    {
                        AddStockTrades();
                    }

                }
            }
        }

        private ObservableCollection<OrderBook> _stockBuyingOrders;
        public ObservableCollection<OrderBook> StockBuyingOrders
        {
            get { return _stockBuyingOrders; }
            set
            {
                _stockBuyingOrders = value;
            }
        }

        private ObservableCollection<OrderBook> _stockSellingOrders;
        public ObservableCollection<OrderBook> StockSellingOrders
        {
            get { return _stockSellingOrders; }
            set
            {
                _stockSellingOrders = value;
            }
        }

        public OrderBookModel(IMarketOrderService marketOrderService)
        {
            StockBuyingOrders = new ObservableCollection<OrderBook>();
            StockSellingOrders = new ObservableCollection<OrderBook>();
            AddInitialTrades();
            MarketOrderService = marketOrderService;
            MarketOrderService.DataUpdated += OnDataUpdated;

        }

        public void AddInitialTrades()
        {
           
        }

        private void OnTradeTimerTick(object sender, EventArgs e)
        {
            
        }

        private void AddStockTrades()
        {
            if (!string.IsNullOrEmpty(SelectedStockSymbol))
            {
                var buyOrders = MarketOrderService.GetBuyOrders(SelectedStockSymbol);
                var sellOrders = MarketOrderService.GetSellOrders(SelectedStockSymbol);

                StockBuyingOrders.Clear();
                StockSellingOrders.Clear();

                foreach (var order in buyOrders)
                {
                    StockBuyingOrders.Add(order);
                }
                foreach (var order in sellOrders)
                {
                    StockSellingOrders.Add(order);
                }
            }
        }

        private void OnDataUpdated(string symbol)
        {
            if (symbol == SelectedStockSymbol)
            {
                UpdateStockTrades();
            }
        }

        private void UpdateStockTrades()
        {
            if (!string.IsNullOrEmpty(SelectedStockSymbol))
            {
                var buyOrders = MarketOrderService.GetBuyOrders(SelectedStockSymbol);
                var sellOrders = MarketOrderService.GetSellOrders(SelectedStockSymbol);

                if(buyOrders.Any())
                {
                    foreach (var trade in buyOrders.Reverse())
                    {
                        if (buyOrders.Count >= MaxTradeCount)
                        {
                            StockBuyingOrders.RemoveAt(buyOrders.Count - 1);
                        }
                        StockBuyingOrders.Insert(0, trade);
                    }
                }
    
                if (sellOrders.Any())
                {
                    foreach (var trade in sellOrders.Reverse())
                    {
                        if (sellOrders.Count >= MaxTradeCount)
                        {
                            StockSellingOrders.RemoveAt(sellOrders.Count - 1);
                        }
                        StockSellingOrders.Insert(0, trade);
                    }
                }
            }
        }

        public void Dispose()
        {
            MarketOrderService.DataUpdated -= OnDataUpdated;
        }

    }
}
