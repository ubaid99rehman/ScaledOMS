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

namespace OMS.ViewModels
{
    public class StockChartModel : ViewModelBase
    {
        //Visible Chart Data Points
        const int maxVisiblePointsCount = 180;

        //Service
        IStockTradeDataService StockTradeDataService;

        //Private Members
        private ChartIntervalItem _selectedInterval;
        private string selectedStockSymbol;
        private ObservableCollection<IStockTradingData> stockTradingData;
        
        //Public Members
        public ObservableCollection<IStockTradingData> StockTradingData
        {
            get => stockTradingData;
            set => SetProperty(ref stockTradingData, value, nameof(StockTradingData));
        }
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
        public List<ChartIntervalItem> IntervalsSource { get; private set; }

        //Constructor
        public StockChartModel(IStockTradeDataService stockTradeDataService)
        {
            StockTradingData = new ObservableCollection<IStockTradingData>();
            IntervalsSource = new List<ChartIntervalItem>();
            InitIntervals();
            SelectedInterval = IntervalsSource[0];
            StockTradeDataService = stockTradeDataService;
        }
        
        [Command]
        public void ChartScroll(XYDiagram2DScrollEventArgs eventArgs)
        {
            if (eventArgs.AxisX != null)
            {
                if ((DateTime)eventArgs.AxisX.ActualVisualRange.ActualMinValue < (DateTime)eventArgs.AxisX.ActualWholeRange.ActualMinValue)
                {
                    var tradingData = new ObservableCollection<IStockTradingData>();
                    TradeTimeInterval interval = GetTradeTimeInterval(SelectedInterval.MeasureUnit);
                    var lastItem = this.StockTradingData.Where(x => x.RecordedTime == StockTradingData.Min(d => d.RecordedTime)).FirstOrDefault();
                    if(lastItem != null)
                    {
                        if(SelectedInterval.MeasureUnit != DateTimeMeasureUnit.Year)
                        {
                            tradingData = StockTradeDataService.GetTradingData(selectedStockSymbol, lastItem.RecordedTime, 60, interval );
                        }
                    }
                    else
                    {
                        //tradingData = StockTradeDataService.GetTradingData(selectedStockSymbol, DateTime.Now, 25, interval );
                    }
                    if(tradingData.Any())
                    {
                        foreach (var trade in tradingData)
                        {
                            StockTradingData.Add(trade);
                        }
                    }
                }
            }
        }
        [Command]
        public void ChartZoom(XYDiagram2DZoomEventArgs eventArgs)
        {
            ManualDateTimeScaleOptions scaleOptions = eventArgs.AxisX.DateTimeScaleOptions as ManualDateTimeScaleOptions;
            if (scaleOptions != null)
            {
                TimeSpan measureUnitInterval = DateTimeHelper.GetTimeSpan(scaleOptions.MeasureUnit, scaleOptions.MeasureUnitMultiplier);
                DateTime max = (DateTime)eventArgs.AxisX.ActualVisualRange.ActualMaxValue;
                DateTime min = (DateTime)eventArgs.AxisX.ActualVisualRange.ActualMinValue;
                TimeSpan duration = max - min;
                double visibleUnitsCount = duration.TotalSeconds / measureUnitInterval.TotalSeconds;
                if (visibleUnitsCount > maxVisiblePointsCount)
                    eventArgs.AxisX.VisualRange.SetMinMaxValues(eventArgs.OldXRange.MinValue, eventArgs.OldXRange.MaxValue);
            }
        }

        //Private Methods 
        private void GetTradeData()
        {
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
        private void ChangeChartSourceData(TradeTimeInterval interval, ObservableCollection<IStockTradingData> tradingData)
        {
            StockTradingData.Clear();
            StockTradingData = tradingData;
        }

        private void InitIntervals()
        {
            IntervalsSource.Add(new ChartIntervalItem() { Caption = "Minute", MeasureUnit = DateTimeMeasureUnit.Minute, MeasureUnitMultiplier = 1 });
            IntervalsSource.Add(new ChartIntervalItem() { Caption = "Hour", MeasureUnit = DateTimeMeasureUnit.Hour, MeasureUnitMultiplier = 1 });
            IntervalsSource.Add(new ChartIntervalItem() { Caption = "Day", MeasureUnit = DateTimeMeasureUnit.Day, MeasureUnitMultiplier = 1 });
            IntervalsSource.Add(new ChartIntervalItem() { Caption = "Month", MeasureUnit = DateTimeMeasureUnit.Month, MeasureUnitMultiplier = 1 });
            IntervalsSource.Add(new ChartIntervalItem() { Caption = "Year", MeasureUnit = DateTimeMeasureUnit.Year, MeasureUnitMultiplier = 1 });
        }

        private TradeTimeInterval GetTradeTimeInterval(DateTimeMeasureUnit item)
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
    }
}
