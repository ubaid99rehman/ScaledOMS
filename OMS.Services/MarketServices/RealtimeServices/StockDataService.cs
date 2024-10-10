using OMS.Core.Models;
using System;
using System.Collections.ObjectModel;
using DevExpress.Mvvm.Native;
using System.Linq;
using OMS.Core.Services.Cache;
using OMS.Core.Services.MarketServices.RealtimeServices;
using OMS.DataAccess.Repositories.MarketRepositories;
using OMS.Core.Models.Stocks;
using OMS.Core.Services;

namespace OMS.Services.AppServices
{
    public class StockDataService : IStockDataService
    {

        #region Constant Volume Limits
        const int volumeMinLow = 30000000;
        const int volumeMaxLow = 60000000;
        const int volumeMinHigh = 1000000000;
        const int volumeMaxHigh = 2000000000; 
        #endregion

        #region Services
        IStockRepository StockRepository;
        IStockDetailRepository StockDetailRepository;
        ICacheService CacheService; 
        #endregion

        public event Action DataUpdated;
        bool FetchData;

        //Cnstructor
        public StockDataService(ICacheService cacheService, ITimerService timerService,
            IStockRepository stockRepository, IStockDetailRepository stockDetailRepository)
        {
            FetchData = false;
            CacheService = cacheService;
            StockDetailRepository = stockDetailRepository;
            StockRepository = stockRepository;

            timerService.SecondTick += OnTimerTick;
            timerService.Start();
        }

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

        private void OnTimerTick(object sender, EventArgs e)
        {
            UpdateStockDetails();
            DataUpdated?.Invoke();
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
                FetchData = true;
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
                FetchData = true;
            }
        } 
    }
}
