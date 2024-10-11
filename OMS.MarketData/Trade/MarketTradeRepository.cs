using OMS.Core.Models;
using OMS.Core.Models.Books;
using OMS.Core.Models.Stocks;
using OMS.DataAccess.Repositories.MarketRepositories;
using OMS.Enums;
using System;
using System.Collections.Generic;

namespace OMS.MarketData
{
    public class MarketTradeRepository : IMarketTradeRepository
    {
        Random random;
        int tradeCount;
        IStockRepository StockRepository;

        //Constructor
        public MarketTradeRepository(IStockRepository stockRepository)
        {
            StockRepository = stockRepository;
            random = new Random();
            tradeCount= random.Next(1, 11);
        }

        public IEnumerable<IBookBase> GetTradesBySymbol(string symbol)
        {
            return AddTrades(symbol);
        }
        private IEnumerable<IBookBase> AddTrades(string symbol)
        {
            IStock stock = StockRepository.GetBySymbol(symbol);
            if (stock == null)
            {
                return new List<IBookBase>();
            }

            List<IBookBase> trades = new List<IBookBase>();
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
