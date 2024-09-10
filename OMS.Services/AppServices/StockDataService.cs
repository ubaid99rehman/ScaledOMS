using OMS.Core.Enums;
using OMS.Core.Models.Stocks;
using OMS.Core.Models;
using OMS.Core.Services.AppServices;
using System;
using System.Collections.ObjectModel;
using DevExpress.Xpf.Core;
using System.Collections.Generic;
using System.Linq;
using DevExpress.Mvvm.Native;
using System.Windows.Threading;


namespace OMS.Services.AppServices
{
    public class StockDataService : IStockDataService
    {

        public const int Tick = 500;
        readonly DispatcherTimer updateTimer;
        readonly Random _random;

        #region Props and Constructor

        const int pointsLimit = 1200;
        const int yearLimit = 1991;
        double basePrice;
        const int volumeMinLow = 30000000;
        const int volumeMaxLow = 60000000;
        const int volumeMinHigh = 1000000000;
        const int volumeMaxHigh = 2000000000;

        private ObservableCollection<Stock> _stocks;
        private ObservableCollection<string> _stockSymbols;
        private ObservableCollection<StockDetail> _stockDetails;
        private Dictionary<string, ObservableCollectionCore<StockTradingData>> _stockTradingData;

        public StockDataService()
        {
            _stocks = new ObservableCollection<Stock>();
            _stockSymbols = new ObservableCollection<string>();
            _stockDetails = new ObservableCollection<StockDetail>();
            _stockTradingData = new Dictionary<string, ObservableCollectionCore<StockTradingData>>();

            _random = new Random();
            updateTimer = new DispatcherTimer(DispatcherPriority.ApplicationIdle);
            InitTimer();

            LoadStocks();
            LoadStockSymbols();
            LoadInitialStockDetailData();
            LoadInitialStocksTradingData();
        }
        #endregion

        void InitTimer()
        {
            updateTimer.Interval = TimeSpan.FromMilliseconds(Tick);
            updateTimer.Tick += new EventHandler(Refresh);
        }

        public Stock GetStock(string symbol)
        {
            return _stocks.Where(s => s.Symbol == symbol).FirstOrDefault();
        }

        public StockDetail GetStockDetail(string symbol)
        {
            return _stockDetails.Where(s => s.Symbol == symbol).FirstOrDefault();
        }

        public ObservableCollection<Stock> GetStocks()
        {
            return _stocks;
        }

        public ObservableCollection<string> GetStockSymbols()
        {
            return _stockSymbols;
        }

        public ObservableCollection<StockDetail> GetStockDetails()
        {
            return _stockDetails;
        }

        public ObservableCollection<StockTradingData> GetStockTradingData(string symbol)
        {
            if (_stockTradingData.ContainsKey(symbol))
            {
                return _stockTradingData[symbol];
            }
            return new ObservableCollection<StockTradingData>();
        }

        public ObservableCollectionCore<StockTradingData> GetTradingData(string symbol, MeasureUnit unit = MeasureUnit.Month, int period = 1)
        {
            if (!_stockTradingData.ContainsKey(symbol))
                throw new ArgumentException($"No trading data available for symbol: {symbol}");

            DateTime currentDate = DateTime.Now;
            DateTime startDate = CalculateStartDate(currentDate, unit, period);

            var data = _stockTradingData[symbol]
                .Where(td => td.RecordedTime >= startDate && td.RecordedTime <= currentDate)
                .ToObservableCollection<StockTradingData>();

            ObservableCollectionCore<StockTradingData> outData = new ObservableCollectionCore<StockTradingData>();

            outData.Assign(data);

            return outData;
        }

        private DateTime CalculateStartDate(DateTime currentDate, MeasureUnit unit, int period)
        {
            switch (unit)
            {
                case MeasureUnit.Day:
                    return currentDate.AddDays(-period);
                case MeasureUnit.Week:
                    return currentDate.AddDays(-7 * period);
                case MeasureUnit.Month:
                    return currentDate.AddMonths(-period);
                case MeasureUnit.Year:
                    return currentDate.AddYears(-period);
                default:
                    return currentDate;
            }

        }

