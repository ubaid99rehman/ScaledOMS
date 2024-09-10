using OMS.Core.Models.Trade;
using OMS.Core.Services.AppServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.Core.Services
{
    internal interface ITradeService : IMarketService<Trade>
    {
    }
}
