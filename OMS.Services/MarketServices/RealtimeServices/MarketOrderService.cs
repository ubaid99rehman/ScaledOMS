using DevExpress.Mvvm.Native;
using OMS.Core.Core.Models.Books;
using OMS.Core.Models;
using OMS.Core.Models.Orders;
using OMS.Core.Services;
using OMS.Core.Services.Cache;
using OMS.Core.Services.MarketServices.RealtimeServices;
using OMS.DataAccess.Repositories.MarketRepositories;
using OMS.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;

namespace OMS.Services.MarketServices.RealtimeServices
{
    public class MarketOrderService : IMarketOrderService
    {
        private readonly string cacheBuyPrefix = "MarketBuyOrders_";
        private readonly string cacheSellPrefix = "MarketSellOrders_";
        public event Action<string> DataUpdated;
        int MaxOrdersCount = 100; 

        #region Services
        IMarketOrderRepository marketOrderRepository;
        IStockDataService StockDataService;
        ICacheService CacheService; 
        #endregion

        //Constructor
        public MarketOrderService(IMarketOrderRepository marketOrderRepository, 
            ITimerService timerService, ICacheService cacheService, 
            IStockDataService stockDataService)
        {
            StockDataService = stockDataService;
            this.marketOrderRepository = marketOrderRepository;
            CacheService = cacheService;
            timerService.SecondTick += OnTimerTick; 
            timerService.Start();
        }

        public ICollectionView GetBuyOrders(string symbol)
        {
            string cacheKey = cacheBuyPrefix + symbol;
            if (CacheService.ContainsKey(cacheKey))
            {
                return GetBuyOrdersCollectionView(GetBuyOrdersFromCache(symbol));
            }
            LoadTrades(symbol);
            return GetBuyOrdersCollectionView(GetBuyOrdersFromCache(symbol));
        }
        public ICollectionView GetSellOrders(string symbol)
        {
            string cacheKey = cacheSellPrefix + symbol;
            if (CacheService.ContainsKey(cacheKey))
            {
                return GetBuyOrdersCollectionView(GetSellOrdersFromCache(symbol));
            }
            LoadTrades(symbol);
            return GetBuyOrdersCollectionView(GetSellOrdersFromCache(symbol));
        }
        private ICollectionView GetBuyOrdersCollectionView(ObservableCollection<BookBase> collection)
        {
            if (collection == null)
            {
                collection = new ObservableCollection<BookBase>();
            }
            var collectionViewSource = new CollectionViewSource { Source = collection };
            ICollectionViewLiveShaping liveShapingView = collectionViewSource.
                View as ICollectionViewLiveShaping;
            if (liveShapingView != null)
            {
                if (liveShapingView.CanChangeLiveSorting)
                {
                    liveShapingView.IsLiveSorting = true;
                    liveShapingView.LiveSortingProperties.Add(nameof(IOrder.OrderDate));
                    collectionViewSource.SortDescriptions.Add(new SortDescription("Timestamp", ListSortDirection.Descending));
                }
            }
            return collectionViewSource.View;

        }
        private ICollectionView GetSellOrdersCollectionView(ObservableCollection<BookBase> collection)
        {
            if (collection == null)
            {
                collection = new ObservableCollection<BookBase>();
            }
            var collectionViewSource = new CollectionViewSource { Source = collection };
            ICollectionViewLiveShaping liveShapingView = collectionViewSource.
                View as ICollectionViewLiveShaping;
            if (liveShapingView != null)
            {
                if (liveShapingView.CanChangeLiveSorting)
                {
                    liveShapingView.IsLiveSorting = true;
                    liveShapingView.LiveSortingProperties.Add(nameof(IOrder.OrderDate));
                    collectionViewSource.SortDescriptions.Add(new SortDescription("Timestamp", ListSortDirection.Descending));
                }
            }
            return collectionViewSource.View;

        }

