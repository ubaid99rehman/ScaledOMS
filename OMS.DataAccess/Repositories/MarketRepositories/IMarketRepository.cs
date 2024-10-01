using OMS.Core.Models.Stocks;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using OMS.Enums;
using OMS.Core.Core.Models.Books;

namespace OMS.DataAccess.Repositories.MarketRepositories
{
    //Base Market Interface
    public interface IMarketRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
    }
    
    public interface IStockRepository : IMarketRepository<IStock>
    {
        IEnumerable<string> GetStockSymbols();
        IStock GetBySymbol(string symbol);
    }
    public interface IStockDetailRepository : IMarketRepository<IStockDetail>
    {
        IStockDetail GetBySymbol(string symbol);
    }
    public interface IStockTradeDataRepository : IMarketRepository<IStockTradingData>
    {
        Task<IEnumerable<IStockTradingData>> GetTradingData(string symbol, DateTime startTime, int points,TradeTimeInterval interval);
        Task<IStockTradingData> GetTradeData(string symbol, int time, TradeTimeInterval interval);
    }
    public interface IMarketOrderRepository : IMarketRepository<BookBase>
    {
        IEnumerable<BookBase> GetAll(string symbol);
    }
    public interface IMarketTradeRepository : IMarketRepository<BookBase>
    {
        IEnumerable<BookBase> GetAll(string symbol);
    }
}