        public void LazyLoadStocksTradingData()
        {
            if (_stockTradingData.Count <= 1 || _stockTradingData == null)
            {
                Random random = new Random();
                DateTime currentDate = DateTime.Now;
                DateTime startDate = currentDate.AddMonths(-3);

                foreach (var stock in _stocks)
                {
                    var tradingData = new ObservableCollectionCore<StockTradingData>();
                    decimal lastClosePrice = stock.LastPrice;

                    for (DateTime date = startDate; date <= currentDate; date = date.AddDays(1))
                    {
                        decimal changePercent = (decimal)(random.NextDouble() * 0.02 - 0.01); // -1% to +1%
                        decimal open = lastClosePrice * (1 + changePercent);
                        decimal high = open * (decimal)(1 + random.NextDouble() * 0.02);  // up to +2%
                        decimal low = open * (decimal)(1 - random.NextDouble() * 0.02);   // down to -2%
                        decimal close = (low + high) / 2; // Close is average of high and low

                        // Ensure the high is always greater than or equal to the low
                        if (low > high)
                        {
                            var temp = low;
                            low = high;
                            high = temp;
                        }

                        var data = new StockTradingData
                        {
                            //Symbol = stock.Symbol,
                            Volume = (decimal)(random.NextDouble() * (double)(volumeMaxHigh - volumeMinHigh) + (double)volumeMinHigh),
                            High = Math.Round(high, 2),
                            Low = Math.Round(low, 2),
                            Open = Math.Round(open, 2),
                            Close = Math.Round(close, 2),
                            RecordedTime = date
                        };

                        tradingData.Add(data);

                        lastClosePrice = close;
                    }



                    _stockTradingData[stock.Symbol] = tradingData;
                }
            }

        }

        public void LazyLoadStocksDetailData()
        {
            if (_stockDetails.Count <= 1 || _stockTradingData == null)
            {
                Random random = new Random();

                foreach (var stock in _stocks)
                {
                    var stockDetail = new StockDetail();
                    stockDetail.Symbol = stock.Symbol;
                    stockDetail.LastPrice = stock.LastPrice;
                    stockDetail.Volume24H = random.Next(1000, 100000); 
                    stockDetail.Change24H = Math.Round((decimal)(random.NextDouble() * 4 - 2), 2); // Random change between -2% and +2%
                    stockDetail.High24H = stock.LastPrice * (1 + Math.Abs((decimal)random.NextDouble() * 0.02m)); // Up to +2% of LastPrice
                    stockDetail.Low24H = stock.LastPrice * (1 - Math.Abs((decimal)random.NextDouble() * 0.02m));  // Down to -2% of LastPrice

                    _stockDetails.Add(stockDetail);
                }
            }
        }

