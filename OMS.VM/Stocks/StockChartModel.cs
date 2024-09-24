using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Xpf.Charts;
using OMS.Common.Helper;
using OMS.Core.Models.Stocks;
using OMS.Core.Services.MarketServices.RealtimeServices;
using OMS.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace OMS.ViewModels
{
    public class StockChartModel : ViewModelBase
    {
        IStockTradeDataService StockTradeDataService;

        const int initialVisiblePointsCount = 180;
        const int maxVisiblePointsCount = 800;
        bool initRange = false;
        public virtual object MinVisibleDate { get; set; }

        Dictionary<TradeTimeInterval, ObservableCollection<StockTradingData>> tradesCache;

        private ChartIntervalItem _selectedInterval;
        public ChartIntervalItem SelectedInterval
        {
            get => _selectedInterval;
            set 
            {
                SetProperty(ref _selectedInterval, value, nameof(SelectedInterval));
                if(_selectedInterval != null)
                {
                    if (SelectedStockSymbol != null)
                    {
                        GetTradeData();
                    }
                }
            }
        }

        public List<ChartIntervalItem> IntervalsSource { get; private set; }

        private string selectedStockSymbol;
        public string SelectedStockSymbol
        {
            get { return selectedStockSymbol; }
            set
            {
                if(value != selectedStockSymbol)
                {
                    SetProperty(ref selectedStockSymbol, value, nameof(SelectedStockSymbol));
                    
                    if(!string.IsNullOrEmpty(selectedStockSymbol))
                    {
                        GetTradeData();
                    }
                }
            }
        }

        private ObservableCollection<StockTradingData> stockTradingData;
        public ObservableCollection<StockTradingData> StockTradingData
        {
            get => stockTradingData;
            set => SetProperty(ref stockTradingData, value, nameof(StockTradingData));
        }

        public StockChartModel(IStockTradeDataService stockTradeDataService)
        {
            tradesCache = new Dictionary<TradeTimeInterval, ObservableCollection<StockTradingData>>();
            StockTradingData = new ObservableCollection<StockTradingData>();
            IntervalsSource = new List<ChartIntervalItem>();
            InitIntervals();
            SelectedInterval = IntervalsSource[0];
            StockTradeDataService = stockTradeDataService;
            stockTradeDataService.DataUpdated += OnDataUpdated;
        }
        
        private void GetTradeData()
        {
            StockTradingData.Clear();
            TradeTimeInterval interval = GetTradeTimeInterval(SelectedInterval.MeasureUnit);
            if (interval == TradeTimeInterval.Year)
            {
                var tradeData = StockTradeDataService.GetTradingData(SelectedStockSymbol, DateTime.Now, 25, interval);
                ChangeChartSourceData(interval, tradeData);
            }
            else
            {
                var Data = StockTradeDataService.GetTradingData(SelectedStockSymbol, DateTime.Now, 180, interval);
                ChangeChartSourceData(interval, Data);
            }
        }

        [Obsolete]
        private void LoadIntervalData()
        {
            TradeTimeInterval interval = GetTradeTimeInterval(SelectedInterval.MeasureUnit);
            var tradeData = StockTradeDataService.GetTradingData(SelectedStockSymbol,DateTime.Now, 180,interval);
            ChangeChartSourceData(interval,tradeData);
        }

        void OnDataUpdated()
        {
            var tradeData = StockTradeDataService.GetBySymbol(selectedStockSymbol);
            TradeTimeInterval interval = TradeTimeInterval.Minute;

            if (!tradesCache.ContainsKey(interval))
            {
                tradesCache.Add(interval, new ObservableCollection<StockTradingData> { tradeData });
            }
            else
            {
                if (tradesCache[interval].Count < 1 || tradesCache[interval] == null)
                {
                    tradesCache[interval] = new ObservableCollection<StockTradingData> { tradeData };
                }
                else
                {
                    lock (tradesCache)
                    {
                        tradesCache[interval].Add(tradeData);
                    }
                }
            }
        }

        void InitIntervals()
        {
            IntervalsSource.Add(new ChartIntervalItem() { Caption = "Minute", MeasureUnit = DateTimeMeasureUnit.Minute, MeasureUnitMultiplier = 1 });
            IntervalsSource.Add(new ChartIntervalItem() { Caption = "Hour", MeasureUnit = DateTimeMeasureUnit.Hour, MeasureUnitMultiplier = 1 });
            IntervalsSource.Add(new ChartIntervalItem() { Caption = "Day", MeasureUnit = DateTimeMeasureUnit.Day, MeasureUnitMultiplier = 1 });
            IntervalsSource.Add(new ChartIntervalItem() { Caption = "Month", MeasureUnit = DateTimeMeasureUnit.Month, MeasureUnitMultiplier = 1 });
            IntervalsSource.Add(new ChartIntervalItem() { Caption = "Year", MeasureUnit = DateTimeMeasureUnit.Year, MeasureUnitMultiplier = 1 });
        }

        void ChangeChartSourceData(TradeTimeInterval interval, ObservableCollection<StockTradingData> tradingData)
        {
            if (!tradesCache.ContainsKey(interval))
            {
                tradesCache.Add(interval, tradingData);
            }
            else
            {
                if (tradesCache[interval].Count < 1 || tradesCache[interval] == null)
                {
                    tradesCache[interval] = tradingData;
                }
                else
                {
                    lock (tradesCache) 
                    {
                        foreach (var trade in tradingData)
                        {
                            tradesCache[interval].Add(trade);
                        }
                    }
                }
            }
            StockTradingData.Clear();
            StockTradingData = tradesCache[interval];
        }

        TradeTimeInterval GetTradeTimeInterval(DateTimeMeasureUnit item)
        {
            switch (item)
            {
                case DateTimeMeasureUnit.Minute:
                    return TradeTimeInterval.Minute;
                case DateTimeMeasureUnit.Hour:
                    return TradeTimeInterval.Hour;
                case DateTimeMeasureUnit.Day:
                    return TradeTimeInterval.Day;
                case DateTimeMeasureUnit.Month:
                    return TradeTimeInterval.Month;
                case DateTimeMeasureUnit.Year:
                    return TradeTimeInterval.Year;
                default:
                    return TradeTimeInterval.Minute;
            }
        }

        [Command]
        public void ChartScroll(XYDiagram2DScrollEventArgs eventArgs)
        {
            if (eventArgs.AxisX != null)
            {
                if ((DateTime)eventArgs.AxisX.ActualVisualRange.ActualMinValue < (DateTime)eventArgs.AxisX.ActualWholeRange.ActualMinValue)
                {
                    var tradingData = new ObservableCollection<StockTradingData>();
                    TradeTimeInterval interval = GetTradeTimeInterval(SelectedInterval.MeasureUnit);
                    var lastItem = this.StockTradingData.Where(x => x.RecordedTime == StockTradingData.Min(d => d.RecordedTime)).FirstOrDefault();
                    if(lastItem != null)
                    {
                        if(SelectedInterval.MeasureUnit != DateTimeMeasureUnit.Year)
                        {
                            tradingData = StockTradeDataService.GetTradingData(selectedStockSymbol, lastItem.RecordedTime, 10, interval );
                        }
                    }
                    else
                    {
                        tradingData = StockTradeDataService.GetTradingData(selectedStockSymbol, DateTime.Now, 180, interval );
                    }
                     
                    foreach (var trade in tradingData)
                    {
                        tradesCache[interval].Add(trade);
                    }
                    foreach(var trade in tradingData)
                    {
                        StockTradingData.Add(trade);
                    }
                }
            }
        }

        [Command]
        public void ChartZoom(XYDiagram2DZoomEventArgs eventArgs)
        {
            //ManualDateTimeScaleOptions scaleOptions = eventArgs.AxisX.DateTimeScaleOptions as ManualDateTimeScaleOptions;
            //if (scaleOptions != null)
            //{
            //    TimeSpan measureUnitInterval = DateTimeHelper.GetInterval(scaleOptions.MeasureUnit, scaleOptions.MeasureUnitMultiplier);
            //    DateTime max = (DateTime)eventArgs.AxisX.ActualVisualRange.ActualMaxValue;
            //    DateTime min = (DateTime)eventArgs.AxisX.ActualVisualRange.ActualMinValue;
            //    TimeSpan duration = max - min;
            //    double visibleUnitsCount = duration.TotalSeconds / measureUnitInterval.TotalSeconds;
            //    if (visibleUnitsCount > maxVisiblePointsCount)
            //        eventArgs.AxisX.VisualRange.SetMinMaxValues(eventArgs.OldXRange.MinValue, eventArgs.OldXRange.MaxValue);
            //}
        }

        [Command]
        public void DataChanged(RoutedEventArgs e)
        {
            ChartControl chart = e.Source as ChartControl;
            if (chart != null)
                InitChartRange(chart);
        }

        void InitChartRange(ChartControl chart)
        {
            if (!initRange)
            {
                ((XYDiagram2D)chart.Diagram).ActualAxisX.ActualVisualRange.SetAuto();
                MinVisibleDate = DateTime.Now - DateTimeHelper.ConvertInterval(SelectedInterval, initialVisiblePointsCount);
                initRange = true;
            }
        }
    }
}
