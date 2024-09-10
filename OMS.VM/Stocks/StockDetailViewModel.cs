using DevExpress.Mvvm;
using OMS.Core.Models;
using System;
using System.Windows.Threading;

namespace OMS.ViewModels
{
    public class StockDetailViewModel : ViewModelBase
    {
        private readonly DispatcherTimer _updateTimer;
        private readonly Random _random;

        private StockDetail _selectedStockDetails;
        public StockDetail SelectedStockDetails
        {
            get => _selectedStockDetails;
            set => SetProperty(ref _selectedStockDetails, value, nameof(SelectedStockDetails));
        }

        public StockDetailViewModel()
        {
            _random = new Random();
            SelectedStockDetails = new StockDetail(0);
            _updateTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            _updateTimer.Tick += new EventHandler(UpdateStockDetails);
            _updateTimer.Start();
        }

        private void UpdateStockDetails(object sender, EventArgs e)
        {
            decimal randomFactor = (decimal)(_random.NextDouble() * 0.02 - 0.01); // -1% to +1%

            if(string.IsNullOrEmpty(SelectedStockDetails.Symbol) )
            {
                SelectedStockDetails.Symbol = "Stock";
            }
            SelectedStockDetails.LastPrice = Math.Round(SelectedStockDetails.LastPrice * (1 + randomFactor), 3);
            SelectedStockDetails.Volume24H = Math.Round(SelectedStockDetails.Volume24H * (1 + randomFactor), 3);
            SelectedStockDetails.Change24H = randomFactor * 100; // Change in percentage
            SelectedStockDetails.High24H = Math.Max(SelectedStockDetails.High24H, SelectedStockDetails.LastPrice); // Update high if new price is higher
            SelectedStockDetails.Low24H = Math.Min(SelectedStockDetails.Low24H, SelectedStockDetails.LastPrice); // Update low if new price is lower
        }
    }
}
