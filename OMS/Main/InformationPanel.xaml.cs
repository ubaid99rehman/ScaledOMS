using Microsoft.Extensions.DependencyInjection;
using OMS.ViewModels;
using System.Windows.Controls;

namespace OMS.Main
{
    public partial class InformationPanel : UserControl
    {
        //Constructor
        public InformationPanel()
        {
            InitializeComponent();
            this.DataContext = AppServiceProvider.GetServiceProvider().GetRequiredService<InformationPanelViewModel>();
        }
    }
}
