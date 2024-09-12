using OMS.Core.Models;
using System;
using System.Collections.ObjectModel;


namespace OMS.Core.Services.MarketServices.RealtimeServices
{
    public interface IMarketTradeService :IMarketService<TradeBook>, IRealtimeService
    {
        event Action<string> DataUpdated;
        ObservableCollection<TradeBook> GetAllBySymbol(string symbol);
    }
}
