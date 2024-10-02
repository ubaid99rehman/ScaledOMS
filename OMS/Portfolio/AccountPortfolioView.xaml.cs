using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Grid;
using Microsoft.Extensions.DependencyInjection;
using OMS.ViewModels;
using System.Windows.Controls;

namespace OMS.Portfolio
{
    public partial class AccountPortfolioView : UserControl
    {
        IContextMenuHelper ContextMenuHelper;
        //Constructor
        public AccountPortfolioView()
        {
            InitializeComponent();
            this.DataContext = AppServiceProvider.GetServiceProvider().GetRequiredService<AccountPortfolioViewModel>();
            //Grid Context Menu
            ContextMenuHelper = AppServiceProvider.GetServiceProvider().GetRequiredService<IContextMenuHelper>(); ;
            ContextMenuHelper.dataGrid = dataGrid;
        }

        //Grid Custom Context Menu 
        private void ShowMenu_Click(object sender, DevExpress.Xpf.Grid.GridMenuEventArgs e)
        {
            RemoveDefaultActions(e);
            AddCustomeActions(e);
        }
        private void AddCustomeActions(GridMenuEventArgs e)
        {
            var customBarItems = ContextMenuHelper.GetAllMenuItems();
            foreach (var barItem in customBarItems)
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


    }
}
