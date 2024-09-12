using OMS.Core.Models.Stocks;
using OMS.Core.Services.MarketServices.RealtimeServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.MarketData.Stocks
{
    public class StockTradeDataService : IStockMarketService
    {
        public ObservableCollection<StockTradingData> GetAll()
        {
            throw new NotImplementedException();
        }

        public StockTradingData GetById(int key)
        {
            throw new NotImplementedException();
        }

        public StockTradingData GetBySymbol(string symbol)
        {
            throw new NotImplementedException();
        }

        public void Refresh(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        public void StartSession()
        {
            throw new NotImplementedException();
        }
    }
}
