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
        //Service
        IStockDataService StockDataService;

        //Private Members
        private string symbol;
        private StockDetail _selectedStockDetails;
        
        //Public Members
        public string Symbol
        {
            get { return symbol; }
            set
            {
                SetProperty(ref symbol, value, nameof(Symbol));
                UpdateSelectedStockDetails();
            }
        }
        public StockDetail SelectedStockDetails
        {
            get => (StockDetail)StockDataService.GetStockDetail(symbol);
            set => SetProperty(ref _selectedStockDetails, value, nameof(SelectedStockDetails));
        }
        
        //Constructor
        public StockDetailViewModel(IStockDataService stockDataService)
        {
            SelectedStockDetails = new StockDetail();
            StockDataService = stockDataService;
            StockDataService.DataUpdated += OnDataRefreshed;
        }

        //Private Methods
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
