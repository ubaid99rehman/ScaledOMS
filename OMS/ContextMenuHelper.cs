using DevExpress.Data;
using DevExpress.Images;
using DevExpress.Mvvm;
using DevExpress.Utils.Design;
using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Grid;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TreeView;

namespace OMS
{
    public class ContextMenuHelper :  ViewModelBase, IContextMenuHelper
    {
        public GridControl dataGrid { get; set; }
        public GridColumn CurrentColumn {  get; set; }
        public ContextMenu contextMenu { get; set; }
        
        private ObservableCollection<BarItem> _menuItems;
        public ObservableCollection<BarItem> MenuItems
        {
            get => _menuItems;
            set
            {
                SetProperty(ref _menuItems, value, nameof(MenuItems));
            }
        }

        bool isSearchEnabled;
        bool isSortingEnabled;
        bool isFilterEnabled;

        public ContextMenuHelper()
        {
            isSearchEnabled = false;
        }

        public ContextMenu GetContextMenu()
        {
            if(contextMenu is null)
            {
                return CreateContextMenu();
            }
            return contextMenu;
        }

        public ObservableCollection<BarItem> GetMenuItems()
        {
            if(MenuItems is null || MenuItems.Count <=0)
            {
                return CreateMenuItems();
            }
            return MenuItems;
        }

        public ContextMenu CreateContextMenu()
        {
            contextMenu = new ContextMenu();

            //Alignment
            MenuItem alignLeft = new MenuItem { Header = "Align Left" };
            alignLeft.Click += AlignLeft_Clicked;
            MenuItem alignCenter = new MenuItem { Header = "Align Center" };
            alignCenter.Click += AlignCenter_Clicked;
            MenuItem alignRight = new MenuItem { Header = "Align Right" };
            alignRight.Click += AlignRight_Clicked;
            MenuItem alignAllColumnsLeft = new MenuItem { Header = "Align All Columns Left" };
            alignAllColumnsLeft.Click += AlignAllColumnsLeft_Clicked;
            MenuItem alignAllColumnsCenter = new MenuItem { Header = "Align All Columns Center" };
            alignAllColumnsCenter.Click += AlignAllColumnsCenter_Clicked;
            MenuItem alignAllColumnsRight = new MenuItem { Header = "Align All Columns Right" };
            alignAllColumnsRight.Click += AlignAllColumnsRight_Clicked;
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
            filterColumn.Click += FilterColumn_Clicked;
            MenuItem clearFilter = new MenuItem { Name = "ClearFilterMenu", Header = "Clear Filter"};
            clearFilter.Click += ClearFilter_Clicked;
            contextMenu.Items.Add(filterColumn);
            contextMenu.Items.Add(clearFilter);

            //Grouping
            var clearGrouping = new BarButtonItem
            {
                Content = "Clear Grouping",
                Glyph = DXImageHelper.GetImageSource(DXImages.Clear, ImageSize.Size16x16),
            };
            clearGrouping.ItemClick += ClearGrouping_ItemClick;
            contextMenu.Items.Add(clearGrouping);

            //Sorting
            MenuItem sortAsc = new MenuItem { Name = "SortAsc", Header = "Sort Ascending" };
            sortAsc.Click += SortAscending_Clicked;
            MenuItem sortDesc = new MenuItem { Name = "SortDesc", Header = "Sort Descending" };
            sortDesc.Click += SortDescending_Clicked;
            MenuItem sortAscAll = new MenuItem { Name = "SortAscAll", Header = "Sort All Columns Ascending" };
            sortAscAll.Click += SortAllColumnsAscending_Clicked;
            MenuItem sortDescAll = new MenuItem { Name = "SortDescAll", Header = "Sort All Columns Descending" };
            sortDescAll.Click += SortAllColumnsDescending_Clicked;
            MenuItem clearSort = new MenuItem { Name = "ClearSort", Header = "Clear Sorting" };
            clearSort.Click += ClearSorting_Clicked;
            contextMenu.Items.Add(sortAsc);
            contextMenu.Items.Add(sortDesc);
            contextMenu.Items.Add(sortAscAll);
            contextMenu.Items.Add(sortDescAll);
            contextMenu.Items.Add(clearSort);

            //Search Panel
            var searchPanel = new BarButtonItem
            {
                Content = "Search Panel",
                Glyph = DXImageHelper.GetImageSource(DXImages.Find, ImageSize.Size16x16),
            };
            searchPanel.ItemClick += SearchPanel_Clicked;
            contextMenu.Items.Add(searchPanel);

            return contextMenu;
        }

