﻿using DevExpress.Mvvm;
using System;
using System.Collections.ObjectModel;
using OMS.Core.Models;


namespace OMS.VM.Trades
{
    public class TradeViewModel : ViewModelBase
    {
        private ObservableCollection<Trade> _orders;
        public ObservableCollection<Trade> Orders
        {
            get => _orders;
            set => SetProperty(ref _orders, value, nameof(Orders));
        }


        private Trade _selectedOrder;
        public Trade SelectedOrder
        {
            get => _selectedOrder;
            set => SetProperty(ref _selectedOrder, value, nameof(SelectedOrder));
        }

        public TradeViewModel()
        {
            LoadOrders();
        }

        private void LoadOrders()
        {
            Orders = new ObservableCollection<Trade>
            {
                new Trade { TradeID = 1, TradeDate = DateTime.Now.AddDays(-1), Symbol = "AAPL", TradeType = "Buy", AccountID = "ACC1001", Price = 145.25m, Quantity = 50 },
                new Trade { TradeID = 2, TradeDate = DateTime.Now.AddDays(-2), Symbol = "GOOGL", TradeType = "Sell", AccountID = "ACC1002", Price = 2753.11m, Quantity = 20 },
                new Trade { TradeID = 3, TradeDate = DateTime.Now.AddDays(-3), Symbol = "MSFT", TradeType = "Buy", AccountID = "ACC1001", Price = 299.72m, Quantity = 100 },
                new Trade { TradeID = 4, TradeDate = DateTime.Now.AddDays(-4), Symbol = "AMZN", TradeType = "Sell", AccountID = "ACC1003", Price = 3478.05m, Quantity = 10 },
                new Trade { TradeID = 5, TradeDate = DateTime.Now.AddDays(-5), Symbol = "TSLA", TradeType = "Buy", AccountID = "ACC1004", Price = 720.30m, Quantity = 15 },
                new Trade { TradeID = 6, TradeDate = DateTime.Now.AddDays(-6), Symbol = "NFLX", TradeType = "Sell", AccountID = "ACC1002", Price = 580.12m, Quantity = 25 },
                new Trade { TradeID = 7, TradeDate = DateTime.Now.AddDays(-7), Symbol = "NVDA", TradeType = "Buy", AccountID = "ACC1001", Price = 195.47m, Quantity = 40 },
                new Trade { TradeID = 8, TradeDate = DateTime.Now.AddDays(-8), Symbol = "FB", TradeType = "Sell", AccountID = "ACC1003", Price = 358.73m, Quantity = 50 },
                new Trade { TradeID = 9, TradeDate = DateTime.Now.AddDays(-9), Symbol = "BABA", TradeType = "Buy", AccountID = "ACC1004", Price = 157.24m, Quantity = 100 },
                new Trade { TradeID = 10, TradeDate = DateTime.Now.AddDays(-10), Symbol = "INTC", TradeType = "Sell", AccountID = "ACC1002", Price = 54.37m, Quantity = 200 }
            };
        }
    }
}
