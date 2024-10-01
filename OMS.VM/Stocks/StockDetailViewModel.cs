using DevExpress.Mvvm;
using OMS.Core.Models;
using OMS.Core.Services.AppServices;
using OMS.Core.Services.MarketServices.RealtimeServices;
using System;
using System.Windows.Threading;

namespace OMS.ViewModels
{
    public class StockDetailViewModel : ViewModelBase
    {
        IStockDataService StockDataService;

        private string symbol;
        public string Symbol
        {
            get { return symbol; }
            set
            {
                SetProperty(ref symbol, value, nameof(Symbol));
                UpdateSelectedStockDetails();
            }
        }

        private StockDetail _selectedStockDetails;
        public StockDetail SelectedStockDetails
        {
            get => (StockDetail)StockDataService.GetStockDetail(symbol);
            set => SetProperty(ref _selectedStockDetails, value, nameof(SelectedStockDetails));
        }

        public StockDetailViewModel(IStockDataService stockDataService)
        {
            SelectedStockDetails = new StockDetail();
            StockDataService = stockDataService;
            StockDataService.DataUpdated += OnDataRefreshed;
        }

        private void UpdateSelectedStockDetails()
        {
            SelectedStockDetails = (StockDetail)StockDataService.GetStockDetail(Symbol);
        }

        public void OnDataRefreshed()
        {
            UpdateSelectedStockDetails();
        }
    }
}
