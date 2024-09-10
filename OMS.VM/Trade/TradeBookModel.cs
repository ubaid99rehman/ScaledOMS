using DevExpress.Mvvm;
using OMS.Core.Enums;
using OMS.Core.Models;
using System;
using System.Collections.ObjectModel;

using System.Windows.Threading;

namespace OMS.ViewModels
{
    public class TradeBookModel : ViewModelBase
    {
        private readonly DispatcherTimer _tradeTimer;
        private readonly Random _random = new Random();

        private Stock _stock;
        public Stock Stock
        {
            get { return _stock; }
            set
            {
                if (SetProperty(ref _stock, value, nameof(Stock)))
                {
                    _tradeTimer.Stop();
                    AddInitialTrades();
                    _tradeTimer.Start();
                }
            }
        }

        private ObservableCollection<TradeBook> _stockTrades;
        public ObservableCollection<TradeBook> StockTrades
        {
            get { return _stockTrades; }
            set
            {
                _stockTrades = value;
            }
        }

        public TradeBookModel(Stock stock)
        {
            _stock = stock;
            AddInitialTrades();

            _tradeTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(_random.Next(2, 6)) // Random interval between 2 and 5 seconds
            };

            _tradeTimer.Tick += OnTradeTimerTick;
            _tradeTimer.Start();
        }

        private void AddInitialTrades()
        {
            _stockTrades = new ObservableCollection<TradeBook>();

            for (int i = 0; i < 100; i++)
            {
                int quantity = _random.Next(1, 1000);
                var trade = new TradeBook
                {
                    Symbol = Stock.Symbol,
                    Quantity = quantity,
                    Price = Stock.LastPrice,
                    Timestamp = DateTime.Now.AddMinutes(_random.Next(0, 1440)) 
                };

                _stockTrades.Add(trade);
            }
        }

        private void OnTradeTimerTick(object sender, EventArgs e)
        {
            
            int quantity = _random.Next(10, 1000);
            var trade = new TradeBook
            {
                Symbol = Stock.Symbol,
                Price = Stock.LastPrice,
                Quantity = quantity,
                Timestamp = DateTime.Now,
                Type = OrderType.Sell
            };
            
            _stockTrades.Add(trade);
            quantity = _random.Next(10, 1000);

            trade = new TradeBook
            {
                Symbol = Stock.Symbol,
                Price = Stock.LastPrice,
                Quantity = quantity,
                Timestamp = DateTime.Now,
                Type = OrderType.Buy
            };

            _stockTrades.Add(trade);

            // Update the timer interval to be random again
            _tradeTimer.Interval = TimeSpan.FromSeconds(_random.Next(2, 6));
        }

    }

}