        private void LoadTrades(string symbol)
        {
            string buyCacheKey = cacheBuyPrefix + symbol;
            string sellCacheKey = cacheSellPrefix + symbol;

            var latestOrders = FetchOrders(symbol);
            ObservableCollection<BookBase> latestBuyOrders = latestOrders.Where(o => o.Type == OrderType.Buy).ToObservableCollection<BookBase>();
            ObservableCollection<BookBase> latestSellOrders = latestOrders.Where(o => o.Type == OrderType.Sell).ToObservableCollection<BookBase>();

            if (latestBuyOrders != null && latestBuyOrders.Count > 0)
            {
                CacheService.Set(buyCacheKey, latestBuyOrders);
            }
            else
            {
                CacheService.Set(buyCacheKey, new ObservableCollection<BookBase>());
            }
            if (latestSellOrders != null && latestSellOrders.Count > 0)
            {
                CacheService.Set(sellCacheKey, latestSellOrders);
            }
            else
            {
                CacheService.Set(sellCacheKey, new ObservableCollection<BookBase>());
            }
        }
        private void OnTimerTick(object sender, EventArgs e)
        {
            foreach (var symbol in GetTrackedSymbols())
            {
                UpdateTradeRecords(symbol);
                DataUpdated?.Invoke(symbol);
            }
        }
        private void UpdateTradeRecords(string symbol)
        {
            string buyCacheKey = cacheBuyPrefix + symbol;
            string sellCacheKey = cacheSellPrefix + symbol;

            //Cached Orders
            var sellOrders = GetSellOrdersFromCache(symbol);
            var buyOrders = GetBuyOrdersFromCache(symbol);
            if (buyOrders == null)
            {
                buyOrders = new ObservableCollection<BookBase>();
            }
            if (sellOrders == null)
            {
                sellOrders = new ObservableCollection<BookBase>();
            }

            //New Market Orders
            var latestOrders = FetchOrders(symbol);
            ObservableCollection<BookBase> latestBuyOrders = latestOrders.Where(o => o.Type == OrderType.Buy).ToObservableCollection<BookBase>();
            ObservableCollection<BookBase> latestSellOrders = latestOrders.Where(o => o.Type == OrderType.Sell).ToObservableCollection<BookBase>();
            
            //Add Market Orders to cache
            //Sell Orders
            if (latestSellOrders.Any())
            {
                int Count = sellOrders.Count + latestSellOrders.Count;
                if (Count > MaxOrdersCount)
                {
                    for (int i = 0; i < Count - MaxOrdersCount; i++)
                    {
                        sellOrders.RemoveAt(i);
                    }
                }
                foreach (BookBase trade in latestSellOrders)
                {
                    sellOrders.Add(trade);
                }
                CacheService.Set(sellCacheKey, sellOrders);
            }
            //Buy Orders
            if (latestBuyOrders.Any())
            {
                int Count = buyOrders.Count + latestBuyOrders.Count;
                if (Count > MaxOrdersCount)
                {
                    for (int i = 0; i < Count - MaxOrdersCount; i++)
                    {
                        buyOrders.RemoveAt(i);
                    }
                }
                foreach (BookBase trade in latestBuyOrders)
                {
                    buyOrders.Add(trade);
                }
                CacheService.Set(buyCacheKey, buyOrders);
            }
        }
        private IEnumerable<string> GetTrackedSymbols()
        {
            return StockDataService.GetStockSymbols();
        }

        private ObservableCollection<BookBase> FetchOrders(string symbol)
        {
            ObservableCollection<BookBase> orders = marketOrderRepository.GetOrdersBySymbol(symbol).ToObservableCollection();
            if (orders != null && orders.Count > 0)
            {
                return orders;
            }
            return new ObservableCollection<BookBase>();
        }
        private ObservableCollection<BookBase> GetBuyOrdersFromCache(string symbol)
        {
            string cacheKey = cacheBuyPrefix + symbol;
            return CacheService.Get<ObservableCollection<BookBase>>(cacheKey);
        }
        private ObservableCollection<BookBase> GetSellOrdersFromCache(string symbol)
        {
            string cacheKey = cacheSellPrefix + symbol;
            return CacheService.Get<ObservableCollection<BookBase>>(cacheKey);
        }
    }
}
