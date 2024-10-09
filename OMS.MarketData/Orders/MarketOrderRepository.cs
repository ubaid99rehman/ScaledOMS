using OMS.Core.Core.Models.Books;
using OMS.Core.Models;
using OMS.Core.Models.Stocks;
using OMS.DataAccess.Repositories.MarketRepositories;
using OMS.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OMS.MarketData
{
    public class MarketOrderRepository : IMarketOrderRepository
    {
        IStockRepository StockRepository;
        Random random;

        int sellOrdersCount;
        int buyOrdersCount;

        //Constructor
        public MarketOrderRepository(IStockRepository stockRepository)
        {
            StockRepository = stockRepository;
            random = new Random();
            sellOrdersCount = random.Next(1, 5);
            buyOrdersCount = random.Next(1, 5);        }

        public IEnumerable<BookBase> GetOrdersBySymbol(string symbol)
        {
            return AddTrades(symbol);
        }
        private IEnumerable<BookBase> AddTrades(string symbol)
        {
            IStock stock = StockRepository.GetBySymbol(symbol);
            if (stock == null)
            {
                return new List<BookBase>();
            }
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
