using OMS.Core.Models.Stocks;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using OMS.Enums;
using OMS.Core.Models.Books;

namespace OMS.DataAccess.Repositories.MarketRepositories
{
    //Base Market Interface
    public interface IMarketRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetBySymbol(string symbol);
    }
    
    //Stocks
    public interface IStockRepository : IMarketRepository<IStock>
    {
        IEnumerable<string> GetStockSymbols();
    }
    public interface IStockDetailRepository : IMarketRepository<IStockDetail>
    {
    }

    //Trading Data
    public interface IMarketOrderRepository
    {
        IEnumerable<IBookBase> GetOrdersBySymbol(string symbol);
    }
    public interface IMarketTradeRepository 
    {
        IEnumerable<IBookBase> GetTradesBySymbol(string symbol);
    }
    public interface IStockTradeDataRepository
    {
        Task<IEnumerable<IStockTradingData>> GetTradingData(string symbol, DateTime startTime, 
            int points,TradeTimeInterval interval);
        Task<IStockTradingData> GetLastTradeData(string symbol);
    }
}
