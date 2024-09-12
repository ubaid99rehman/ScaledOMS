using OMS.Core.Enums;
using OMS.Core.Models;
using OMS.Core.Services.MarketServices;
using OMS.DataAccess.Repositories;
using OMS.DataAccess.Repositories.MarketRepositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace OMS.MarketData.Stocks
{
    public class MarketOrderRepository : IMarketOrderRepository
    {
        Random random;

        List<string> StockSymbols;
        List<Stock> Stocks;
        IStockRepository StockRepository;
        Dictionary<string, List<OrderBook>> tradesDictionary;

        public MarketOrderRepository(IStockRepository stockRepository)
        {
            StockRepository = stockRepository;
            random = new Random();

            tradesDictionary = new Dictionary<string, List<OrderBook>>();
            Stocks = new List<Stock>();
            StockSymbols = new List<string>();
            FetchStockData();
        }

        public IEnumerable<OrderBook> GetAll(string symbol)
        {
            if (!tradesDictionary.ContainsKey(symbol))
            {
                AddOrders(Stocks.Where(s => s.Symbol == symbol).FirstOrDefault());
            }
            return tradesDictionary[symbol];
        }

        public IEnumerable<OrderBook> GetAll()
        {
            foreach (var symbol in StockSymbols)
            {
                if (!tradesDictionary.ContainsKey(symbol))
                {
                    AddOrders(Stocks.Where(s => s.Symbol == symbol).FirstOrDefault());
                }
            }
            return tradesDictionary.Values.SelectMany(t => t);
        }

        public OrderBook GetById(int id)
        {
            Stock stock = Stocks.Where(s => s.ID == id).FirstOrDefault();
            if (!tradesDictionary.ContainsKey(stock.Symbol))
            {
                AddOrders(stock);
            }
            return tradesDictionary[stock.Symbol].FirstOrDefault();
        }

        void FetchStockData()
        {
            if (StockSymbols == null || StockSymbols.Count < 1)
            {
                StockSymbols = StockRepository.GetStockSymbols().ToList<string>();
            }
            if (Stocks == null || Stocks.Count < 1)
            {
                Stocks = StockRepository.GetAll().ToList<Stock>();
            }
            foreach (var symbol in StockSymbols)
            {
                AddOrders(Stocks.Where(s => s.Symbol == symbol).FirstOrDefault());
            }
        }

        private void AddOrders(Stock stock)
        {
            Random random = new Random();
            int tradeCount = random.Next(1, 11);

            List<OrderBook> trades = new List<OrderBook>();

            for (int i = 0; i < tradeCount; i++)
            {
                int qty = random.Next(1, 1000);
                OrderBook trade = new OrderBook
                {
                    Symbol = stock.Symbol,
                    Price = stock.LastPrice,
                    Quantity = qty,
                    Total = qty * stock.LastPrice,
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