        public ObservableCollection<BarItem> CreateMenuItems()
        {
            MenuItems = new ObservableCollection<BarItem>();

            // Sorting Asc Items
            var sortAscMenu = new BarSubItem
            {
                Content = "Sort Ascending",
                Glyph = DXImageHelper.GetImageSource(DXImages.SortAsc, ImageSize.Size16x16)
            };
            BarButtonItem sortAsc = new BarButtonItem { Content = "Sort Ascending" };
            sortAsc.ItemClick += SortAscending_Clicked;
            BarButtonItem sortAscAll = new BarButtonItem { Content = "Sort All Columns Ascending" };
            sortAscAll.ItemClick += SortAllColumnsAscending_Clicked;
            sortAscMenu.Items.Add(sortAsc);
            sortAscMenu.Items.Add(sortAscAll);

            //Sort Desc Items
            var sortDescMenu = new BarSubItem
            {
                Content = "Sort Descending",
                Glyph = DXImageHelper.GetImageSource(DXImages.SortDesc, ImageSize.Size16x16)
            };
            BarButtonItem sortDesc = new BarButtonItem { Content = "Sort Descending" };
            sortDesc.ItemClick += SortDescending_Clicked;
            BarButtonItem sortDescAll = new BarButtonItem { Content = "Sort All Columns Descending" };
            sortDescAll.ItemClick += SortAllColumnsDescending_Clicked;
            sortDescMenu.Items.Add(sortDesc);
            sortDescMenu.Items.Add(sortDescAll);

            BarButtonItem clearSort = new BarButtonItem { 
                Content = "Clear Sorting",
                Glyph = DXImageHelper.GetImageSource(DXImages.Clear, ImageSize.Size16x16),
                IsEnabled = false
            };
            clearSort.ItemClick += ClearSorting_Clicked;

            // Add Sorting Items to Collection
            MenuItems.Add(sortAscMenu);
            MenuItems.Add(sortDescMenu);
            MenuItems.Add(clearSort);

            // Search Panel
            BarButtonItem searchPanel = new BarButtonItem { 
                Content = "Search Panel",
                Glyph = DXImageHelper.GetImageSource(DXImages.Find, ImageSize.Size16x16)
            };
            searchPanel.ItemClick += SearchPanel_Clicked;
            MenuItems.Add(new BarItemSeparator());
            MenuItems.Add(searchPanel);

            // Alignment Left Items
            var alignLeftMenu = new BarSubItem
            {
                Content = "Align Left",
                Glyph = DXImageHelper.GetImageSource(DXImages.AlignVerticalLeft, ImageSize.Size16x16)
            };
            BarButtonItem alignLeft = new BarButtonItem { Content = "Align Left" };
            alignLeft.ItemClick += AlignLeft_Clicked;
            BarButtonItem alignAllLeft = new BarButtonItem { Content = "Align All Columns Left" };
            alignAllLeft.ItemClick += AlignAllColumnsLeft_Clicked;
            alignLeftMenu.Items.Add(alignLeft);
            alignLeftMenu.Items.Add(alignAllLeft);

            // Alignment Center Items
            var alignCenterMenu = new BarSubItem
            {
                Content = "Align Center",
                Glyph = DXImageHelper.GetImageSource(DXImages.AlignVerticalCenter, ImageSize.Size16x16)
            };
            BarButtonItem alignCenter = new BarButtonItem { Content = "Align Center" };
            alignCenter.ItemClick += AlignCenter_Clicked;
            BarButtonItem alignAllCenter = new BarButtonItem { Content = "Align All Columns Center" };
            alignAllCenter.ItemClick += AlignAllColumnsCenter_Clicked;
            alignCenterMenu.Items.Add(alignCenter);
            alignCenterMenu.Items.Add(alignAllCenter);

            // Alignment Right Items
            var alignRightMenu = new BarSubItem
            {
                Content = "Align Right",
                Glyph = DXImageHelper.GetImageSource(DXImages.AlignVerticalRight, ImageSize.Size16x16)
            };
            BarButtonItem alignRight = new BarButtonItem { Content = "Align Right" };
            alignRight.ItemClick += AlignRight_Clicked;
            BarButtonItem alignAllRight = new BarButtonItem { Content = "Align All Columns Right" };
            alignAllRight.ItemClick += AlignAllColumnsRight_Clicked;
            alignRightMenu.Items.Add(alignRight);
            alignRightMenu.Items.Add(alignAllRight);

            // Add Alignment Items to Collection
            MenuItems.Add(new BarItemSeparator());
            MenuItems.Add(alignLeftMenu);
            MenuItems.Add(alignCenterMenu);
            MenuItems.Add(alignRightMenu);

            // Filter Menu Items
            BarButtonItem filterColumn = new BarButtonItem { 
                Content = "Filter Column",
                Glyph = DXImageHelper.GetImageSource(DXImages.Filter, ImageSize.Size16x16)
            };
            filterColumn.ItemClick += FilterColumn_Clicked;

            BarButtonItem clearFilter = new BarButtonItem { 
                Content = "Clear Filter",
                Glyph = DXImageHelper.GetImageSource(DXImages.Cancel, ImageSize.Size16x16),
                IsEnabled = false
            };
            clearFilter.ItemClick += ClearFilter_Clicked;

            // Add Filter Items to Collection
            MenuItems.Add(new BarItemSeparator());
            MenuItems.Add(filterColumn);
            MenuItems.Add(clearFilter);

            return MenuItems;
        }
        
