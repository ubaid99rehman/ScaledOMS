using DevExpress.Mvvm.Native;
using OMS.Common.Helper;
using OMS.Core.Models.Stocks;
using OMS.Core.Services.Cache;
using OMS.Core.Services.MarketServices.RealtimeServices;
using OMS.DataAccess.Repositories.MarketRepositories;
using OMS.Enums;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace OMS.Services.MarketServices.RealtimeServices
{
    public class StockTradeDataService : IStockTradeDataService
    {
        public event Action DataUpdated;
        IStockRepository StockRepository;
        IStockTradeDataRepository StockTradeDataRepository;
        IStockDataService StockDataService;
        ICacheService CacheService;
        int Tick = 60000;
        readonly DispatcherTimer updateTimer;
        bool FetchData = false;

        public StockTradeDataService(IStockRepository stockRepository, 
            IStockTradeDataRepository stockTradeDataRepository, 
            IStockDataService stockDataService,
            ICacheService cacheService)
        {
            StockRepository = stockRepository;
            StockTradeDataRepository = stockTradeDataRepository;
            StockDataService = stockDataService;
            CacheService = cacheService;
            updateTimer = new DispatcherTimer(DispatcherPriority.ApplicationIdle);
            InitTimer();
        }

        //Public Access Methods Implementation
        public ObservableCollection<IStockTradingData> GetAll()
        {
            throw new NotImplementedException();
        }
        public IStockTradingData GetById(int key)
        {
            throw new NotImplementedException();
        }
        public IStockTradingData GetBySymbol(string symbol)
        {
            TradeTimeInterval interval = TradeTimeInterval.Minute;
            string cacheKey = $"{symbol}_{interval}";
            if(CacheService.ContainsKey(cacheKey))
            {
                var data = CacheService.Get<ObservableCollection<IStockTradingData>>(cacheKey);
                return data.LastOrDefault();
            }

            return StockTradeDataRepository.GetTradeData(symbol, 1, interval).GetAwaiter().GetResult();
        }
        public ObservableCollection<IStockTradingData> GetTradingData(string symbol, DateTime startTime, int points=180, TradeTimeInterval interval= TradeTimeInterval.Minute)
        {
            if(!FetchData)
            {
                StartSession();
                FetchData = true;
            }

            if(startTime == null)
            {
                startTime = DateTime.Now;
            }
            DateTime fromTime = DateTimeHelper.GetStartTime(interval, points, startTime);

            string cacheKey = $"{symbol}_{interval}";
            
            if (CacheService.ContainsKey(cacheKey))
            {
            var cachedData = CacheService.Get<ObservableCollection<IStockTradingData>>(cacheKey);

                var tradeData = cachedData.Where(trade => trade.RecordedTime >= fromTime && trade.RecordedTime < startTime)
                    .ToList();

                if (tradeData.Count >= points)
                {
                    return tradeData.Take(points).ToObservableCollection<IStockTradingData>();
                }
                else
                { 

                    var moreData = Task.Run(async () => await StockTradeDataRepository.GetTradingData(symbol, startTime, points, interval)).Result.ToObservableCollection<IStockTradingData>();
                    
                    foreach(var trade in moreData)
                    {
                        if(!cachedData.Any(x => x.RecordedTime == trade.RecordedTime))
                        {
                            cachedData.Add(trade);
                        }
                        
                    }
                    CacheService.Set(cacheKey, cachedData);
                    return moreData;
                }
            }
            
            var resultData = Task.Run(async () => await StockTradeDataRepository
            .GetTradingData(symbol,startTime, points, interval)).Result.ToObservableCollection<IStockTradingData>();
            
            CacheService.Set(cacheKey, resultData);
            return resultData;
        }
        public void Refresh(object sender, EventArgs e)
        {
            FetchTradeData();
            DataUpdated?.Invoke();
        }
        public void StartSession()
        {
            updateTimer.Start();
        }

        #region Private Methods
        void FetchTradeData()
        {
            TradeTimeInterval interval = TradeTimeInterval.Minute;
            var symbols = StockDataService.GetStockSymbols();

            foreach (var symbol in symbols)
            {
                var data = StockTradeDataRepository.GetTradeData(symbol, 1, interval).GetAwaiter().GetResult();
                string cacheKey = $"{symbol}_{interval}";
                if (CacheService.ContainsKey(cacheKey))
                {
                    CacheService.Get<ObservableCollection<IStockTradingData>>(cacheKey).Add(data);
                }
                else
                {
                    CacheService.Set(cacheKey, new ObservableCollection<IStockTradingData> { data });
                }
            }
        }
        void InitTimer()
        {
            updateTimer.Interval = TimeSpan.FromMilliseconds(Tick);
            updateTimer.Tick += new EventHandler(Refresh);
        } 
        #endregion
    }
}
