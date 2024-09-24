using OMS.Core.Models;
using OMS.DataAccess.Repositories.MarketRepositories;
using OMS.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OMS.MarketData.Stocks
{
    public class MarketOrderRepository : IMarketOrderRepository
    {
        Random random;

        List<string> StockSymbols;
        List<Stock> Stocks;
        IStockRepository StockRepository;

        public MarketOrderRepository(IStockRepository stockRepository)
        {
            StockRepository = stockRepository;
            StockSymbols = new List<string>();
            Stocks = new List<Stock>();
            random = new Random();
            FetchStockData();
        }

        public IEnumerable<OrderBook> GetAll(string symbol)
        {
            return AddOrders(Stocks.Where(s => s.Symbol == symbol).FirstOrDefault());
        }

        public IEnumerable<OrderBook> GetAll()
        {
            Dictionary<string, IEnumerable<OrderBook>> tradesDictionary = new Dictionary<string, IEnumerable<OrderBook>>();
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
            return AddOrders(stock).FirstOrDefault();
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
            //foreach (var symbol in StockSymbols)
            //{
            //    AddOrders(Stocks.Where(s => s.Symbol == symbol).FirstOrDefault());
            //}
        }

        private IEnumerable<OrderBook> AddOrders(Stock stock)
        {
            int sellOrdersCount = random.Next(1, 5);
            int buyOrdersCount = random.Next(1, 5);
            List<OrderBook> orders = new List<OrderBook>();

            for (int i = 0; i < sellOrdersCount; i++)
            {
                int qty = random.Next(1, 1000);
                OrderBook trade = new OrderBook
                {
                    Symbol = stock.Symbol,
                    Price = stock.LastPrice,
                    Quantity = qty,
                    Total = qty * stock.LastPrice,
                    Timestamp = DateTime.Now,
                    Type = OrderType.Sell
                };
                trade.Total = trade.Price * trade.Quantity;
                orders.Add(trade);
            }

            for (int i = 0; i < buyOrdersCount; i++)
            {
                int qty = random.Next(1, 1000);
                OrderBook trade = new OrderBook
                {
                    Symbol = stock.Symbol,
                    Price = stock.LastPrice,
                    Quantity = qty,
                    Total = qty * stock.LastPrice,
                    Timestamp = DateTime.Now,
                    Type = OrderType.Buy
                };
                trade.Total = trade.Price * trade.Quantity;
                orders.Add(trade);
            }

            return  orders;
        }
    }
}