        private void EnableClearSort()
        {
            if(!isSortingEnabled)
            {
                var menuItem = MenuItems.Where(item => item.Content == "Clear Sorting").FirstOrDefault();
                if (menuItem != null)
                {
                    menuItem.IsEnabled = true;
                isSortingEnabled = !isSortingEnabled;
                }
            }
        }

        private void DisableClearSort()
        {
            if (isSortingEnabled)
            {
                var menuItem = MenuItems.Where(item => item.Content == "Clear Sorting").FirstOrDefault();
                if (menuItem != null)
                {
                    menuItem.IsEnabled = false;
                    isSortingEnabled = !isSortingEnabled;
                }
            }
        }

        #region Alignment Events
        private void AlignLeft_Clicked(object sender, RoutedEventArgs e)
        {
            CurrentColumn.HorizontalHeaderContentAlignment = HorizontalAlignment.Left;
            CurrentColumn.ActualEditSettings.HorizontalContentAlignment = (DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment)HorizontalAlignment.Left;
        }

        private void AlignCenter_Clicked(object sender, RoutedEventArgs e)
        {
            CurrentColumn.HorizontalHeaderContentAlignment = HorizontalAlignment.Center;
            CurrentColumn.ActualEditSettings.HorizontalContentAlignment = (DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment)HorizontalAlignment.Center;
        }

        private void AlignRight_Clicked(object sender, RoutedEventArgs e)
        {
            CurrentColumn.HorizontalHeaderContentAlignment = HorizontalAlignment.Right;
            CurrentColumn.ActualEditSettings.HorizontalContentAlignment = (DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment)HorizontalAlignment.Right;
        }

        private void AlignAllColumnsLeft_Clicked(object sender, RoutedEventArgs e)
        {
            foreach (var column in dataGrid.Columns)
            {
                column.HorizontalHeaderContentAlignment = HorizontalAlignment.Left;
                column.ActualEditSettings.HorizontalContentAlignment = (DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment)HorizontalAlignment.Left;
            }
        }

        private void AlignAllColumnsCenter_Clicked(object sender, RoutedEventArgs e)
        {
            foreach (var column in dataGrid.Columns)
            {
                column.HorizontalHeaderContentAlignment = HorizontalAlignment.Center;
                column.ActualEditSettings.HorizontalContentAlignment = (DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment)HorizontalAlignment.Center;
            }
        }

