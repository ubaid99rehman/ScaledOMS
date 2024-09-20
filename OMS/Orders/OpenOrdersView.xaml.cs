using DevExpress.Data;
using DevExpress.Images;
using DevExpress.Utils.Design;
using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Grid;
using Microsoft.Extensions.DependencyInjection;
using OMS.ViewModels;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace OMS.Orders
{
    public partial class OpenOrdersView : UserControl
    {
        GridColumn CurrentColumn;

        public OpenOrdersView()
        {
            InitializeComponent();
            this.DataContext = AppServiceProvider.GetServiceProvider().GetRequiredService<OrdersListModel>();
        }

        private void btnCancelOrder_Click(object sender, RoutedEventArgs e)
        {
            if (this.DataContext is OrdersListModel model)
            {
                model.CancelOrder(out bool isCancelled,out string message);

                model.CloseEditForm();
                if(isCancelled)
                {
                    ThemedMessageBox.Show("Order Cancelled Successfully!", "Order Cancelled", MessageBoxButton.OK);
                }
                else
                {
                    ThemedMessageBox.Show(message);
                }
            }
        }

        private void btnUpdateOrder_Click(object sender, RoutedEventArgs e)
        {
            if (this.DataContext is OrdersListModel model)
            {
                model.UpdateOrder(out bool isUpdated, out string message);
                if(isUpdated)
                {
                    model.CloseEditForm();
                    MessageBox.Show("Order Updated Successfully!", "Order Updated", MessageBoxButton.OK);
                }
                else
                {
                    MessageBox.Show("Cannot Update Order!", "Order Update", MessageBoxButton.OK);
                }

            }
        }

        private void Show_EditForm(object sender, MouseButtonEventArgs e)
        {
            if(this.DataContext is OrdersListModel model)
            {
                model.ShowEditForm();
            }
        }

        private void ShowMenu_Click(object sender, DevExpress.Xpf.Grid.GridMenuEventArgs e)
        {
            var eItems = e.Items;

            //Remove Default Sortings
            e.Customizations.Add(new RemoveAction { ElementName = DefaultColumnMenuItemNames.FilterEditor });
            e.Customizations.Add(new RemoveAction { ElementName = DefaultColumnMenuItemNames.ColumnChooser });
            e.Customizations.Add(new RemoveAction { ElementName = DefaultColumnMenuItemNames.SearchPanel });
            e.Customizations.Add(new RemoveAction { ElementName = DefaultColumnMenuItemNames.BestFit });
            e.Customizations.Add(new RemoveAction { ElementName = DefaultColumnMenuItemNames.BestFitColumns });
            e.Customizations.Add(new RemoveAction { ElementName = DefaultColumnMenuItemNamesBase.SortAscending });
            e.Customizations.Add(new RemoveAction { ElementName = DefaultColumnMenuItemNamesBase.SortDescending });
            e.Customizations.Add(new RemoveAction { ElementName = DefaultColumnMenuItemNamesBase.ClearSorting });
            
            //Add New Asc Sort Options
            var sortAscMenu = new BarSubItem 
            {
                Content = "Sort Ascending",
                Glyph = DXImageHelper.GetImageSource(DXImages.SortAsc, ImageSize.Size16x16)
            };
            var sortAsc = new BarButtonItem
            {
                Content = "Selected Column",
                Command= e.Items.Where(item => item.Name == DefaultColumnMenuItemNamesBase.SortAscending).First().Command,
            };
            var sortAscAll = new BarButtonItem
            {
                Content = "All Columns",
            };
            sortAscAll.ItemClick += SortAscAllColumns;
            sortAscMenu.Items.Add(sortAscAll);
            sortAscMenu.Items.Add(sortAsc);

            //Add New Desc Sort Options
            var sortDescMenu = new BarSubItem {
                Content = "Sort Descending",
                Glyph = DXImageHelper.GetImageSource(DXImages.SortDesc, ImageSize.Size16x16)
            };
            var sortDesc = new BarButtonItem
            {
                Content = "Selected Column",
                Command = e.Items.Where(item => item.Name == DefaultColumnMenuItemNamesBase.SortDescending).First().Command,
            };
            var sortDescAll = new BarButtonItem
            {
                Content = "All Columns",
            };
            sortDescAll.ItemClick += SortDescAllColumns;
            sortDescMenu.Items.Add(sortDescAll);
            sortDescMenu.Items.Add(sortDesc);

            //Add Clear Sort
            var clearSort = e.Items.Where(item => item.Name == DefaultColumnMenuItemNamesBase.ClearSorting).First();
           
            //Add Left Align
            var alignLeftMenu = new BarSubItem
            {
                Content = "Align Left",
                Glyph = DXImageHelper.GetImageSource(DXImages.AlignVerticalLeft, ImageSize.Size16x16)
            };
            var alignLeft = new BarButtonItem
            {
                Content = "Selected Column",
            };
            var alignLeftAll = new BarButtonItem
            {
                Content = "All Columns",
            };

            alignLeft.ItemClick += AlignLeft;
            alignLeftAll.ItemClick += AlignLeftAll;
            alignLeftMenu.Items.Add(alignLeft);
            alignLeftMenu.Items.Add(alignLeftAll);

            //Add Center Align
            var alignCenterMenu = new BarSubItem
            {
                Content = "Align Center",
                Glyph = DXImageHelper.GetImageSource(DXImages.AlignVerticalCenter, ImageSize.Size16x16)
            };
            var alignCenter = new BarButtonItem
            {
                Content = "Selected Column",
            };
            var alignCenterAll = new BarButtonItem
            {
                Content = "All COlumns",
            };

            alignCenter.ItemClick += AlignCenter;
            alignCenterAll.ItemClick += AlignCenterAll;
            alignCenterMenu.Items.Add(alignCenter);
            alignCenterMenu.Items.Add(alignCenterAll);

            //Add Right Align
            var alignRightMenu = new BarSubItem
            {
                Content = "Align Right",
                Glyph = DXImageHelper.GetImageSource(DXImages.AlignVerticalRight, ImageSize.Size16x16)
            };
            var alignRight = new BarButtonItem
            {
                Content = "Selected Column",
            };
            var alignRightAll = new BarButtonItem
            {
                Content = "All Columns",
            };

            alignRight.ItemClick += AlignRight;
            alignRightAll.ItemClick += AlignRightAll;
            alignRightMenu.Items.Add(alignRight);
            alignRightMenu.Items.Add(alignRightAll);

            var clearGrouping = new BarButtonItem
            {
                Content = "Clear Grouping",
                Glyph = DXImageHelper.GetImageSource(DXImages.Clear, ImageSize.Size16x16),
            };
            clearGrouping.ItemClick += ClearGrouping_ItemClick;
            var searchPanel = new BarButtonItem
            {
                Content = "Search Panel",
                Glyph = DXImageHelper.GetImageSource(DXImages.Find, ImageSize.Size16x16),
                Command = e.Items.Where(item => item.Name == DefaultColumnMenuItemNamesBase.SearchPanel).First().Command,
            };
            e.Customizations.Add(clearGrouping);
            e.Customizations.Add(searchPanel);
            e.Customizations.Add(sortAscMenu);
            e.Customizations.Add(sortDescMenu);
            e.Customizations.Add(clearSort);
            e.Customizations.Add(new BarItemSeparator());
            e.Customizations.Add(alignRightMenu);
            e.Customizations.Add(alignLeftMenu);
            e.Customizations.Add(alignCenterMenu);
        }

        private void ClearGrouping_ItemClick(object sender, ItemClickEventArgs e)
        {
            dataGrid.ClearGrouping();
        }

        #region Mouse Click Events
        private void Mouse_Right_Button_Clicked(object sender, MouseButtonEventArgs e)
        {
            var hitInfo = ((TableView)dataGrid.View).CalcHitInfo(Mouse.GetPosition(dataGrid));
            if (hitInfo != null)
            {
                CurrentColumn = hitInfo.Column;
            }
        }
        #endregion

        private void AlignLeft(object sender, ItemClickEventArgs e)
        {
            if (CurrentColumn != null)
            {
                CurrentColumn.HorizontalHeaderContentAlignment = HorizontalAlignment.Left;
                CurrentColumn.ActualEditSettings.HorizontalContentAlignment = (DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment)HorizontalAlignment.Left;
            }
        }

        private void AlignRight(object sender, ItemClickEventArgs e)
        {
            if (CurrentColumn != null)
            {
                CurrentColumn.HorizontalHeaderContentAlignment = HorizontalAlignment.Right;
                CurrentColumn.ActualEditSettings.HorizontalContentAlignment = (DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment)HorizontalAlignment.Right;
            }
        }

        private void AlignCenter(object sender, ItemClickEventArgs e)
        {
            if (CurrentColumn != null)
            {
                CurrentColumn.HorizontalHeaderContentAlignment = HorizontalAlignment.Center;
                CurrentColumn.ActualEditSettings.HorizontalContentAlignment = (DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment)HorizontalAlignment.Center;
            }
        }

        private void AlignLeftAll(object sender, ItemClickEventArgs e)
        {
            foreach (var column in dataGrid.Columns)
            {
                column.HorizontalHeaderContentAlignment = HorizontalAlignment.Left;
                column.ActualEditSettings.HorizontalContentAlignment = (DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment)HorizontalAlignment.Left;
            }
        }

        private void AlignCenterAll(object sender, ItemClickEventArgs e)
        {
            foreach (var column in dataGrid.Columns)
            {
                column.HorizontalHeaderContentAlignment = HorizontalAlignment.Center;
                column.ActualEditSettings.HorizontalContentAlignment = (DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment)HorizontalAlignment.Center;
            }
        }

        private void AlignRightAll(object sender, ItemClickEventArgs e)
        {
            foreach (var column in dataGrid.Columns)
            {
                column.HorizontalHeaderContentAlignment = HorizontalAlignment.Right;
                column.ActualEditSettings.HorizontalContentAlignment = (DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment)HorizontalAlignment.Right;
            }
        }

        private void SortAscAllColumns(object sender, ItemClickEventArgs e)
        {
            foreach (var column in dataGrid.Columns)
            {
                dataGrid.SortBy(column, ColumnSortOrder.Ascending);
            }
        }

        private void SortDescAllColumns(object sender, ItemClickEventArgs e)
        {
            foreach (var column in dataGrid.Columns)
            {
                dataGrid.SortBy(column, ColumnSortOrder.Descending);
            }
        }

    }
}
