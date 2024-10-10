using DevExpress.Mvvm.Native;
using OMS.Core.Core.Models.Books;
using OMS.Core.Models;
using OMS.Core.Models.Orders;
using OMS.Core.Services;
using OMS.Core.Services.Cache;
using OMS.Core.Services.MarketServices.RealtimeServices;
using OMS.DataAccess.Repositories.MarketRepositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;

namespace OMS.Services.MarketServices.RealtimeServices
{
    public class MarketTradeService : IMarketTradeService
    {
        private readonly string cacheKeyPrefix = "MarketTrades_";
        int MaxTradeCount = 100;
        public event Action<string> DataUpdated;

        #region Services
        IMarketTradeRepository marketTradeRepository;
        IStockDataService StockDataService;
        ICacheService CacheService; 
        #endregion

        //Constructor
        public MarketTradeService(IMarketTradeRepository tradeRepository, 
            ITimerService timerService, ICacheService cacheService, 
            IStockDataService stockDataService)
        {
            StockDataService = stockDataService;
            marketTradeRepository = tradeRepository;
            CacheService = cacheService;

            timerService.SecondTick += OnTimerTick; 
            timerService.Start();
        }

        public ICollectionView GetAll(string symbol)
        {
            string cacheKey = cacheKeyPrefix + symbol;
            if (CacheService.ContainsKey(cacheKey))
            {
                return GetCollectionView(GetTradesFromCache(symbol));
            }
            LoadTrades(symbol);
            return GetCollectionView(GetTradesFromCache(symbol));
        }
        private ICollectionView GetCollectionView(ObservableCollection<BookBase> collection)
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
            string cacheKey = cacheKeyPrefix + symbol;
            var trades = FetchTrades(symbol);
            if(trades !=null && trades.Count > 0)
            {
                CacheService.Set(cacheKey, trades);
            }
            else
            {
                CacheService.Set(cacheKey, new ObservableCollection<BookBase>());
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
            var cacheKey = cacheKeyPrefix + symbol;
            var trades = GetTradesFromCache(symbol);
            if(trades == null)
            {
                trades = new ObservableCollection<BookBase>();
            }
            ObservableCollection<BookBase> latestTrades = FetchTrades(symbol);
            if (latestTrades.Any())
            {
                int Count = trades.Count + latestTrades.Count;
                if (Count > MaxTradeCount)
                {
                    for (int i = 0; i < Count-MaxTradeCount; i++)
                    {
                        trades.RemoveAt(i);
                    }
                }
                foreach(BookBase trade in latestTrades)
                {
                    trades.Add(trade);
                }
            }
            CacheService.Set(cacheKey, trades);
        }
        
        private IEnumerable<string> GetTrackedSymbols()
        {
            return StockDataService.GetStockSymbols();
        } 
        
        private ObservableCollection<BookBase> FetchTrades(string symbol)
        {
            ObservableCollection<BookBase> trades = marketTradeRepository.GetTradesBySymbol(symbol).ToObservableCollection();
            if (trades != null && trades.Count > 0)
            {
                return trades;
            }
            return new ObservableCollection<BookBase>();
        }
        private ObservableCollection<BookBase> GetTradesFromCache(string symbol)
        {
            string cacheKey = cacheKeyPrefix + symbol;
            return CacheService.Get<ObservableCollection<BookBase>>(cacheKey);
        }
    }
}