        private void AlignAllColumnsRight_Clicked(object sender, RoutedEventArgs e)
        {
            foreach (var column in dataGrid.Columns)
            {
                column.HorizontalHeaderContentAlignment = HorizontalAlignment.Right;
                column.ActualEditSettings.HorizontalContentAlignment = (DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment)HorizontalAlignment.Right;
            }
        }
        #endregion

        #region Filter Events
        private void FilterColumn_Clicked(object sender, RoutedEventArgs e)
        {
            if(!isFilterEnabled)
            {
                ((TableView)dataGrid.View).ShowAutoFilterRow = true;
                isFilterEnabled = !isFilterEnabled;
                var menuItem = MenuItems.Where(item => item.Content == "Clear Filter").FirstOrDefault();
                if (menuItem != null)
                {
                    menuItem.IsEnabled = true;
                }
            }
        }

        private void ClearFilter_Clicked(object sender, RoutedEventArgs e)
        {
            if (isFilterEnabled)
            {
                isFilterEnabled = !isFilterEnabled;
                foreach (var col in dataGrid.Columns)
                {
                    dataGrid.ClearColumnFilter(col.FieldName);
                }
                ((TableView)dataGrid.View).ShowAutoFilterRow = false;
                var menuItem = MenuItems.Where(item => item.Content == "Clear Filter").FirstOrDefault();
                if (menuItem != null)
                {
                    menuItem.IsEnabled = false;
                }
            }
        }
        #endregion
        
        #region Grouping Event

        private void ClearGrouping_ItemClick(object sender, ItemClickEventArgs e)
        {
            dataGrid.ClearGrouping();
        }

        #endregion

        #region Search Event
        private void SearchPanel_Clicked(object sender, RoutedEventArgs e)
        {
            if (isSearchEnabled)
            {
                dataGrid.View.SearchPanelClearOnClose= true;
                dataGrid.View.HideSearchPanel();
                isSearchEnabled = !isSearchEnabled;
                var menuItem = MenuItems.Where(item => item.Content == "Close Search Panel").FirstOrDefault();
                if(menuItem != null)
                {
                    menuItem.Content = "Search Panel";
                }
            }
            else
            {
                dataGrid.View.ShowSearchPanel(true);
                isSearchEnabled = !isSearchEnabled;
                var menuItem = MenuItems.Where(item => item.Content == "Search Panel").FirstOrDefault();
                
                if (menuItem != null)
                {
                    menuItem.Content = "Close Search Panel";
                }
            }
        } 
        #endregion

        #region Sorting Events
        private void SortAscending_Clicked(object sender, RoutedEventArgs e)
        {
            dataGrid.SortBy(CurrentColumn, ColumnSortOrder.Ascending);
        }

        private void SortDescending_Clicked(object sender, RoutedEventArgs e)
        {
            dataGrid.SortBy(CurrentColumn, ColumnSortOrder.Descending);
        }

        private void SortAllColumnsAscending_Clicked(object sender, RoutedEventArgs e)
        {
            foreach (var column in dataGrid.Columns)
            {
                dataGrid.SortBy(column, ColumnSortOrder.Ascending);
            }
        }

        private void SortAllColumnsDescending_Clicked(object sender, RoutedEventArgs e)
        {
            foreach (var column in dataGrid.Columns)
            {
                dataGrid.SortBy(column, ColumnSortOrder.Descending);
            }
        }

        private void ClearSorting_Clicked(object sender, RoutedEventArgs e)
        {
            foreach (var column in dataGrid.Columns)
            {
                dataGrid.SortBy(column, ColumnSortOrder.None);
            }
        }

        #endregion

        #region Mouse Clicked Events
        public void Mouse_Right_Button_Clicked(object sender, MouseButtonEventArgs e)
        {
            var hitInfo = ((TableView)dataGrid.View).CalcHitInfo(Mouse.GetPosition(dataGrid));
            if (hitInfo != null)
            {
                CurrentColumn = hitInfo.Column;
            }
        }
        #endregion
    }
}
