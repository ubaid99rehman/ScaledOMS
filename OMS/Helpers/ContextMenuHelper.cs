using DevExpress.Data;
using DevExpress.Images;
using DevExpress.Utils.Design;
using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Grid;
using OMS.Core.Models;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace OMS
{
    public class ContextMenuHelper : BaseModel, IContextMenuHelper
    {
        //Enable Actions
        bool isSearchEnabled;
        bool isFilterEnabled;

        //Custom Grid Context Menu Items
        public ContextMenu contextMenu { get; set; }
        public GridColumn CurrentColumn {  get; set; }
        public GridControl dataGrid { get; set; }

        //private Menu Items Members
        private ObservableCollection<BarItem> _allMenuItems;
        private ObservableCollection<BarItem> _sortingMenuItems;
        private ObservableCollection<BarItem> _alignMenuItems;
        private ObservableCollection<BarItem> _searchFilterMenuItems;
        //public Menu Items Members
        public ObservableCollection<BarItem> AllMenuItems
        {
            get => _allMenuItems;
            set
            {
                SetProperty(ref _allMenuItems, value, nameof(AllMenuItems));
            }
        }
        public ObservableCollection<BarItem> SortingMenuItems
        {
            get => _sortingMenuItems;
            set
            {
                SetProperty(ref _sortingMenuItems, value, nameof(SortingMenuItems));
            }
        }
        public ObservableCollection<BarItem> SearchFilterMenuItems
        {
            get => _searchFilterMenuItems;
            set
            {
                SetProperty(ref _searchFilterMenuItems, value, nameof(SearchFilterMenuItems));
            }
        }
        public ObservableCollection<BarItem> AlignMenuItems
        {
            get => _alignMenuItems;
            set
            {
                SetProperty(ref _alignMenuItems, value, nameof(AlignMenuItems));
            }
        }

        //Constructor
        public ContextMenuHelper()
        {
            isSearchEnabled = false;
            isFilterEnabled = false;
        }

        //ContextMenu Methods
        public ObservableCollection<BarItem> GetAllMenuItems()
        {
            if(AllMenuItems is null || AllMenuItems.Count <=0)
            {
                return CreateAllMenuItems();
            }
            return AllMenuItems;
        }
        public ObservableCollection<BarItem> GetSortingMenuItems()
        {
            if (SortingMenuItems is null || SortingMenuItems.Count <= 0)
            {
                return CreateSortingMenuItems();
            }
            return SortingMenuItems;
        }
        public ObservableCollection<BarItem> GetAlignmentMenuItems()
        {
            if (AlignMenuItems is null || AlignMenuItems.Count <= 0)
            {
                return CreateAlignmentMenuItems();
            }
            return AlignMenuItems;
        }
        public ObservableCollection<BarItem> GetSearchAndFilterMenuItems()
        {
            if (SearchFilterMenuItems is null || SearchFilterMenuItems.Count <= 0)
            {
                return CreateSearchAndFilterMenuItems();
            }
            return SearchFilterMenuItems;
        }
        public ContextMenu GetContextMenu(bool sorting, bool align, bool grouping, bool search, bool filter)
        {
            if(contextMenu is null)
            {
                return CreateContextMenu(sorting,align,grouping,search,filter);
            }
            return contextMenu;
        }
        public void Mouse_Right_Button_Clicked(object sender, MouseButtonEventArgs e)
        {
            var hitInfo = ((TableView)dataGrid.View).CalcHitInfo(Mouse.GetPosition(dataGrid));
            if (hitInfo != null)
            {
                CurrentColumn = hitInfo.Column;
            }
        }
        
        //Context Menu Action Items Creation
        public ObservableCollection<BarItem> CreateAllMenuItems()
        {
            AllMenuItems = new ObservableCollection<BarItem>();
            // Add Sorting Menu Items
            var sortingMenuItems = CreateSortingMenuItems();
            foreach (var item in sortingMenuItems)
            {
                AllMenuItems.Add(item);
            }
            // Add Search and Filter Menu Items
            var searchAndFilterMenuItems = CreateSearchAndFilterMenuItems();
            foreach (var item in searchAndFilterMenuItems)
            {
                AllMenuItems.Add(item);
            }
            // Add Alignment Menu Items
            var alignmentMenuItems = CreateAlignmentMenuItems();
            foreach (var item in alignmentMenuItems)
            {
                AllMenuItems.Add(item);
            }

            return AllMenuItems;
        }
        public ObservableCollection<BarItem> CreateSortingMenuItems()
        {
            var sortingMenuItems = new ObservableCollection<BarItem>();
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
            // Sort Desc Items
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
            BarButtonItem clearSort = new BarButtonItem
            {
                Content = "Clear Sorting",
                Glyph = DXImageHelper.GetImageSource(DXImages.Clear, ImageSize.Size16x16),
                IsEnabled = false
            };
            clearSort.ItemClick += ClearSorting_Clicked;
            // Add Sorting Items to Collection
            sortingMenuItems.Add(sortAscMenu);
            sortingMenuItems.Add(sortDescMenu);
            sortingMenuItems.Add(clearSort);

            return sortingMenuItems;
        }
        public ObservableCollection<BarItem> CreateAlignmentMenuItems()
        {
            var alignmentMenuItems = new ObservableCollection<BarItem>();
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
            alignmentMenuItems.Add(new BarItemSeparator());
            alignmentMenuItems.Add(alignLeftMenu);
            alignmentMenuItems.Add(alignCenterMenu);
            alignmentMenuItems.Add(alignRightMenu);

            return alignmentMenuItems;
        }
        public ObservableCollection<BarItem> CreateSearchAndFilterMenuItems()
        {
            var searchAndFilterMenuItems = new ObservableCollection<BarItem>();
            // Search Panel
            BarButtonItem searchPanel = new BarButtonItem
            {
                Content = "Search Panel",
                Glyph = DXImageHelper.GetImageSource(DXImages.Find, ImageSize.Size16x16)
            };
            searchPanel.ItemClick += SearchPanel_Clicked;
            BarButtonItem filterColumn = new BarButtonItem
            {
                Content = "Filter Column",
                Glyph = DXImageHelper.GetImageSource(DXImages.Filter, ImageSize.Size16x16)
            };
            filterColumn.ItemClick += FilterColumn_Clicked;
            BarButtonItem clearFilter = new BarButtonItem
            {
                Content = "Clear Filter",
                Glyph = DXImageHelper.GetImageSource(DXImages.Cancel, ImageSize.Size16x16),
                IsEnabled = false
            };
            clearFilter.ItemClick += ClearFilter_Clicked;
            // Add Search and Filter Items to Collection
            searchAndFilterMenuItems.Add(new BarItemSeparator());
            searchAndFilterMenuItems.Add(searchPanel);
            searchAndFilterMenuItems.Add(filterColumn);
            searchAndFilterMenuItems.Add(clearFilter);
            return searchAndFilterMenuItems;
        }

        //Custom Context Menu Creation
        public ContextMenu CreateContextMenu(bool sorting, bool align, bool grouping, bool search, bool filter)
        {
            contextMenu = new ContextMenu();
            if (align)
            {
                AddAlignmentMenuItems();
            }
            if (search)
            {
                AddSearchMenuItem();
            }
            if (filter)
            {
                AddFilterMenuItem();
            }
            if (grouping)
            {
                AddGroupingMenuItem();
            }
            if (sorting)
            {
                AddSortingMenuItems();

            }
            
            return contextMenu;
        }
        private void AddSearchMenuItem()
        {
            var searchPanel = new BarButtonItem
            {
                Content = "Search Panel",
                Glyph = DXImageHelper.GetImageSource(DXImages.Find, ImageSize.Size16x16),
            };
            searchPanel.ItemClick += SearchPanel_Clicked;
            contextMenu.Items.Add(searchPanel);
        }
        private void AddSortingMenuItems()
        {
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
        }
        private void AddGroupingMenuItem()
        {
            var clearGrouping = new BarButtonItem
            {
                Content = "Clear Grouping",
                Glyph = DXImageHelper.GetImageSource(DXImages.Clear, ImageSize.Size16x16),
            };
            clearGrouping.ItemClick += ClearGrouping_ItemClick;
            contextMenu.Items.Add(clearGrouping);
        }
        private void AddFilterMenuItem()
        {
            MenuItem filterColumn = new MenuItem { Name = "FilterMenu", Header = "Filter Column" };
            filterColumn.Click += FilterColumn_Clicked;
            MenuItem clearFilter = new MenuItem { Name = "ClearFilterMenu", Header = "Clear Filter" };
            clearFilter.Click += ClearFilter_Clicked;
            contextMenu.Items.Add(filterColumn);
            contextMenu.Items.Add(clearFilter);
        }
        private void AddAlignmentMenuItems()
        {
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
                var menuItem = SearchFilterMenuItems.Where(item => item.Content == "Clear Filter").FirstOrDefault();
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
                var menuItem = SearchFilterMenuItems.Where(item => item.Content == "Clear Filter").FirstOrDefault();
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
                var menuItem = SearchFilterMenuItems.Where(item => item.Content == "Close Search Panel").FirstOrDefault();
                if(menuItem != null)
                {
                    menuItem.Content = "Search Panel";
                }
            }
            else
            {
                dataGrid.View.ShowSearchPanel(true);
                isSearchEnabled = !isSearchEnabled;
                var menuItem = SearchFilterMenuItems.Where(item => item.Content == "Search Panel").FirstOrDefault();
                
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
    }
}
