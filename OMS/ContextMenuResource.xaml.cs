using DevExpress.Data;
using DevExpress.Xpf.Grid;
using System.Windows;
using System.Windows.Controls;

namespace OMS
{
    public partial class ContextMenuResource 
    {
        GridColumn CurrentColumn;
        GridControl dataGrid;

        #region Alignment Events
        #region Horizontal Alignment
        private void AlignLeft_Click(object sender, RoutedEventArgs e)
        {
            CurrentColumn.HorizontalHeaderContentAlignment = HorizontalAlignment.Left;
            CurrentColumn.ActualEditSettings.HorizontalContentAlignment = (DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment)HorizontalAlignment.Left;
        }

        private void AlignCenter_Click(object sender, RoutedEventArgs e)
        {
            CurrentColumn.HorizontalHeaderContentAlignment = HorizontalAlignment.Center;
            CurrentColumn.ActualEditSettings.HorizontalContentAlignment = (DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment)HorizontalAlignment.Center;
        }

        private void AlignRight_Click(object sender, RoutedEventArgs e)
        {
            CurrentColumn.HorizontalHeaderContentAlignment = HorizontalAlignment.Right;
            CurrentColumn.ActualEditSettings.HorizontalContentAlignment = (DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment)HorizontalAlignment.Right;
        }

        private void AlignAllColumnsLeft_Click(object sender, RoutedEventArgs e)
        {
            foreach (var column in dataGrid.Columns)
            {
                column.HorizontalHeaderContentAlignment = HorizontalAlignment.Left;
                column.ActualEditSettings.HorizontalContentAlignment = (DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment)HorizontalAlignment.Left;
            }
        }

        private void AlignAllColumnsCenter_Click(object sender, RoutedEventArgs e)
        {
            foreach (var column in dataGrid.Columns)
            {
                column.HorizontalHeaderContentAlignment = HorizontalAlignment.Center;
                column.ActualEditSettings.HorizontalContentAlignment = (DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment)HorizontalAlignment.Center;
            }
        }

        private void AlignAllColumnsRight_Click(object sender, RoutedEventArgs e)
        {
            foreach (var column in dataGrid.Columns)
            {
                column.HorizontalHeaderContentAlignment = HorizontalAlignment.Right;
                column.ActualEditSettings.HorizontalContentAlignment = (DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment)HorizontalAlignment.Right;
            }
        }
        #endregion

        #region Vertical Alignment
        private void AlignTop_Click(object sender, RoutedEventArgs e)
        {
            CurrentColumn.ActualEditSettings.VerticalContentAlignment = VerticalAlignment.Top;
        }

        private void AlignVerticalCenter_Click(object sender, RoutedEventArgs e)
        {
            CurrentColumn.ActualEditSettings.VerticalContentAlignment = VerticalAlignment.Center;
        }

        private void AlignBottom_Click(object sender, RoutedEventArgs e)
        {
            CurrentColumn.ActualEditSettings.VerticalContentAlignment = VerticalAlignment.Bottom;
        }

        private void AlignAllColumnsTop_Click(object sender, RoutedEventArgs e)
        {
            foreach (var column in dataGrid.Columns)
            {
                column.ActualEditSettings.VerticalContentAlignment = VerticalAlignment.Top;
            }
        }

        private void AlignAllColumnsVerticalCenter_Click(object sender, RoutedEventArgs e)
        {
            foreach (var column in dataGrid.Columns)
            {
                column.ActualEditSettings.VerticalContentAlignment = VerticalAlignment.Center;
            }
        }

        private void AlignAllColumnsBottom_Click(object sender, RoutedEventArgs e)
        {
            foreach (var column in dataGrid.Columns)
            {
                column.ActualEditSettings.VerticalContentAlignment = VerticalAlignment.Bottom;
            }
        }
        #endregion
        #endregion

        #region Sort Events
        private void SortAscending_Click(object sender, RoutedEventArgs e)
        {
            dataGrid.SortBy(CurrentColumn, ColumnSortOrder.Ascending);
            SortAsc.IsEnabled = true;
            SortDesc.IsEnabled = true;
            SortDescAll.IsEnabled = true;
            SortAscAll.IsEnabled = true;
            ClearSort.IsEnabled = true;
        }

        private void SortDescending_Click(object sender, RoutedEventArgs e)
        {
            dataGrid.SortBy(CurrentColumn, ColumnSortOrder.Descending);
            SortAsc.IsEnabled = true;
            SortDesc.IsEnabled = true;
            SortDescAll.IsEnabled = true;
            SortAscAll.IsEnabled = true;
            ClearSort.IsEnabled = true;
        }

        private void SortAllColumnsAscending_Click(object sender, RoutedEventArgs e)
        {
            foreach (var column in dataGrid.Columns)
            {
                dataGrid.SortBy(column, ColumnSortOrder.Ascending);
            }
            SortAsc.IsEnabled = false;
            SortAscAll.IsEnabled = false;
            SortDescAll.IsEnabled = true;
            SortDesc.IsEnabled = true;
            ClearSort.IsEnabled = true;
        }

        private void SortAllColumnsDescending_Click(object sender, RoutedEventArgs e)
        {
            foreach (var column in dataGrid.Columns)
            {
                dataGrid.SortBy(column, ColumnSortOrder.Descending);
            }

            SortDesc.IsEnabled = false;
            SortDescAll.IsEnabled = false;
            SortAsc.IsEnabled = true;
            SortAscAll.IsEnabled = true;
            ClearSort.IsEnabled = true;
        }

        private void ClearSorting_Click(object sender, RoutedEventArgs e)
        {
            foreach (var column in dataGrid.Columns)
            {
                dataGrid.SortBy(column, ColumnSortOrder.None);
            }
            ClearSort.IsEnabled = false;
            //Enable Other Menu's
            SortAsc.IsEnabled = true;
            SortDesc.IsEnabled = true;
            SortDescAll.IsEnabled = true;
            SortAscAll.IsEnabled = true;
        }
        #endregion

        #region Filter Events
        private void FilterColumn_Click(object sender, RoutedEventArgs e)
        {
            //PopulateCheckedListBox();
            //var itemsource = FilterCheckList.ItemsSource;
            //ShowFilterPopup();
            ((TableView)dataGrid.View).ShowAutoFilterRow = true;
            
            FilterMenu.IsEnabled = false;
            ClearFilterMenu.IsEnabled = true;
        }

        private void ClearFilter_Click(object sender, RoutedEventArgs e)
        {
            foreach (var col in dataGrid.Columns)
            {
                dataGrid.ClearColumnFilter(col.FieldName);
            }
            FilterMenu.IsEnabled = true;
            ClearFilterMenu.IsEnabled = false;
            ((TableView)dataGrid.View).ShowAutoFilterRow = false;
        }
        #endregion
    }
}
