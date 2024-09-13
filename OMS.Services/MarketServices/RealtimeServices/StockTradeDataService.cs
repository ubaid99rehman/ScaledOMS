using DevExpress.Mvvm.Native;
using OMS.Common.Enums;
using OMS.Core.Models.Stocks;
using OMS.Core.Services.Cache;
using OMS.Core.Services.MarketServices.RealtimeServices;
using OMS.DataAccess.Repositories.MarketRepositories;
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

        public ObservableCollection<StockTradingData> GetAll()
        {
            throw new NotImplementedException();
        }

        public StockTradingData GetById(int key)
        {
            throw new NotImplementedException();
        }

        public StockTradingData GetBySymbol(string symbol)
        {
            TradeTimeInterval interval = TradeTimeInterval.Minute;
            string cacheKey = $"{symbol}_{interval}";
            if(CacheService.ContainsKey(cacheKey))
            {
                var data = CacheService.Get<ObservableCollection<StockTradingData>>(cacheKey);
                return data.LastOrDefault();
            }

            return StockTradeDataRepository.GetTradeData(symbol, 1, interval).GetAwaiter().GetResult();
        }

        public ObservableCollection<StockTradingData> GetTradingData(string symbol, int time=180, TradeTimeInterval interval= TradeTimeInterval.Minute)
        {
            if(!FetchData)
            {
                StartSession();
                FetchData = true;
            }
            string cacheKey = $"{symbol}_{interval}";
            if (CacheService.ContainsKey(cacheKey))
            {
                var cData = CacheService.Get<ObservableCollection<StockTradingData>>(cacheKey);
                if(cData.Count < time)
                {
                    var moreData = Task.Run(async () => await StockTradeDataRepository.GetTradingData(symbol, time, interval)).Result;

                    ObservableCollection<StockTradingData> newData = moreData.ToObservableCollection<StockTradingData>();
                    CacheService.Set(cacheKey, newData);
                }
                else
                {
                    return CacheService.Get<ObservableCollection<StockTradingData>>(cacheKey).
                        Take(time).ToObservableCollection<StockTradingData>();
                }
            }
            
            var tradeData = Task.Run(async () => await StockTradeDataRepository.GetTradingData(symbol,time, interval)).Result;

            ObservableCollection<StockTradingData> data = tradeData.ToObservableCollection<StockTradingData>();
            CacheService.Set(cacheKey, data);
            return data;
        }

        public void Refresh(object sender, EventArgs e)
        {
            FetchTradeData();
            DataUpdated?.Invoke();
        }

        void FetchTradeData()
        {
            TradeTimeInterval interval = TradeTimeInterval.Minute;
            var symbols = StockDataService.GetStockSymbols();

            foreach (var symbol in symbols) 
            {
                var data = StockTradeDataRepository.GetTradeData(symbol, 1,interval ).GetAwaiter().GetResult();
                string cacheKey = $"{symbol}_{interval}";
                if (CacheService.ContainsKey(cacheKey))
                {
                    CacheService.Get<ObservableCollection<StockTradingData>>(cacheKey).Add(data);
                }
                else
                {
                    CacheService.Set(cacheKey,new ObservableCollection<StockTradingData> { data});
                }
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

    }
}
