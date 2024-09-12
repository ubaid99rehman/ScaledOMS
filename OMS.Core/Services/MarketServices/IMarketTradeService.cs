using OMS.Core.Models;
using System.Collections.ObjectModel;


namespace OMS.Core.Services.MarketServices
{
    public interface IMarketTradeService :IMarketService<TradeBook>, IRealtimeService
    {
        ObservableCollection<TradeBook> GetAllBySymbol(string symbol);
    }
}
