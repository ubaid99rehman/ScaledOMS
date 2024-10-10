using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Grid;
using Microsoft.Extensions.DependencyInjection;
using OMS.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace OMS.Orders
{
    public partial class OrderHistoryView : UserControl
    {
        OrderHistoryViewModel model;
        IContextMenuHelper ContextMenuHelper;

        //Constructor
        public OrderHistoryView()
        {
            InitializeComponent();
            model =AppServiceProvider.GetServiceProvider().GetRequiredService<OrderHistoryViewModel>();
            this.DataContext = model;
            ContextMenuHelper = AppServiceProvider.GetServiceProvider().GetRequiredService<IContextMenuHelper>(); ;
            ContextMenuHelper.dataGrid = dataGrid;
            SetUsersVisibility();
        }

        private void SetUsersVisibility()
        {
            if (model.ShowUsers)
            {
                UsersList.Visibility = Visibility.Visible;
            }
            else
            {
                UsersList.Visibility = Visibility.Collapsed;
            }
        }

        //Custom Context Menu
        private void ShowMenu_Click(object sender, DevExpress.Xpf.Grid.GridMenuEventArgs e)
        {
            RemoveDefaultActions(e);
            AddCustomeActions(e);
        }
        private void AddCustomeActions(GridMenuEventArgs e)
        {
            var customalignmentBarItems = ContextMenuHelper.GetAlignmentMenuItems();
            var customSearchBarItems = ContextMenuHelper.GetSearchAndFilterMenuItems();
            foreach (var barItem in customSearchBarItems)
            {
                e.Customizations.Add(barItem);
            }
            foreach (var barItem in customalignmentBarItems)
            {
                e.Customizations.Add(barItem);
            }
        }
        private void RemoveDefaultActions(GridMenuEventArgs e)
        {
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
        }

        private void Mouse_Right_Button_Clicked(object sender, MouseButtonEventArgs e)
        {
            ContextMenuHelper.Mouse_Right_Button_Clicked(sender, e);
        }
    }
}
