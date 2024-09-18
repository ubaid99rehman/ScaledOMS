﻿using OMS.Common.Enums;
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
            StockRepository = stockRepository;
            random = new Random();
            StockSymbols = new List<string>();
            Stocks = new List<Stock>();
            LoadTradeData();
        }

        void LoadTradeData()
        {
            //Loads Symbols and Stock Prices
            FetchStockData();
            foreach (var symbol in StockSymbols)
            {
                AddInitialTrades(Stocks.Where(s => s.Symbol == symbol).FirstOrDefault());
            }
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

        private void AddInitialTrades(Stock stock)
        {
            int tradeCount = 180;
            List<StockTradingData> trades = new List<StockTradingData>();
            var lastPrice = stock.LastPrice;

            for (int i = 0; i < tradeCount; i++)
            {
                var priceChange = (decimal)(random.NextDouble() * 2 - 1); 
                var openPrice = lastPrice + priceChange;  
                var high = openPrice + (decimal)(random.NextDouble() * 2); 
                var low = openPrice - (decimal)(random.NextDouble() * 2);  
                var close = low + ((decimal)random.NextDouble() * (high - low));
                var volume = (priceChange * (volumeHigh - volumeLow) + volumeLow);
                if(volume < 0)
                {
                    volume = 0 - volume;
                }

                StockTradingData trade = new StockTradingData
                {
                    Volume = volume,
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

        public IEnumerable<StockTradingData> GetAll()
        {
            throw new NotImplementedException();
        }

        public StockTradingData GetById(int id)
        {
            throw new NotImplementedException();
        }
        
        public async Task<StockTradingData> GetTradeData(string symbol, 
            int time, TradeTimeInterval interval)
        {
            var lastPrice =  GetLastPriceForSymbol(symbol,interval);
            return Load(symbol, lastPrice, time, interval);
        }

        public async Task<IEnumerable<StockTradingData>> 
            GetTradingData(string symbol, DateTime startTime, int points, TradeTimeInterval interval)
        {
            //Time period
            if(startTime == null)
            {
                startTime = DateTime.Now;
            }
            DateTime endTime = startTime;  
            DateTime fromTime = DateTimeHelper.GetStartTime(interval, points, startTime);

            if (tradesCache.ContainsKey(symbol) && tradesCache[symbol].ContainsKey(interval))
            {
                var cachedTrades = tradesCache[symbol][interval]
                    .Where(trade => trade.RecordedTime >= fromTime && trade.RecordedTime <= startTime)
                    .ToList();

                if (cachedTrades.Count >= points)
                {
                    return cachedTrades.Take(points).ToList();
                }
            }

            var lastPrice = GetLastPriceForSymbol(symbol,interval,startTime);  
            LazyLoad(symbol, lastPrice, points, interval, startTime);

            if(tradesCache.ContainsKey(symbol) && tradesCache[symbol].ContainsKey(interval))
            {
                return tradesCache[symbol][interval]
                    .Where(trade => trade.RecordedTime < startTime).Take(points).ToList();
            }
            return new List<StockTradingData>();
        }

        private StockTradingData Load(string symbol, decimal lastPrice, 
            int time, TradeTimeInterval interval)
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

            if (tradesCache.ContainsKey(symbol) && tradesCache[symbol].ContainsKey(interval))
            {
                tradesCache[symbol][interval].Add(trade);
            }
            return trade;
        }

        private void LazyLoad(string symbol, decimal lastPrice, int points, TradeTimeInterval interval, DateTime toTime)
        {
            List<StockTradingData> trades = new List<StockTradingData>();

            DateTime startTime = DateTimeHelper.GetStartTime(interval, points, toTime);
            DateTime endTime = toTime;
            TimeSpan timeSpan = DateTimeHelper.GetTimeSpanForInterval(interval);

            for (var currentTime = endTime.Add(-timeSpan); currentTime >= startTime; currentTime = currentTime.Add(-timeSpan))
            {
                var priceChange = (decimal)(random.NextDouble() * 2 - 1);
                var close = lastPrice;
                var openPrice = lastPrice + priceChange;
                var high = openPrice + (decimal)(random.NextDouble() * 2);
                var low = openPrice - (decimal)(random.NextDouble() * 2);
                //var close = low + ((decimal)random.NextDouble() * (high - low));
                var volume = (priceChange * (volumeHigh - volumeLow) + volumeLow);
                if (volume < 0)
                {
                    volume = 0-volume;
                }

                StockTradingData trade = new StockTradingData
                {
                    Volume = volume,
                    High = Math.Round(high, 3),
                    Low = Math.Round(low, 3),
                    Open = Math.Round(openPrice, 3),
                    Close = Math.Round(close, 3),
                    RecordedTime = currentTime,
                };

                lastPrice = trade.Open;
                trades.Add(trade);
            }

            if (!tradesCache.ContainsKey(symbol))
            {
                tradesCache[symbol] = new Dictionary<TradeTimeInterval, List<StockTradingData>>();
            }

            if (!tradesCache[symbol].ContainsKey(interval))
            {
                tradesCache[symbol][interval] = new List<StockTradingData>();
            }

            tradesCache[symbol][interval].AddRange(trades.Where(t => !tradesCache[symbol][interval]
                                                       .Any(cachedTrade => cachedTrade.RecordedTime == t.RecordedTime)));
        }

        private decimal GetLastPriceForSymbol(string symbol)
        {
            return tradesCache.ContainsKey(symbol) && tradesCache[symbol].ContainsKey(TradeTimeInterval.Minute)
                ? tradesCache[symbol][TradeTimeInterval.Minute].Last().Close
                : (decimal)(100 + new Random().NextDouble() * 10); 
        }
        
        private decimal GetLastPriceForSymbol(string symbol, TradeTimeInterval interval)
        {
            return tradesCache.ContainsKey(symbol) && tradesCache[symbol].ContainsKey(interval)
                ? tradesCache[symbol][interval].Last().Close
                : (decimal)(100 + new Random().NextDouble() * 10); 
        }

        private decimal GetLastPriceForSymbol(string symbol, TradeTimeInterval interval, DateTime time)
        {
            if( tradesCache.ContainsKey(symbol) && tradesCache[symbol].ContainsKey(interval))
            {
                var trade =  tradesCache[symbol][interval].Where(x => x.RecordedTime == time).FirstOrDefault().Open;
                return trade;
            }
            
            return (decimal)(100 + new Random().NextDouble() * 10);
        }

    }
}
