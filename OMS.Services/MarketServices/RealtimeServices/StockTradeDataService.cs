using DevExpress.Mvvm.Native;
using OMS.Common.Helper;
using OMS.Core.Models.Stocks;
using OMS.Core.Services;
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

        #region Services
        IStockRepository StockRepository;
        IStockTradeDataRepository StockTradeDataRepository;
        IStockDataService StockDataService;
        ICacheService CacheService;
        #endregion

        public StockTradeDataService(IStockRepository stockRepository,
            IStockTradeDataRepository stockTradeDataRepository, 
            IStockDataService stockDataService, ITimerService timerService,
            ICacheService cacheService)
        {
            StockRepository = stockRepository;
            StockTradeDataRepository = stockTradeDataRepository;
            StockDataService = stockDataService;
            CacheService = cacheService;
            timerService.MinuteTick += OnTimerTick;
            
            
        }

        //Public Access Methods Implementation
        public IStockTradingData GetBySymbol(string symbol)
        {
            TradeTimeInterval interval = TradeTimeInterval.Minute;
            string cacheKey = $"{symbol}_{interval}";
            if(CacheService.ContainsKey(cacheKey))
            {
                var data = CacheService.Get<ObservableCollection<IStockTradingData>>(cacheKey);
                return data.LastOrDefault();
            }

            return StockTradeDataRepository.GetLastTradeData(symbol).GetAwaiter().GetResult();
        }
        public ObservableCollection<IStockTradingData> GetTradingData(string symbol, DateTime startTime, 
            int points=180, TradeTimeInterval interval= TradeTimeInterval.Minute)
        {
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
        
        private void OnTimerTick(object sender, EventArgs e)
        {
            FetchTradeData();
            DataUpdated?.Invoke();
        }
        private void FetchTradeData()
        {
            TradeTimeInterval interval = TradeTimeInterval.Minute;
            var symbols = StockDataService.GetStockSymbols();
            foreach (var symbol in symbols)
            {
                var data = StockTradeDataRepository.GetLastTradeData(symbol).GetAwaiter().GetResult();
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
    }
}
