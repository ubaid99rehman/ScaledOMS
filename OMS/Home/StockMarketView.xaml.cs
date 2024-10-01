using Microsoft.Extensions.DependencyInjection;
using OMS.ViewModels;
using System.Windows.Controls;

namespace OMS.Home
{
    public partial class StockMarketView : UserControl
    {
        //Constructor
        public StockMarketView()
        {
            InitializeComponent();
            this.DataContext = AppServiceProvider.GetServiceProvider().GetRequiredService<StockMarketModel>();
        }
    }
}
