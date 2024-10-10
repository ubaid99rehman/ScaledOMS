using OMS.Core.Models.Stocks;
using OMS.Enums;
using System;
using System.Collections.ObjectModel;

namespace OMS.Core.Services.MarketServices.RealtimeServices
{
    public interface IStockTradeDataService
    {
        event Action DataUpdated;
        IStockTradingData GetBySymbol(string symbol);
        ObservableCollection<IStockTradingData> GetTradingData(string symbol, DateTime startTime, int time=180, TradeTimeInterval interval = TradeTimeInterval.Minute);
    }
}
