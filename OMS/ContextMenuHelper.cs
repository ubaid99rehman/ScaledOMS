using DevExpress.Data;
using DevExpress.Xpf.Grid;
using System.Windows;
using System.Windows.Controls;

namespace OMS
{
    public class ContextMenuHelper : IContextMenuHelper
    {
        public GridControl dataGrid { get; set; }
        public GridColumn CurrentColumn {  get; set; }
        public ContextMenu contextMenu { get; set; }

        public ContextMenuHelper()
        {
        }

        public ContextMenu CreateContextMenu()
        {
            contextMenu = new ContextMenu();

            //Sorting
            MenuItem sortAsc = new MenuItem { Name = "SortAsc", Header = "Sort Ascending" };
            sortAsc.Click += SortAscending_Click;
            MenuItem sortDesc = new MenuItem { Name = "SortDesc", Header = "Sort Descending" };
            sortDesc.Click += SortDescending_Click;
            MenuItem sortAscAll = new MenuItem { Name = "SortAscAll", Header = "Sort All Columns Ascending" };
            sortAscAll.Click += SortAllColumnsAscending_Click;
            MenuItem sortDescAll = new MenuItem { Name = "SortDescAll", Header = "Sort All Columns Descending" };
            sortDescAll.Click += SortAllColumnsDescending_Click;
            MenuItem clearSort = new MenuItem { Name = "ClearSort", Header = "Clear Sorting", IsEnabled = false };
            clearSort.Click += ClearSorting_Click;
            //Adding Sorting
            contextMenu.Items.Add(sortAsc);
            contextMenu.Items.Add(sortDesc);
            contextMenu.Items.Add(sortAscAll);
            contextMenu.Items.Add(sortDescAll);
            contextMenu.Items.Add(clearSort);

            //Alignment
            MenuItem alignLeft = new MenuItem { Header = "Align Left" };
            alignLeft.Click += AlignLeft_Click;
            MenuItem alignCenter = new MenuItem { Header = "Align Center" };
            alignCenter.Click += AlignCenter_Click;
            MenuItem alignRight = new MenuItem { Header = "Align Right" };
            alignRight.Click += AlignRight_Click;
            MenuItem alignAllColumnsLeft = new MenuItem { Header = "Align All Columns Left" };
            alignAllColumnsLeft.Click += AlignAllColumnsLeft_Click;
            MenuItem alignAllColumnsCenter = new MenuItem { Header = "Align All Columns Center" };
            alignAllColumnsCenter.Click += AlignAllColumnsCenter_Click;
            MenuItem alignAllColumnsRight = new MenuItem { Header = "Align All Columns Right" };
            alignAllColumnsRight.Click += AlignAllColumnsRight_Click;
            //Adding Alignment
            contextMenu.Items.Add(new Separator());
            contextMenu.Items.Add(alignLeft);
            contextMenu.Items.Add(alignCenter);
            contextMenu.Items.Add(alignRight);
            contextMenu.Items.Add(new Separator());
            contextMenu.Items.Add(alignAllColumnsLeft);
            contextMenu.Items.Add(alignAllColumnsCenter);
            contextMenu.Items.Add(alignAllColumnsRight);

            //Filter Menu
            MenuItem filterColumn = new MenuItem { Name = "FilterMenu", Header = "Filter Column" };
            filterColumn.Click += FilterColumn_Click;
            MenuItem clearFilter = new MenuItem { Name = "ClearFilterMenu", Header = "Clear Filter", IsEnabled = false };
            clearFilter.Click += ClearFilter_Click;
            //Adding Filter
            contextMenu.Items.Add(filterColumn);
            contextMenu.Items.Add(clearFilter);

            return contextMenu;
        }

        #region Sorting 
        private void SortAscending_Click(object sender, RoutedEventArgs e)
        {
            dataGrid.SortBy(CurrentColumn, ColumnSortOrder.Ascending);
            UpdateSortMenuStates(true, true, true, true, true);
        }

        private void SortDescending_Click(object sender, RoutedEventArgs e)
        {
            dataGrid.SortBy(CurrentColumn, ColumnSortOrder.Descending);
            UpdateSortMenuStates(true, true, true, true, true);
        }

        private void SortAllColumnsAscending_Click(object sender, RoutedEventArgs e)
        {
            foreach (var column in dataGrid.Columns)
            {
                dataGrid.SortBy(column, ColumnSortOrder.Ascending);
            }
            UpdateSortMenuStates(false, true, false, true, true);
        }

        private void SortAllColumnsDescending_Click(object sender, RoutedEventArgs e)
        {
            foreach (var column in dataGrid.Columns)
            {
                dataGrid.SortBy(column, ColumnSortOrder.Descending);
            }
            UpdateSortMenuStates(true, false, true, false, true);
        }

        private void ClearSorting_Click(object sender, RoutedEventArgs e)
        {
            foreach (var column in dataGrid.Columns)
            {
                dataGrid.SortBy(column, ColumnSortOrder.None);
            }
            UpdateSortMenuStates(true, true, true, true, false);
        }

        private void UpdateSortMenuStates(bool sortAsc, bool sortDesc, bool sortAscAll, bool sortDescAll, bool clearSort)
        {
            foreach (var item in (contextMenu).Items)
            {
                if (item is MenuItem menuItem)
                {
                    switch (menuItem.Name)
                    {
                        case "SortAsc":
                            menuItem.IsEnabled = sortAsc;
                            break;
                        case "SortDesc":
                            menuItem.IsEnabled = sortDesc;
                            break;
                        case "SortAscAll":
                            menuItem.IsEnabled = sortAscAll;
                            break;
                        case "SortDescAll":
                            menuItem.IsEnabled = sortDescAll;
                            break;
                        case "ClearSort":
                            menuItem.IsEnabled = clearSort;
                            break;
                    }
                }
            }
        }
        #endregion

        #region Alignment
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

        #region Filter Events
        private void FilterColumn_Click(object sender, RoutedEventArgs e)
        {
            ((TableView)dataGrid.View).ShowAutoFilterRow = true;

            UpdateFilterMenuStates(false, true);
        }

        private void ClearFilter_Click(object sender, RoutedEventArgs e)
        {
            foreach (var col in dataGrid.Columns)
            {
                dataGrid.ClearColumnFilter(col.FieldName);
            }
            UpdateFilterMenuStates(true, false);
            ((TableView)dataGrid.View).ShowAutoFilterRow = false;
        }

        private void UpdateFilterMenuStates(bool filterMenuEnabled, bool clearFilterMenuEnabled)
        {
            foreach (var item in contextMenu.Items)
            {
                if (item is MenuItem menuItem)
                {
                    switch (menuItem.Name)
                    {
                        case "FilterMenu":
                            menuItem.IsEnabled = filterMenuEnabled;
                            break;
                        case "ClearFilterMenu":
                            menuItem.IsEnabled = clearFilterMenuEnabled;
                            break;
                    }
                }
            }
        }
        #endregion

    }
}
