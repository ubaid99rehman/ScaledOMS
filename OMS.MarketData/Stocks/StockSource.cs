using OMS.Core.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.MarketData.Stocks
{
    public static class StockSource
    {
        public static ObservableCollection<Stock> _stocks  = new ObservableCollection<Stock>
        {
            new Stock { ID = 1, Symbol = "AAPL", Name = "Apple Inc.", LastPrice = 150.23M },
            new Stock { ID = 2, Symbol = "MSFT", Name = "Microsoft Corp.", LastPrice = 299.99M },
            new Stock { ID = 3, Symbol = "GOOGL", Name = "Alphabet Inc.", LastPrice = 2840.12M },
            new Stock { ID = 4, Symbol = "AMZN", Name = "Amazon.com Inc.", LastPrice = 3335.55M },
            new Stock { ID = 5, Symbol = "TSLA", Name = "Tesla Inc.", LastPrice = 675.30M },
            new Stock { ID = 6, Symbol = "FB", Name = "Meta Platforms Inc.", LastPrice = 350.10M },
            new Stock { ID = 7, Symbol = "NFLX", Name = "Netflix Inc.", LastPrice = 515.00M },
            new Stock { ID = 8, Symbol = "NVDA", Name = "NVIDIA Corporation", LastPrice = 210.50M },
            new Stock { ID = 9, Symbol = "INTC", Name = "Intel Corporation", LastPrice = 54.60M },
            new Stock { ID = 10, Symbol = "AMD", Name = "Advanced Micro Devices, Inc.", LastPrice = 85.30M },
            new Stock { ID = 11, Symbol = "BABA", Name = "Alibaba Group Holding Limited", LastPrice = 210.00M },
            new Stock { ID = 12, Symbol = "BIDU", Name = "Baidu, Inc.", LastPrice = 180.75M },
            new Stock { ID = 13, Symbol = "UBER", Name = "Uber Technologies, Inc.", LastPrice = 45.10M },
            new Stock { ID = 14, Symbol = "LYFT", Name = "Lyft, Inc.", LastPrice = 20.00M },
            new Stock { ID = 15, Symbol = "TWTR", Name = "Twitter, Inc.", LastPrice = 55.20M },
            new Stock { ID = 16, Symbol = "SNAP", Name = "Snap Inc.", LastPrice = 12.30M },
            new Stock { ID = 17, Symbol = "SHOP", Name = "Shopify Inc.", LastPrice = 1400.20M },
            new Stock { ID = 18, Symbol = "SQ", Name = "Square, Inc.", LastPrice = 250.50M },
            new Stock { ID = 19, Symbol = "PYPL", Name = "PayPal Holdings, Inc.", LastPrice = 275.45M },
            new Stock { ID = 20, Symbol = "SPOT", Name = "Spotify Technology S.A.", LastPrice = 240.70M }
                
        };
    }
}
