using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Grid;
using Microsoft.Extensions.DependencyInjection;
using OMS.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace OMS.Orders
{
    public partial class OrderHistoryView : UserControl
    {
        OrderHistoryViewModel model;

        //Constructor
        public OrderHistoryView()
        {
            InitializeComponent();
            model =AppServiceProvider.GetServiceProvider().GetRequiredService<OrderHistoryViewModel>();
            this.DataContext = model;
            SetUsersVisibility();
        }

        private void SetUsersVisibility()
        {
            if (model.ShowUsers)
            {
                UsersList.Visibility = Visibility.Visible;
            }
            else
            {
                UsersList.Visibility = Visibility.Collapsed;
            }
        }
    }
}
