using DevExpress.Xpf.Core;
using OMS.Core.Enums;
using OMS.Core.Models;
using OMS.Core.Models.Stocks;
using System.Collections.ObjectModel;

namespace OMS.Core.Services.AppServices
{
    public interface IStockDataService : IRealtimeService
    {
        Stock GetStock(string symbol);
        ObservableCollection<Stock> GetStocks();
        
        StockDetail GetStockDetail(string symbol);
        ObservableCollection<StockDetail> GetStockDetails();

        ObservableCollection<string> GetStockSymbols();

        ObservableCollection<StockTradingData> GetStockTradingData(string symbol);
        ObservableCollectionCore<StockTradingData> GetTradingData(string symbol, MeasureUnit unit = MeasureUnit.Month, int period = 1);

        void LazyLoadStocksTradingData();
        void LazyLoadStocksDetailData();

        void LoadStocks();
        void LoadStockSymbols();
        void LoadInitialStockDetailData();
        void LoadInitialStocksTradingData();
    }

}
