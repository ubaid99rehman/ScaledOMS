using OMS.Core.Models;
using OMS.DataAccess.Repositories.MarketRepositories;
using OMS.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OMS.MarketData.Stocks
{
    public class StockDetailRepository : IStockDetailRepository
    {
        #region Constants
        const long volumeLow =  1000000000;
        const long volumeHigh = 1000000000000;
        #endregion

        private readonly string _connectionString;
        IStockRepository StockRepository;

        IEnumerable<Stock> _stocks;
        Random _random;

        public StockDetailRepository(IStockRepository stockRepository) 
        {
            _random = new Random();
            _connectionString = DbHelper.Connection;
            StockRepository = stockRepository;
        }

        public IEnumerable<StockDetail> GetAll()
        {
            FetchStocks();
            List<StockDetail> stockDetails = new List<StockDetail>();
            foreach(Stock stock in _stocks)
            {
                stockDetails.Add(GenerateDetails(stock));
            }
            return stockDetails;
        }

        public StockDetail GetById(int id)
        {
            FetchStocks();
            var stock = _stocks.Where(s => s.ID == id).FirstOrDefault();

            if(stock != null)
            {
                return GenerateDetails(stock);
            }
            return null;
        }

        public StockDetail GetBySymbol(string symbol)
        {
            FetchStocks();
            var stock = _stocks.Where(s => s.Symbol == symbol).FirstOrDefault();
            if (stock != null)
            {
                return GenerateDetails(stock);
            }
            return null;
        }

        private StockDetail GenerateDetails(Stock stock)
        {
            var stockDetail = new StockDetail();
            var price = stock.LastPrice;
            stockDetail.ID = stock.ID;  
            stockDetail.Name = stock.Name;
            stockDetail.Symbol = stock.Symbol;
            stockDetail.LastPrice = price * (1 - Math.Abs((decimal)_random.NextDouble() * 0.02m)); ;
            stockDetail.Volume24H = (decimal)(_random.NextDouble() * (double)(volumeHigh - volumeLow) + (double)volumeLow);
            stockDetail.Change24H = Math.Round((decimal)(_random.NextDouble() * 4 - 2), 2); // Random change between -2% and +2%
            stockDetail.High24H = price * (1 + Math.Abs((decimal)_random.NextDouble() * 0.02m)); // Up to +2% of LastPrice
            stockDetail.Low24H = price * (1 - Math.Abs((decimal)_random.NextDouble() * 0.02m));  // Down to -2% of LastPrice

            return stockDetail;
        }
        
        private void FetchStocks()
        {
            if (_stocks == null || _stocks.Count() < 1)
            {
                _stocks = StockRepository.GetAll();
            }
        }
    }
}
