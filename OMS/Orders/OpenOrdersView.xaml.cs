using DevExpress.Data;
using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Core.Native;
using DevExpress.Xpf.Grid;
using Microsoft.Extensions.DependencyInjection;
using OMS.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace OMS.Orders
{
    public partial class OpenOrdersView : UserControl
    {
        GridColumn CurrentColumn;
        IContextMenuHelper ContextMenuHelper;
        bool isEditOpen;

        public OpenOrdersView()
        {
            InitializeComponent();
            ContextMenuHelper = AppServiceProvider.GetServiceProvider().GetRequiredService<IContextMenuHelper>(); ;
            this.DataContext = AppServiceProvider.GetServiceProvider().GetRequiredService<OrdersListModel>();
            dataGrid.MouseRightButtonDown += ContextMenuHelper.Mouse_Right_Button_Clicked;
            ContextMenuHelper.dataGrid = dataGrid;
            isEditOpen = false;
        }

        private void btnCancelOrder_Click(object sender, RoutedEventArgs e)
        {
            if (this.DataContext is OrdersListModel model)
            {
                model.CancelOrder(out bool isCancelled,out string message);
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
            //if(this.DataContext is OrdersListModel model)
            //{
            //    model.ShowEditForm();
            //}
            if (!isEditOpen)
            {
                if (DataContext is OrdersListModel model)
                {
                    EditOrder editOrder = new EditOrder();
                    editOrder.DataContext = this.DataContext;
                    editOrder.Closed += EditOrder_Closed;
                    var Owner= AppServiceProvider.GetServiceProvider().GetRequiredService<MainWindow>(); ;
                    editOrder.Owner = Owner;
                    editOrder.Show();
                    isEditOpen = true;
                }
            }
        }

        private void ShowMenu_Click(object sender, DevExpress.Xpf.Grid.GridMenuEventArgs e)
        {
            //Remove Default Events
            e.Customizations.Add(new RemoveAction { ElementName = DefaultColumnMenuItemNames.ColumnChooser });
            e.Customizations.Add(new RemoveAction { ElementName = DefaultColumnMenuItemNames.BestFit });
            e.Customizations.Add(new RemoveAction { ElementName = DefaultColumnMenuItemNames.BestFitColumns });

            e.Customizations.Add(new RemoveAction { ElementName = DefaultColumnMenuItemNames.FilterEditor });
            e.Customizations.Add(new RemoveAction { ElementName = DefaultColumnMenuItemNames.SearchPanel });

            e.Customizations.Add(new RemoveAction { ElementName = DefaultColumnMenuItemNames.SortAscending });
            e.Customizations.Add(new RemoveAction { ElementName = DefaultColumnMenuItemNames.SortDescending });
            e.Customizations.Add(new RemoveAction { ElementName = DefaultColumnMenuItemNames.ClearSorting });

            e.Customizations.Add(new RemoveAction { ElementName = DefaultColumnMenuItemNames.GroupBox });
            e.Customizations.Add(new RemoveAction { ElementName = DefaultColumnMenuItemNames.GroupColumn });
            e.Customizations.Add(new RemoveAction { ElementName = DefaultColumnMenuItemNames.ClearGrouping });

            e.Customizations.Add(new RemoveAction { ElementName = DefaultColumnMenuItemNames.GroupSummaryEditor });
            e.Customizations.Add(new RemoveAction { ElementName = DefaultColumnMenuItemNames.FullExpand });
            e.Customizations.Add(new RemoveAction { ElementName = DefaultColumnMenuItemNames.FullCollapse });
            e.Customizations.Add(new RemoveAction { ElementName = DefaultColumnMenuItemNames.MenuColumnGroupInterval });

            //Add Custom Events
            var customBarItems = ContextMenuHelper.GetMenuItems();

            foreach (var barItem in customBarItems)
            {
                e.Customizations.Add(barItem);
            }
        }

        private void ClearGrouping_ItemClick(object sender, ItemClickEventArgs e)
        {
            dataGrid.ClearGrouping();
        }

        #region Mouse Click Events
        private void Mouse_Right_Button_Clicked(object sender, MouseButtonEventArgs e)
        {
            //var hitInfo = ((TableView)dataGrid.View).CalcHitInfo(Mouse.GetPosition(dataGrid));
            //if (hitInfo != null)
            //{
            //    CurrentColumn = hitInfo.Column;
            //}
        }

        private void EditOrder_Closed(object sender, System.EventArgs e)
        {
            isEditOpen = false;
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
