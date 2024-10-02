
using DevExpress.Mvvm;
using System.Collections.ObjectModel;

namespace OMS.ViewModels
{

    public class DashboardViewModel : ViewModelBase
    {
        //Demo Data 
        public ObservableCollection<MarketOverview> MarketOverviewData { get; set; }
        public ObservableCollection<StockHolding> StockHoldingsData { get; set; }
        public ObservableCollection<MonthlyOrderData> MonthlyOrderData { get; set; }
        public ObservableCollection<PortfolioOverview> PortfolioOverviewData { get; set; }
        public ObservableCollection<OrderManagement> OrderManagementData { get; set; }

        //Constructor
        public DashboardViewModel()
        {
            MarketOverviewData = new ObservableCollection<MarketOverview>
        {
            new MarketOverview { Symbol = "AAPL", LastPrice = 150.23, Change24H = -1.23, Volume24H = 20000 },
            new MarketOverview { Symbol = "MSFT", LastPrice = 280.11, Change24H = 2.34, Volume24H = 15000 },
            new MarketOverview { Symbol = "GOOGL", LastPrice = 2500.45, Change24H = 0.98, Volume24H = 5000 },
        };
            StockHoldingsData = new ObservableCollection<StockHolding>
        {
            new StockHolding { Symbol = "AAPL", Volume = 150000, Units = 500 },
            new StockHolding { Symbol = "MSFT", Volume = 120000, Units = 400 },
            new StockHolding { Symbol = "GOOGL", Volume = 100000, Units = 350 },
            new StockHolding { Symbol = "AMZN", Volume = 80000, Units = 280 },
            new StockHolding { Symbol = "NVDA", Volume = 80000, Units = 300 }
        };
            MonthlyOrderData = new ObservableCollection<MonthlyOrderData>
        {
            new MonthlyOrderData { Month = "January", ProfitLoss = 25 },
            new MonthlyOrderData { Month = "February", ProfitLoss = 40 },
            new MonthlyOrderData { Month = "March", ProfitLoss = 30 },
            new MonthlyOrderData { Month = "April", ProfitLoss = 60 },
            new MonthlyOrderData { Month = "May", ProfitLoss = 80 }
        };
            PortfolioOverviewData = new ObservableCollection<PortfolioOverview>
        {
            new PortfolioOverview { Asset = "AAPL", Quantity = 100, AvgPrice = 150, MarketValue = 15500, UnrealizedPnL = 500 },
            new PortfolioOverview { Asset = "MSFT", Quantity = 50, AvgPrice = 200, MarketValue = 10500, UnrealizedPnL = 500 },
        };
            OrderManagementData = new ObservableCollection<OrderManagement>
        {
            new OrderManagement { OrderID = 1, Symbol = "AAPL", OrderType = "Buy", Quantity = 100, Status = "Completed" },
            new OrderManagement { OrderID = 2, Symbol = "MSFT", OrderType = "Sell", Quantity = 50, Status = "Pending" },
        };
        }
    }

    #region Demo Data Classes
    // MarketOverview Class
    public class MarketOverview
    {
        public string Symbol { get; set; }
        public double LastPrice { get; set; }
        public double Change24H { get; set; }
        public double Volume24H { get; set; }
    }
    // StockHolding Class
    public class StockHolding
    {
        public string Symbol { get; set; }
        public double Volume { get; set; }
        public int Units { get; set; }
    }
    // MonthlyOrderData Class
    public class MonthlyOrderData
    {
        public string Month { get; set; }
        public double ProfitLoss { get; set; }
    }
    // PortfolioOverview Class
    public class PortfolioOverview
    {
        public string Asset { get; set; }
        public int Quantity { get; set; }
        public double AvgPrice { get; set; }
        public double MarketValue { get; set; }
        public double UnrealizedPnL { get; set; }
    }
    // OrderManagement Class
    public class OrderManagement
    {
        public int OrderID { get; set; }
        public string Symbol { get; set; }
        public string OrderType { get; set; }
        public int Quantity { get; set; }
        public string Status { get; set; }
    } 
    #endregion
}
