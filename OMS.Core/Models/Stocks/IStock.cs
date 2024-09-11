using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.Core.Models.Stocks
{
    public interface IStock
    {
        int ID { get; set; }
        string Symbol { get; set; }
        string Name { get; set; }
        decimal LastPrice { get; set; }
    }

}
