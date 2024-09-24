using DevExpress.Mvvm;
using OMS.Core.Models;
using OMS.Core.Services.MarketServices.RealtimeServices;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Threading;

namespace OMS.ViewModels
{
    public class TradeBookModel : ViewModelBase, IDisposable
    {
        private const int MaxTradeCount = 100;
        private readonly IMarketTradeService _marketTradeService;

        private string _selectedStockSymbol;
        public string SelectedStockSymbol
        {
            get => _selectedStockSymbol;
            set
            {
                if(_selectedStockSymbol != value)
                {
                    if (SetProperty(ref _selectedStockSymbol, value, nameof(SelectedStockSymbol)))
                    {
                        AddStockTrades();
                    }

                }
            }
        }
        
        private ObservableCollection<TradeBook> _stockTrades;
        public ObservableCollection<TradeBook> StockTrades
        {
            get => _stockTrades;
            private set => SetProperty(ref _stockTrades, value,nameof(StockTrades));
        }

        public TradeBookModel(IMarketTradeService marketTradeService)
        {
            _marketTradeService = marketTradeService;
            _stockTrades = new ObservableCollection<TradeBook>();

            _marketTradeService.DataUpdated += OnDataUpdated;
        }

        private void AddStockTrades()
        {
            if (!string.IsNullOrEmpty(SelectedStockSymbol))
            {
                var trades = _marketTradeService.GetAllBySymbol(SelectedStockSymbol);
                
                StockTrades.Clear();
                foreach (var trade in trades)
                {
                    StockTrades.Add(trade);
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
                var trades = _marketTradeService.GetAllBySymbol(SelectedStockSymbol);

                if (trades.Any())
                {
                    foreach (var trade in trades.Reverse())
                    {
                        if (StockTrades.Count >= MaxTradeCount)
                        {
                            StockTrades.RemoveAt(StockTrades.Count - 1);
                        }
                        StockTrades.Insert(0, trade);
                    }
                }
            }
        }

        public void Dispose()
        {
            _marketTradeService.DataUpdated -= OnDataUpdated;
        }
    }
}
