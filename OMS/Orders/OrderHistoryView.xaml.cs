using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Grid;
using Microsoft.Extensions.DependencyInjection;
using OMS.ViewModels;
using System.Windows.Controls;
using System.Windows.Input;

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
