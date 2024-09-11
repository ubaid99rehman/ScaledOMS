using OMS.Core.Models;
using OMS.Core.Services.AppServices;
using System;
using System.Collections.ObjectModel;
using DevExpress.Mvvm.Native;
using OMS.DataAccess.Repositories;
using System.Windows.Threading;
using OMS.Core.Services;
using System.Linq;

namespace OMS.Services.AppServices
{
    public class StockDataService : IStockDataService
    {
        IStockRepository StockRepository;
        IStockDetailRepository StockDetailRepository;
        ICacheService CacheService;
        readonly DispatcherTimer updateTimer;
        bool FetchData = false;

        #region Constants
        const int Tick = 500;
        const int volumeMinLow = 30000000;
        const int volumeMaxLow = 60000000;
        const int volumeMinHigh = 1000000000;
        const int volumeMaxHigh = 2000000000;
        #endregion
        
        public event Action DataUpdated;

        public StockDataService(ICacheService cacheService, IStockRepository stockRepository, 
            IStockDetailRepository stockDetailRepository)
        {
            CacheService = cacheService;
            StockDetailRepository = stockDetailRepository;
            StockRepository = stockRepository;
            updateTimer = new DispatcherTimer(DispatcherPriority.ApplicationIdle);
            InitTimer();
        }

        void InitTimer()
        {
            updateTimer.Interval = TimeSpan.FromMilliseconds(Tick);
            updateTimer.Tick += new EventHandler(Refresh);
        }

        public Stock GetStock(string symbol)
        {
            return GetStocks().Where(s => s.Symbol == symbol).FirstOrDefault();
        }

        public StockDetail GetStockDetail(string symbol)
        {
            var stocks = GetStockDetails();
            var stock = stocks.Where(s => s.Symbol == symbol).FirstOrDefault();
            return stock;
        }

        public ObservableCollection<Stock> GetStocks()
        {
            if (CacheService.ContainsKey("StocksList"))
            {
               return CacheService.Get<ObservableCollection<Stock>>("StocksList");
            }

            ObservableCollection<Stock> stocksList = StockRepository.GetAll().ToObservableCollection<Stock>();
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

        public ObservableCollection<StockDetail> GetStockDetails()
        {
            if (CacheService.ContainsKey("StockDetails"))
            {
                return CacheService.Get<ObservableCollection<StockDetail>>("StockDetails");
            }

            FetchStockDetails();
            return CacheService.Get<ObservableCollection<StockDetail>>("StockDetails");

        }

        private void FetchStockDetails()
        {
            var details = StockDetailRepository.GetAll().ToObservableCollection<StockDetail>();
            if (CacheService.ContainsKey("StockDetails"))
            {
                CacheService.Get<ObservableCollection<StockDetail>>("StockDetails");
            }
            else
            {
                CacheService.Set("StockDetails",details);
            }
            if(!FetchData)
            {
                StartSession();
            }
        }

        void UpdateStockDetails()
        {
            var details = StockDetailRepository.GetAll().ToObservableCollection<StockDetail>();
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

        public void Refresh(object sender, EventArgs e)
        {
            UpdateStockDetails();
            DataUpdated?.Invoke();

        }

        public void StartSession()
        {
            updateTimer.Start();
        }
    }
}
