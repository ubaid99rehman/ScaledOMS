using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace OMS.Core.Models.Stocks
{
    public interface IStockTradingData
    {
        decimal Volume { get; set; }
        decimal High {get; set;}
        decimal Low {get; set;}
        decimal Open {get; set;}
        decimal Close {get; set;}
        DateTime RecordedTime {get; set;}
    }
}
