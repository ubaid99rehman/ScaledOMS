using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Grid;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Input;

namespace OMS
{
    public interface IContextMenuHelper
    {
        GridControl dataGrid { get; set; }
        GridColumn CurrentColumn {  get; set; }
        ContextMenu contextMenu {  get; set; }
        ObservableCollection<BarItem> MenuItems { get; set; }
        
        ContextMenu GetContextMenu();
        ObservableCollection<BarItem> GetMenuItems();

        void Mouse_Right_Button_Clicked(object sender, MouseButtonEventArgs e);
    }
}