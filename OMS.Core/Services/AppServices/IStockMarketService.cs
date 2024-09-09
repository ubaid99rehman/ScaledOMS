using OMS.Core.Models.Stocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.Core.Services.AppServices
{
    internal interface IStockMarketService : IMarketService<StockTradingData>
    {
    }
}
