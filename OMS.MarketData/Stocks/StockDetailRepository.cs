using OMS.Core.Models;
using OMS.Core.Models.Stocks;
using OMS.DataAccess.Repositories.MarketRepositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OMS.MarketData
{
    public class StockDetailRepository : IStockDetailRepository
    {
        #region Constant Volume Limits
        const long volumeLow =  1000000000;
        const long volumeHigh = 1000000000000;
        #endregion

        IStockRepository StockRepository;
        Random _random;
        
        //Constructor
        public StockDetailRepository(IStockRepository stockRepository) 
        {
            _random = new Random();
            StockRepository = stockRepository;
        }
        
        public IEnumerable<IStockDetail> GetAll()
        {
            List<IStockDetail> stockDetails = new List<IStockDetail>();
            foreach (IStock stock in FetchStocks())
            {
                stockDetails.Add(GenerateDetails(stock));
            }
            return stockDetails;
        }
        public IStockDetail GetBySymbol(string symbol)
        {
            var stock = FetchStocks().Where(s => s.Symbol == symbol).FirstOrDefault();
            if (stock != null)
            {
                return GenerateDetails(stock);
            }
            return null;
        } 
        private IStockDetail GenerateDetails(IStock stock)
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
        private IEnumerable<IStock> FetchStocks()
        {
            return StockRepository.GetAll();
        } 
    }
}
