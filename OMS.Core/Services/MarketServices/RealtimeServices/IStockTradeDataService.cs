using OMS.Common.Enums;
using OMS.Core.Models.Stocks;
using System;
using System.Collections.ObjectModel;


namespace OMS.Core.Services.MarketServices.RealtimeServices
{
    public interface IStockTradeDataService : IMarketService<StockTradingData>, IRealtimeService
    {
        event Action DataUpdated;

        StockTradingData GetBySymbol(string symbol);

        ObservableCollection<StockTradingData> GetTradingData(string symbol, DateTime startTime, int time=180, TradeTimeInterval interval = TradeTimeInterval.Minute);
    }
}
