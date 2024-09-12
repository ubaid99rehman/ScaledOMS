using OMS.Common.Enums;
using OMS.Core.Models.Stocks;
using System.Collections.ObjectModel;


namespace OMS.Core.Services.MarketServices.RealtimeServices
{
    public interface IStockTradeDataService : IMarketService<StockTradingData>, IRealtimeService
    {
        StockTradingData GetBySymbol(string symbol);

        ObservableCollection<StockTradingData> GetTradingData(string symbol, TimePeriod period, int time, TimeInterval interval);
    }
}
