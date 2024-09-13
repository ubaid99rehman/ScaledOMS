using DevExpress.Xpf.Charts;
using OMS.Core.Models.Stocks;
using OMS.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace OMS.Stocks
{

    public partial class StockChart : UserControl
    {
        public StockChart()
        {
            InitializeComponent();
        }

        private void ExportToCSV(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            if(this.DataContext is StockChartModel model) 
            {
                var data = model.StockTradingData;
            }
            ExportDisplayedChartData();
        }

        private void ExportDisplayedChartData()
        {
            var chartControl = (ChartControl)Chart;
            var volumeChartData = new List<StockTradingData>();
            var stockChartData = new List<StockTradingData>();

            var visibleRange = ((XYDiagram2D)ChartDiagram).AxisX.VisualRange;

            foreach (var series in chartControl.Diagram.Series)
            {
                if (series is StockSeries2D stockSeriesData)
                {
                    if (stockSeriesData.Visible)
                    {
                        foreach (var point in stockSeriesData.Points)
                        {
                            var stockData = point.Tag as StockTradingData;
                            if (stockData != null)
                            {
                                //Add All Datasource Points
                                //stockChartData.Add(stockData);

                                //Add Visible Range Points
                                var pointXValue = Convert.ToDateTime(point.Argument);
                                if ((DateTime)visibleRange.ActualMinValue <= pointXValue && pointXValue <= (DateTime)visibleRange.ActualMaxValue)
                                {
                                    stockChartData.Add(stockData);
                                }
                            }
                        }
                    }
                }
            }

            if (stockChartData.Any())
            {
                var saveFileDialog = new Microsoft.Win32.SaveFileDialog
                {
                    Filter = "CSV files (*.csv)|*.csv",
                    FileName = "StockData.csv"
                };

                if (saveFileDialog.ShowDialog() == true)
                {
                    using (var writer = new StreamWriter(saveFileDialog.FileName))
                    {
                        writer.WriteLine("RecordedTime,Open,High,Low,Close,Volume");

                        foreach (var data in stockChartData)
                        {
                            writer.WriteLine($"{data.RecordedTime},{data.Open},{data.High},{data.Low},{data.Close},{data.Volume}");
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("No visible data available for export.", "Export to CSV", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
