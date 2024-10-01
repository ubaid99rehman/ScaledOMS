using OMS.Core.Models;
using System;
using System.Collections.ObjectModel;
using DevExpress.Mvvm.Native;
using System.Windows.Threading;
using System.Linq;
using OMS.Core.Services.Cache;
using OMS.Core.Services.MarketServices.RealtimeServices;
using OMS.DataAccess.Repositories.MarketRepositories;
using OMS.Core.Models.Stocks;

namespace OMS.Services.AppServices
{
    public class StockDataService : IStockDataService
    {
        #region Constants
        const int Tick = 500;
        const int volumeMinLow = 30000000;
        const int volumeMaxLow = 60000000;
        const int volumeMinHigh = 1000000000;
        const int volumeMaxHigh = 2000000000;
        #endregion
        IStockRepository StockRepository;
        IStockDetailRepository StockDetailRepository;
        ICacheService CacheService;
        public event Action DataUpdated;
        readonly DispatcherTimer updateTimer;
        bool FetchData;

        //Cnstructor
        public StockDataService(ICacheService cacheService, IStockRepository stockRepository, IStockDetailRepository stockDetailRepository)
        {
            FetchData = false;
            CacheService = cacheService;
            StockDetailRepository = stockDetailRepository;
            StockRepository = stockRepository;
            updateTimer = new DispatcherTimer(DispatcherPriority.ApplicationIdle);
            InitTimer();
        }

        //Public Access Methods Implementation
        public IStock GetStock(string symbol)
        {
            var stocks = GetStocks();
            IStock stock = stocks.Where(s => s.Symbol == symbol).FirstOrDefault();
            return stock;
        }
        public IStockDetail GetStockDetail(string symbol)
        {
            var stocks = GetStockDetails();
            var stock = stocks.Where(s => s.Symbol == symbol).FirstOrDefault();
            return stock;
        }
        public ObservableCollection<IStock> GetStocks()
        {
            if (CacheService.ContainsKey("StocksList"))
            {
               return CacheService.Get<ObservableCollection<IStock>>("StocksList");
            }

            ObservableCollection<IStock> stocksList = StockRepository.GetAll().ToObservableCollection<IStock>();
            CacheService.Set("StocksList", stocksList);
            return stocksList;  
        }
        public ObservableCollection<string> GetStockSymbols()
        {
            if(CacheService.ContainsKey("StockSymbols"))
            {
                return CacheService.Get<ObservableCollection<string>>("StockSymbols");
            }

            ObservableCollection<string> stockSymbols = StockRepository.GetStockSymbols().ToObservableCollection<string>();
            CacheService.Set("StockSymbols", stockSymbols);

            return stockSymbols;
        }
        public ObservableCollection<IStockDetail> GetStockDetails()
        {
            if (CacheService.ContainsKey("StockDetails"))
            {
                return CacheService.Get<ObservableCollection<IStockDetail>>("StockDetails");
            }

            FetchStockDetails();
            return CacheService.Get<ObservableCollection<IStockDetail>>("StockDetails");

        }
        public void Refresh(object sender, EventArgs e)
        {
            UpdateStockDetails();
            DataUpdated?.Invoke();

        }
        public void StartSession()
        {
            updateTimer.Start();
        }

        #region Private Methods
        private void InitTimer()
        {
            updateTimer.Interval = TimeSpan.FromMilliseconds(Tick);
            updateTimer.Tick += new EventHandler(Refresh);
        }
        private void FetchStockDetails()
        {
            var details = StockDetailRepository.GetAll().ToObservableCollection<IStockDetail>();
            if (CacheService.ContainsKey("StockDetails"))
            {
                CacheService.Get<ObservableCollection<StockDetail>>("StockDetails");
            }
            else
            {
                CacheService.Set("StockDetails", details);
            }
            if (!FetchData)
            {
                StartSession();
            }
        }
        private void UpdateStockDetails()
        {
            var details = StockDetailRepository.GetAll().ToObservableCollection<IStockDetail>();
            if (CacheService.ContainsKey("StockDetails"))
            {
                CacheService.Set("StockDetails", details);
            }
            else
            {
                CacheService.Set("StockDetails", details);
            }

            if (!FetchData)
            {
                StartSession();
            }
        } 
        #endregion
    }
}
