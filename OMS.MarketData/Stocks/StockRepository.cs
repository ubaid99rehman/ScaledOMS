using OMS.Core.Models;
using OMS.Core.Models.Stocks;
using OMS.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace OMS.MarketData.Stocks
{
    public class StockRepository : IStockRepository
    {
        public IEnumerable<Stock> GetAll()
        {
            return StockSource._stocks;
        }

        public Stock GetById(int id)
        {
            return StockSource._stocks.Where(s => s.ID == id).FirstOrDefault();    
        }

        public Stock GetBySymbol(string symbol)
        {
            return StockSource._stocks.Where(s => s.Symbol == symbol).FirstOrDefault();
        }

        public IEnumerable<string> GetStockSymbols()
        {
            List<string> symbols = new List<string>();
            foreach (var stock in StockSource._stocks)
            {
                symbols.Add(stock.Symbol);
            }
            return symbols;
        }
    }
}
