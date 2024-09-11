using OMS.Core.Models;
using OMS.Core.Models.Stocks;
using OMS.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OMS.MarketData.Stocks
{
    public class StockDetailRepository : IStockDetailRepository
    {
        #region Constants
        const int volumeMinLow = 30000000;
        const int volumeMaxLow = 60000000;
        const int volumeMinHigh = 1000000000;
        const int volumeMaxHigh = 2000000000; 
        #endregion
        
        Random _random;

        public StockDetailRepository() 
        {
            _random = new Random();
        }

        public IEnumerable<StockDetail> GetAll()
        {
            List<StockDetail> stockDetails = new List<StockDetail>();
            foreach(Stock stock in StockSource._stocks)
            {
                stockDetails.Add(GenerateDetails(stock));
            }
            return stockDetails;
        }

        public StockDetail GetById(int id)
        {
            var stock = StockSource._stocks.Where(s => s.ID == id).FirstOrDefault();
            if(stock != null)
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
            stockDetail.Volume24H = (decimal)(_random.NextDouble() * (double)(volumeMaxHigh - volumeMinLow) + (double)volumeMinLow);
            stockDetail.Change24H = Math.Round((decimal)(_random.NextDouble() * 4 - 2), 2); // Random change between -2% and +2%
            stockDetail.High24H = price * (1 + Math.Abs((decimal)_random.NextDouble() * 0.02m)); // Up to +2% of LastPrice
            stockDetail.Low24H = price * (1 - Math.Abs((decimal)_random.NextDouble() * 0.02m));  // Down to -2% of LastPrice

            return stockDetail;
        }

        public StockDetail GetBySymbol(string symbol)
        {
            var stock = StockSource._stocks.Where(s => s.Symbol == symbol).FirstOrDefault();
            if (stock != null)
            {
                return GenerateDetails(stock);
            }
            return null;
        }
    }
}
