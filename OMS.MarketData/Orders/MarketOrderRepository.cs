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
    public class MarketOrderRepository : IMarketOrderRepository
    {
        Random random;
        List<string> StockSymbols;
        List<IStock> Stocks;
        IStockRepository StockRepository;
        //Constructor
        public MarketOrderRepository(IStockRepository stockRepository)
        {
            StockRepository = stockRepository;
            StockSymbols = new List<string>();
            Stocks = new List<IStock>();
            random = new Random();
            FetchStockData();
        }
        //Public Data Access Methods
        public IEnumerable<BookBase> GetAll(string symbol)
        {
            return AddOrders(Stocks.Where(s => s.Symbol == symbol).FirstOrDefault());
        }
        public IEnumerable<BookBase> GetAll()
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
        public BookBase GetById(int id)
        {
            IStock stock = Stocks.Where(s => s.ID == id).FirstOrDefault();
            return AddOrders(stock).FirstOrDefault();
        }
        //Private Data Loading Methods
        void FetchStockData()
        {
            if (StockSymbols == null || StockSymbols.Count < 1)
            {
                StockSymbols = StockRepository.GetStockSymbols().ToList<string>();
            }
            if (Stocks == null || Stocks.Count < 1)
            {
                Stocks = StockRepository.GetAll().ToList<IStock>();
            }
        }
        private IEnumerable<BookBase> AddOrders(IStock stock)
        {
            int sellOrdersCount = random.Next(1, 5);
            int buyOrdersCount = random.Next(1, 5);
            List<BookBase> orders = new List<BookBase>();

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

            return orders;
        }
    }
}
