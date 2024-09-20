using DevExpress.Xpf.Grid;
using System.Windows.Controls;

namespace OMS
{
    public interface IContextMenuHelper
    {
        GridControl dataGrid { get; set; }
        GridColumn CurrentColumn {  get; set; }
        ContextMenu contextMenu {  get; set; }
        ContextMenu CreateContextMenu();
    }
}