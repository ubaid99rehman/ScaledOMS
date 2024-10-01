using OMS.Core.Core.Models.Books;
using OMS.Core.Models;
using OMS.Core.Models.Stocks;
using OMS.DataAccess.Repositories.MarketRepositories;
using OMS.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OMS.MarketData.Stocks
{
    public class MarketTradeRepository : IMarketTradeRepository
    {
        Random random;
        List<string> StockSymbols;
        List<IStock> Stocks;
        IStockRepository StockRepository;
        //Constructor
        public MarketTradeRepository(IStockRepository stockRepository)
        {
            StockRepository = stockRepository;
            random = new Random();
            Stocks = new List<IStock>();
            StockSymbols = new List<string>();
        }
        //Public Data Access Methods
        public IEnumerable<BookBase> GetAll(string symbol)
        {
            FetchStockData();
            return AddTrades(Stocks.Where(s=> s.Symbol==symbol).FirstOrDefault());
        }
        public IEnumerable<BookBase> GetAll()
        {
            FetchStockData();
            var tradesDictionary = new  List<BookBase>();
            foreach (var symbol in StockSymbols)
            {
               tradesDictionary.AddRange(AddTrades(Stocks.Where(s => s.Symbol == symbol).FirstOrDefault()));
            }
            return tradesDictionary;
        }
        public BookBase GetById(int id)
        {
            FetchStockData();
            IStock stock = Stocks.Where(s => s.ID == id).FirstOrDefault();
            return AddTrades(stock).FirstOrDefault();
            
        }
        //Private Data Loading Methods
        void FetchStockData()
        {
            if(StockSymbols == null || StockSymbols.Count < 1)
            {
                StockSymbols = StockRepository.GetStockSymbols().ToList<string>();
            }
            if(Stocks == null || Stocks.Count < 1)
            {
                Stocks = StockRepository.GetAll().ToList<IStock>();
            }
            foreach (var symbol in StockSymbols) 
            {
                AddTrades(Stocks.Where(s => s.Symbol == symbol).FirstOrDefault());
            }
        }
        private IEnumerable<BookBase> AddTrades(IStock stock)
        {
            int tradeCount = random.Next(1, 11);
            List<BookBase> trades = new List<BookBase>();
            for (int i = 0; i < tradeCount; i++)
            {
                int qty = random.Next(1, 1000);
                TradeBook trade = new TradeBook
                {
                    Symbol = stock.Symbol,
                    Price = stock.LastPrice,
                    Quantity = qty,
                    Total = qty*stock.LastPrice, 
                    Timestamp = DateTime.Now,
                    Type = (OrderType)random.Next(1, 3)
                };
                trade.Total = trade.Price * trade.Quantity;
                trades.Add(trade);
            }

            return trades;
        }
    }
}
