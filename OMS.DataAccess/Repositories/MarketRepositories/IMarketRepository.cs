using OMS.Core.Models.Stocks;
using OMS.Core.Models;
using System.Collections.Generic;
using OMS.Common.Enums;
using System.Threading.Tasks;
using System;

namespace OMS.DataAccess.Repositories.MarketRepositories
{
    //Market Repo's
    public interface IMarketRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
    }

    public interface IStockRepository : IMarketRepository<Stock>
    {
        IEnumerable<string> GetStockSymbols();
        Stock GetBySymbol(string symbol);
    }

    public interface IStockDetailRepository : IMarketRepository<StockDetail>
    {
        StockDetail GetBySymbol(string symbol);
    }

    public interface IStockTradeDataRepository : IMarketRepository<StockTradingData>
    {
        Task<IEnumerable<StockTradingData>> GetTradingData(string symbol, int time, TradeTimeInterval interval );
        Task<StockTradingData> GetTradeData(string symbol, int time, TradeTimeInterval interval);
    }

    public interface IMarketOrderRepository : IMarketRepository<OrderBook>
    {
        IEnumerable<OrderBook> GetAll(string symbol);
    }

    public interface IMarketTradeRepository : IMarketRepository<TradeBook>
    {
        IEnumerable<TradeBook> GetAll(string symbol);
    }

}
