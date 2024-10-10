using DevExpress.Xpf.Grid;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Windows.Controls;

namespace OMS.Trade
{
    public partial class TradeBookView : UserControl
    {
        IContextMenuHelper ContextMenuHelper;

        //Constructor
        public TradeBookView()
        {
            InitializeComponent();
            //Grid Context Menu
            ContextMenuHelper = AppServiceProvider.GetServiceProvider().GetRequiredService<IContextMenuHelper>();
            ContextMenuHelper.dataGrid = dataGrid;
            watchListView.ContextMenu = ContextMenuHelper.GetContextMenu(true,true,true,true,true);
        }
    }
}
