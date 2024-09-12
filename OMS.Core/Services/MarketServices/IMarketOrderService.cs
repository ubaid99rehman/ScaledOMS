using OMS.Core.Models;
using System.Collections.ObjectModel;


namespace OMS.Core.Services.MarketServices
{
    public interface IMarketOrderService :  IMarketService<OrderBook>
    {
        ObservableCollection<OrderBook> GetAllBySymbol(string symbol);
    }
}
