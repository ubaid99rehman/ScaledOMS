using DevExpress.Mvvm;
using OMS.Core.Enums;
using OMS.Core.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace OMS.ViewModels
{
    public class OrderBookModel : ViewModelBase
    {
        private readonly DispatcherTimer _tradeTimer;
        private readonly Random _random = new Random();

        private Stock _stock;
        public Stock Stock
        {
            get { return _stock; }
            set 
            { 
                if(SetProperty(ref _stock,value,nameof(Stock)))
                {
                    AddInitialTrades();
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

        public OrderBookModel(Stock stock)
        {
            _stock = stock;
            AddInitialTrades();

            _tradeTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(_random.Next(2, 6)) 
            };

            _tradeTimer.Tick += OnTradeTimerTick;
            _tradeTimer.Start();
        }

        public void AddInitialTrades()
        {
            StockBuyingOrders = new ObservableCollection<OrderBook>();
            StockSellingOrders = new ObservableCollection<OrderBook>();

            for (int i = 0; i < 100; i++)
            {
                int quantity = _random.Next(1, 10);
                var order = new OrderBook
                {
                    Symbol = _stock.Symbol,
                    Quantity = quantity,
                    Price = Stock.LastPrice,
                    Total = Stock.LastPrice*quantity,
                    Timestamp = DateTime.Now.AddMinutes(_random.Next(0, 1000)),
                    Type = OrderType.Buy
                };

                StockBuyingOrders.Add(order);
            }

            for (int i = 0; i < 100; i++)
            {
                int quantity = _random.Next(1, 10);
                var order = new OrderBook
                {
                    Symbol = _stock.Symbol,
                    Quantity = quantity,
                    Price = Stock.LastPrice,
                    Total = Stock.LastPrice * quantity,
                    Timestamp = DateTime.Now.AddMinutes(_random.Next(0, 1000)),
                    Type = OrderType.Sell
                };

                StockSellingOrders.Add(order);
            }
        }

        private void OnTradeTimerTick(object sender, EventArgs e)
        {
            int quantity = _random.Next(1, 10);
            var order = new OrderBook
            {
                Symbol = _stock.Symbol,
                Quantity = _random.Next(1, 1000),
                Price = Stock.LastPrice,
                Total = Stock.LastPrice * quantity,
                Timestamp = DateTime.Now,
                Type = OrderType.Sell
            };

            StockSellingOrders.Add(order);
            quantity = _random.Next(1, 10);
            
            order = new OrderBook
            {
                Symbol = _stock.Symbol,
                Quantity = _random.Next(1, 1000),
                Price = Stock.LastPrice,
                Total = Stock.LastPrice * quantity,
                Timestamp = DateTime.Now,
                Type = OrderType.Buy
            };

            StockBuyingOrders.Add(order);

            _tradeTimer.Interval = TimeSpan.FromSeconds(_random.Next(2, 6));
        }

    }
}
