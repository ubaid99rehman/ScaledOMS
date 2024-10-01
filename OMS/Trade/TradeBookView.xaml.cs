using DevExpress.Xpf.Grid;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace OMS.Trade
{
    public partial class TradeBookView : UserControl
    {
        IContextMenuHelper ContextMenuHelper;
        GridColumn CurrentColumn;
        Point MousePositon;

        //Constructor
        public TradeBookView()
        {
            InitializeComponent();
            //Grid Context Menu
            ContextMenuHelper = AppServiceProvider.GetServiceProvider().GetRequiredService<IContextMenuHelper>();
            ContextMenuHelper.dataGrid = dataGrid;
            watchListView.ContextMenu = ContextMenuHelper.GetContextMenu(true,true,true,true,true);
        }
        //Mouse Click Events
        private void Mouse_Right_Button_Clicked(object sender, MouseButtonEventArgs e)
        {
            var hitInfo = ((TableView)dataGrid.View).CalcHitInfo(Mouse.GetPosition(dataGrid));
            if (hitInfo != null)
            {
                CurrentColumn = hitInfo.Column;
                ContextMenuHelper.CurrentColumn = CurrentColumn;
            }
            MousePositon = Mouse.GetPosition(Application.Current.MainWindow);
        }
    }
}
