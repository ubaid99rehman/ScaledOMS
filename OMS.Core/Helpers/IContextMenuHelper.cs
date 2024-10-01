using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Core;
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

        ObservableCollection<BarItem> GetAllMenuItems();
        ObservableCollection<BarItem> GetSortingMenuItems();
        ObservableCollection<BarItem> GetAlignmentMenuItems();
        ObservableCollection<BarItem> GetSearchAndFilterMenuItems();
        ContextMenu GetContextMenu(bool sorting, bool align, bool grouping, bool search, bool filter);

        void Mouse_Right_Button_Clicked(object sender, MouseButtonEventArgs e);
    }
}