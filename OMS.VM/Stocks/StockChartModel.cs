using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Editors.RangeControl;
using OMS.Common.Helpers;
using OMS.Core.Enums;
using OMS.Core.Models.Stocks;
using System;
using System.Collections.Generic;
using System.Windows;


namespace OMS.ViewModels
{
    public class StockChartModel : ViewModelBase
    {
        private string selectedStockSymbol;
        public string SelectedStockSymbol
        {
            get { return selectedStockSymbol; }
            set
            {
                SetProperty(ref selectedStockSymbol, value, nameof(SelectedStockSymbol));
            }
        }

        public StockChartModel()
        {
            
        }
    }
}
