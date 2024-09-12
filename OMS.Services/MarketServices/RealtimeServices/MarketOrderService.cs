using DevExpress.Mvvm.Native;
using OMS.Core.Enums;
using OMS.Core.Models;
using OMS.Core.Services.Cache;
using OMS.Core.Services.MarketServices.RealtimeServices;
using OMS.DataAccess.Repositories.MarketRepositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Threading;

namespace OMS.Services.MarketServices.RealtimeServices
{
    public class MarketOrderService : IMarketOrderService
    {
        const int Tick = 2000;
        private readonly string cacheKeyPrefix = "MarketOrders_";
        bool FetchData = false;

        IMarketOrderRepository marketOrderRepository;
        IStockDataService StockDataService;
        ICacheService CacheService;

        public event Action<string> DataUpdated;

        readonly DispatcherTimer updateTimer;

        public MarketOrderService(IMarketOrderRepository marketOrderRepository,
            ICacheService cacheService,
            IStockDataService stockDataService)
        {
            StockDataService = stockDataService;
            this.marketOrderRepository = marketOrderRepository;
            CacheService = cacheService;
            updateTimer = new DispatcherTimer(DispatcherPriority.ApplicationIdle);
            InitTimer();
            StartSession();
        }

        public ObservableCollection<OrderBook> GetAll()
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<OrderBook> GetBuyOrders(string symbol)
        {
            var cacheKey = cacheKeyPrefix + symbol;
            if (CacheService.ContainsKey(cacheKey))
            {
                var buyingOrders = CacheService.Get<ObservableCollection<OrderBook>>(cacheKey);
                return buyingOrders.Where(o => o.Type == OrderType.Buy).ToObservableCollection();
            }

            var orders = marketOrderRepository.GetAll(symbol).ToObservableCollection<OrderBook>();
            var buyOrders = orders.Where(o => o.Type == OrderType.Buy).ToObservableCollection<OrderBook>();
            CacheService.Set(cacheKey, orders);
            return buyOrders;
        }

        public OrderBook GetById(int key)
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<OrderBook> GetSellOrders(string symbol)
        {
            var cacheKey = cacheKeyPrefix + symbol;
            if (CacheService.ContainsKey(cacheKey))
            {
                var sellingOrders = CacheService.Get<ObservableCollection<OrderBook>>(cacheKey);
                return sellingOrders.Where(o => o.Type == OrderType.Sell).ToObservableCollection<OrderBook>();
            }

            var orders = marketOrderRepository.GetAll(symbol).ToObservableCollection<OrderBook>();
            var sellOrders = orders.Where(o => o.Type == OrderType.Sell).ToObservableCollection<OrderBook>();
            CacheService.Set(cacheKey, orders);
            return sellOrders;
        }

        public void Refresh(object sender, EventArgs e)
        {
            foreach (var symbol in GetTrackedSymbols())
            {
                UpdateTradeRecords(symbol);
                DataUpdated?.Invoke(symbol);
            }
        }

        public void StartSession()
        {
            updateTimer.Start();
        }

        void InitTimer()
        {
            updateTimer.Interval = TimeSpan.FromMilliseconds(Tick);
            updateTimer.Tick += new EventHandler(Refresh);
        }

        private void UpdateTradeRecords(string symbol)
        {
            var cacheKey = cacheKeyPrefix + symbol;
            var latestTrades = marketOrderRepository.GetAll(symbol).Take(100).ToObservableCollection();
            CacheService.Set(cacheKey, latestTrades);
        }

        private IEnumerable<string> GetTrackedSymbols()
        {
            return CacheService.Get<ObservableCollection<string>>("StockSymbols") ?? StockDataService.GetStockSymbols();
        }
    }
}
