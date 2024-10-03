using Microsoft.Extensions.DependencyInjection;
using OMS.ViewModels;
using System.Windows.Controls;

namespace OMS.Main
{
    public partial class InformationPanelView : UserControl
    {
        //Constructor
        public InformationPanelView()
        {
            InitializeComponent();
            this.DataContext = AppServiceProvider.GetServiceProvider().GetRequiredService<InformationPanelViewModel>();
        }
    }
}
