using OMS.Common.Enums;
using OMS.Common.Helper;
using OMS.Core.Models;
using OMS.Core.Models.Stocks;
using OMS.DataAccess.Repositories.MarketRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMS.MarketData.Stocks
{
    public class StockTradeDataRepository : IStockTradeDataRepository
    {
        Random random;

        #region Constants
        const long volumeLow = 1000000000;
        const long volumeHigh = 1000000000000;
        #endregion

        List<string> StockSymbols;
        List<Stock> Stocks;
        IStockRepository StockRepository;

        private Dictionary<string, Dictionary<TradeTimeInterval, List<StockTradingData>>> tradesCache
            = new Dictionary<string, Dictionary<TradeTimeInterval, List<StockTradingData>>>();

        public StockTradeDataRepository(IStockRepository stockRepository)
        {
            Stocks = new List<Stock>();
            StockSymbols = new List<string>();
            
            random = new Random();
            StockRepository = stockRepository;
            LoadTradeData();
        }

        void LoadTradeData()
        {
            foreach (var symbol in StockSymbols)
            {
                AddInitialTrades(Stocks.Where(s => s.Symbol == symbol).FirstOrDefault());
            }
        }

        private void AddInitialTrades(Stock stock)
        {
            int tradeCount = 60;
            List<StockTradingData> trades = new List<StockTradingData>();
            var lastPrice = stock.LastPrice;

            for (int i = 0; i < tradeCount; i++)
            {
                var priceChange = (decimal)(random.NextDouble() * 2 - 1); 
                var openPrice = lastPrice + priceChange;  
                var high = openPrice + (decimal)(random.NextDouble() * 2); 
                var low = openPrice - (decimal)(random.NextDouble() * 2);  
                var close = low + ((decimal)random.NextDouble() * (high - low));

                StockTradingData trade = new StockTradingData
                {
                    Volume = (priceChange * (volumeHigh - volumeLow) + volumeLow),
                    High = Math.Round(high, 2),       
                    Low = Math.Round(low, 2),
                    Open = Math.Round(openPrice, 2),
                    Close = Math.Round(close, 2),
                    RecordedTime = DateTime.Now.AddMinutes(-i),
                };

                lastPrice = trade.Close;
                trades.Add(trade);
            }

            var recentTrades = new Dictionary<TradeTimeInterval, List<StockTradingData>>();
            recentTrades[TradeTimeInterval.Minute] = trades;
            tradesCache[stock.Symbol] = recentTrades;
        }

        void FetchStockData()
        {
            if (StockSymbols == null || StockSymbols.Count < 1)
            {
                StockSymbols = StockRepository.GetStockSymbols().ToList<string>();
            }
            if (Stocks == null || Stocks.Count < 1)
            {
                Stocks = StockRepository.GetAll().ToList<Stock>();
            }
        }

        public IEnumerable<StockTradingData> GetAll()
        {
            throw new NotImplementedException();
        }

        public StockTradingData GetById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<StockTradingData>> GetTradingData(string symbol,int time, TradeTimeInterval interval)
        {
            if (tradesCache.ContainsKey(symbol) && tradesCache[symbol].ContainsKey(interval))
            {
                return tradesCache[symbol][interval]; 
            }

            var lastPrice = GetLastPriceForSymbol(symbol);

            LazyLoad(symbol, lastPrice, time, interval);

            return tradesCache[symbol][interval];
        }

        public async Task<StockTradingData> GetTradeData(string symbol, int time, TradeTimeInterval interval)
        {
            var lastPrice =  GetLastPriceForSymbol(symbol);
            return Load(symbol, lastPrice, time, interval);
        }

        private StockTradingData Load(string symbol, decimal lastPrice, int time, TradeTimeInterval interval)
        {

            DateTime startTime = DateTimeHelper.GetStartTime(interval, time);
            DateTime endTime = DateTime.Now;

            var priceChange = (decimal)(random.NextDouble() * 2 - 1);
            var openPrice = lastPrice + priceChange;
            var high = openPrice + (decimal)(random.NextDouble() * 2);
            var low = openPrice - (decimal)(random.NextDouble() * 2);
            var close = low + ((decimal)random.NextDouble() * (high - low));

            var volume = (priceChange * (volumeHigh - volumeLow) + volumeLow);
            if (volume < 0)
            {
                volume = -volume;
            }

            StockTradingData trade = new StockTradingData
            {
                Volume = volume,
                High = Math.Round(high, 3),
                Low = Math.Round(low, 3),
                Open = Math.Round(openPrice, 3),
                Close = Math.Round(close, 3),
                RecordedTime = DateTime.Now
            };

            return trade;
        }

        private void LazyLoad(string symbol, decimal lastPrice, int time, TradeTimeInterval interval)
        {
            DateTime startTime = DateTimeHelper.GetStartTime(interval, time);
            DateTime endTime = DateTime.Now;
            
            List<StockTradingData> trades = new List<StockTradingData>();
            TimeSpan timeSpan = DateTimeHelper.GetTimeSpanForInterval(interval);

            for (var currentTime = startTime; currentTime <= endTime; currentTime = currentTime.Add(timeSpan))
            {
                var priceChange = (decimal)(random.NextDouble() * 2 - 1);
                var openPrice = lastPrice + priceChange;
                var high = openPrice + (decimal)(random.NextDouble() * 2);
                var low = openPrice - (decimal)(random.NextDouble() * 2);
                var close = low + ((decimal)random.NextDouble() * (high - low));

                var volume = (priceChange * (volumeHigh - volumeLow) + volumeLow);
                if(volume < 0)
                {
                    volume = -volume;
                }

                StockTradingData trade = new StockTradingData
                {
                    Volume = volume,
                    High = Math.Round(high, 3),
                    Low = Math.Round(low, 3),
                    Open = Math.Round(openPrice, 3),
                    Close = Math.Round(close, 3),
                    RecordedTime = currentTime
                };

                trades.Add(trade);
                lastPrice = trade.Close;
            }

            if (!tradesCache.ContainsKey(symbol))
            {
                tradesCache[symbol] = new Dictionary<TradeTimeInterval, List<StockTradingData>>();
                tradesCache[symbol][interval] = new List<StockTradingData>();
            }
            else
            {
                if (!tradesCache[symbol].ContainsKey(interval))
                {
                    tradesCache[symbol][interval] = new List<StockTradingData>();
                }
            }

            if(tradesCache[symbol][interval].Count <1)
            {
                tradesCache[symbol][interval] = trades;
            }
            else
            {
                foreach (var trade in trades) 
                {
                    tradesCache[symbol][interval].Add(trade);
                }
            }
        }

        private decimal GetLastPriceForSymbol(string symbol)
        {
            return tradesCache.ContainsKey(symbol) && tradesCache[symbol].ContainsKey(TradeTimeInterval.Minute)
                ? tradesCache[symbol][TradeTimeInterval.Minute].Last().Close
                : (decimal)(100 + new Random().NextDouble() * 10); 
        }

    }
}
