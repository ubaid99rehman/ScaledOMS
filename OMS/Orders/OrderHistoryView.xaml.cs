using Microsoft.Extensions.DependencyInjection;
using OMS.ViewModels;
using System.Windows.Controls;

namespace OMS.Orders
{
    public partial class OrderHistoryView : UserControl
    {
        //Constructor
        public OrderHistoryView()
        {
            InitializeComponent();
            this.DataContext = AppServiceProvider.GetServiceProvider().GetRequiredService<OrderHistoryViewModel>();
        }
    }
}
