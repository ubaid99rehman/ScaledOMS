using OMS.Core.Models;
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
        List<Stock> Stocks;
        IStockRepository StockRepository;
        Dictionary<string, List<TradeBook>> tradesDictionary;

        public MarketTradeRepository(IStockRepository stockRepository)
        {
            StockRepository = stockRepository;
            random = new Random();

            tradesDictionary = new Dictionary<string, List<TradeBook>>();
            Stocks = new List<Stock>();
            StockSymbols = new List<string>();
        }

        public IEnumerable<TradeBook> GetAll(string symbol)
        {
            FetchStockData();
            if (!tradesDictionary.ContainsKey(symbol))
            {
                AddTrades(Stocks.Where(s=> s.Symbol==symbol).FirstOrDefault());
            }
            return tradesDictionary[symbol];
        }

        public IEnumerable<TradeBook> GetAll()
        {
            FetchStockData();
            foreach (var symbol in StockSymbols)
            {
                if (!tradesDictionary.ContainsKey(symbol))
                {
                    AddTrades(Stocks.Where(s => s.Symbol == symbol).FirstOrDefault());
                }
            }
            return tradesDictionary.Values.SelectMany(t => t);
        }

        public TradeBook GetById(int id)
        {
            FetchStockData();
            Stock stock = Stocks.Where(s => s.ID == id).FirstOrDefault();
            if (!tradesDictionary.ContainsKey(stock.Symbol))
            {
                AddTrades(stock);
            }
            return tradesDictionary[stock.Symbol].FirstOrDefault();
        }

        void FetchStockData()
        {
            if(StockSymbols == null || StockSymbols.Count < 1)
            {
                StockSymbols = StockRepository.GetStockSymbols().ToList<string>();
            }
            if(Stocks == null || Stocks.Count < 1)
            {
                Stocks = StockRepository.GetAll().ToList<Stock>();
            }
            foreach (var symbol in StockSymbols) 
            {
                AddTrades(Stocks.Where(s => s.Symbol == symbol).FirstOrDefault());
            }
        }

        private void AddTrades(Stock stock)
        {
            Random random = new Random();
            int tradeCount = random.Next(1, 11);

            List<TradeBook> trades = new List<TradeBook>();

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

            tradesDictionary[stock.Symbol] = trades;
        }
    }
}