        public void LoadStocks()
        {
            _stocks = new ObservableCollection<Stock>

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

        public void LoadStockSymbols()
        {
            _stockSymbols.Clear();
            foreach (var stock in _stocks)
            {
                _stockSymbols.Add(stock.Symbol);
            }
        }

        public void LoadInitialStockDetailData()
        {
            Random random = new Random();

            var stock = new StockDetail();
            stock.LastPrice = _stocks.First().LastPrice;
            stock.Volume24H = (decimal)(random.NextDouble() * (double)(volumeMaxHigh - volumeMinLow) + (double)volumeMinLow);
            stock.Change24H = Math.Round((decimal)(random.NextDouble() * 4 - 2), 2); // Random change between -2% and +2%
            stock.High24H = stock.LastPrice * (1 + Math.Abs((decimal)random.NextDouble() * 0.02m)); // Up to +2% of LastPrice
            stock.Low24H = stock.LastPrice * (1 - Math.Abs((decimal)random.NextDouble() * 0.02m));  // Down to -2% of LastPrice

            _stockDetails.Add(stock);

        }

        public void LoadInitialStocksTradingData()
        {
            Random random = new Random();
            DateTime currentDate = DateTime.Now;
            DateTime startDate = currentDate.AddMonths(-3);

            var stock = _stocks.FirstOrDefault();
            var tradingData = new ObservableCollectionCore<StockTradingData>();
            decimal lastClosePrice = stock.LastPrice;

            for (DateTime date = startDate; date <= currentDate; date = date.AddDays(1))
            {
                decimal changePercent = (decimal)(random.NextDouble() * 0.02 - 0.01); // -1% to +1%
                decimal open = lastClosePrice * (1 + changePercent);
                decimal high = open * (decimal)(1 + random.NextDouble() * 0.02);  // up to +2%
                decimal low = open * (decimal)(1 - random.NextDouble() * 0.02);   // down to -2%
                decimal close = (low + high) / 2; // Close is average of high and low

                // Ensure the high is always greater than or equal to the low
                if (low > high)
                {
                    var temp = low;
                    low = high;
                    high = temp;
                }


                var data = new StockTradingData
                {
                    //Symbol = stock.Symbol,
                    Volume = (decimal)(random.NextDouble() * (double)(volumeMaxHigh - volumeMinLow) + (double)volumeMinLow),
                    High = Math.Round(high, 2),
                    Low = Math.Round(low, 2),
                    Open = Math.Round(open, 2),
                    Close = Math.Round(close, 2),
                    RecordedTime = date
                };

                tradingData.Add(data);

                lastClosePrice = close;
            }

            _stockTradingData[stock.Symbol] = tradingData;
        }

        public void StartSession()
        {
            updateTimer.Start();
        }

        public void Refresh(object sender, EventArgs e)
        {
            decimal changePercent = 0.5m;

            lock (_stockDetails)
            {
                foreach (var stockDetail in _stockDetails)
                {
                    decimal newLastPrice = Math.Round(stockDetail.LastPrice * (1 + changePercent / 100), 3);
                    stockDetail.LastPrice = newLastPrice;
                    stockDetail.Change24H = changePercent;

                    decimal highChange = Math.Round((decimal)(_random.NextDouble() * 0.02), 4);
                    decimal lowChange = Math.Round((decimal)(_random.NextDouble() * 0.02), 4);

                    highChange = _random.NextDouble() > 0.5 ? highChange : -highChange;
                    lowChange = _random.NextDouble() > 0.5 ? lowChange : -lowChange;

                    decimal newHigh24H = Math.Round(newLastPrice * (1 + highChange), 3);
                    decimal newLow24H = Math.Round(newLastPrice * (1 + lowChange), 3);

                    if (newLow24H > newHigh24H)
                    {
                        var temp = newLow24H;
                        newLow24H = newHigh24H;
                        newHigh24H = temp;
                    }

                    stockDetail.High24H = newHigh24H;
                    stockDetail.Low24H = newLow24H;

                    decimal volumeChangePercent = Math.Round((decimal)(_random.NextDouble() * 10 - 5), 2); // -5.00 to +5.00
                    stockDetail.Volume24H = Math.Round(stockDetail.Volume24H * (1 + volumeChangePercent / 100), 3);
                }
            }
            UpdateTradingData();
        }

        private void UpdateTradingData()
        {
            DateTime currentDate = DateTime.Now;

            foreach (var stock in _stocks)
            {
                if (_stockTradingData.TryGetValue(stock.Symbol, out var tradingData))
                {
                    var latestDetail = _stockDetails.FirstOrDefault(sd => sd.Symbol == stock.Symbol);
                    if (latestDetail != null)
                    {
                        var newTradingData = new StockTradingData
                        {
                            Volume = latestDetail.Volume24H,
                            High = latestDetail.High24H,
                            Low = latestDetail.Low24H,
                            Open = latestDetail.LastPrice, 
                            Close = latestDetail.LastPrice,
                            RecordedTime = currentDate
                        };

                        tradingData.Insert(0, newTradingData);

                        if (tradingData.Count > pointsLimit)
                        {
                            tradingData.RemoveAt(tradingData.Count - 1);
                        }
                    }
                }
            }
        }
    }

}
