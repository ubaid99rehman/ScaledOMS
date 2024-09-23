using DevExpress.Xpf.Grid;
using Microsoft.Extensions.DependencyInjection;
using OMS.Core.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace OMS.Trade
{
    public partial class TradeBookView : UserControl
    {
        IContextMenuHelper ContextMenuHelper;
        GridColumn CurrentColumn;
        Point MousePositon;

        public TradeBookView()
        {
            InitializeComponent();
            ContextMenuHelper = AppServiceProvider.GetServiceProvider().GetRequiredService<IContextMenuHelper>();
            ContextMenuHelper.dataGrid = dataGrid;
            watchListView.ContextMenu = ContextMenuHelper.GetContextMenu();
        }

        #region Mouse Click Events
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
        #endregion

        private void PopulateCheckedListBox()
        {
            if (CurrentColumn != null)
            {
                var itemsSource = (ObservableCollection<TradeBook>)dataGrid.ItemsSource;
                var columnName = CurrentColumn.FieldName;
                var values = new HashSet<object>();

                foreach (var item in itemsSource)
                {
                    var property = item.GetType().GetProperty(columnName);
                    if (property != null)
                    {
                        var value = property.GetValue(item);
                        values.Add(value);
                    }
                }
                FilterCheckList.ItemsSource = values.ToList();
            }
        }

        private void ShowFilterPopup()
        {
            filterPopup.Placement = PlacementMode.AbsolutePoint;
            filterPopup.HorizontalOffset = MousePositon.X;
            filterPopup.VerticalOffset = MousePositon.Y;
            filterPopup.IsOpen = true;
        }

        private void FilterSelectionChanged(object sender, RoutedEventArgs e)
        {
            //Gets selected item in checklist
            var selectedItems = FilterCheckList.EditValue as IList<string>;
            ApplyFilter(selectedItems);
        }

        private void ApplyFilter(IList<string> items)
        {
            //Apply Filter in Grid
        }


    }
}
