using DevExpress.Mvvm.Native;
using OMS.Core.Core.Models.Books;
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
    public class MarketTradeService : IMarketTradeService
    {
        const int Tick = 2000;
        private readonly string cacheKeyPrefix = "MarketTrades_";
        public event Action<string> DataUpdated;
        readonly DispatcherTimer updateTimer;
        IMarketTradeRepository marketTradeRepository;
        IStockDataService StockDataService;
        ICacheService CacheService;

        //Constructor
        public MarketTradeService(IMarketTradeRepository marketTradeRepository, ICacheService cacheService, IStockDataService stockDataService)
        {
            StockDataService = stockDataService;
            this.marketTradeRepository = marketTradeRepository;
            CacheService = cacheService;
            
            updateTimer = new DispatcherTimer(DispatcherPriority.ApplicationIdle);
            InitTimer();
        }

        //Public Access Methods Implementation
        public ObservableCollection<BookBase> GetAllBySymbol(string symbol)
        {
            StartSession();
            var cacheKey = cacheKeyPrefix + symbol;
            if (CacheService.ContainsKey(cacheKey))
            {
                return CacheService.Get<ObservableCollection<BookBase>>(cacheKey);
            }

            var trades = marketTradeRepository.GetAll(symbol).ToObservableCollection();
            CacheService.Set(cacheKey, trades);
            return trades;
        }
        public BookBase GetById(int key)
        {
            return marketTradeRepository.GetById(key);
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
        public ObservableCollection<BookBase> GetAll()
        {
            throw new NotImplementedException();
        }

        #region Private Methods
        private void InitTimer()
        {
            updateTimer.Interval = TimeSpan.FromMilliseconds(Tick);
            updateTimer.Tick += new EventHandler(Refresh);
        }
        private void UpdateTradeRecords(string symbol)
        {
            var cacheKey = cacheKeyPrefix + symbol;
            var latestTrades = marketTradeRepository.GetAll(symbol).Take(100).ToList();

            var tradeCollection = latestTrades.ToObservableCollection();
            CacheService.Set(cacheKey, tradeCollection);
        }
        private IEnumerable<string> GetTrackedSymbols()
        {
            return CacheService.Get<ObservableCollection<string>>("StockSymbols") ?? StockDataService.GetStockSymbols();
        } 
        #endregion
    }
}
