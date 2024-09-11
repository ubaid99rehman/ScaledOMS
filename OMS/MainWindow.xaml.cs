using DevExpress.Xpf.Core;
using DevExpress.Xpf.Docking;
using Microsoft.Extensions.DependencyInjection;
using OMS.ViewModels;

namespace OMS
{
    public partial class MainWindow : ThemedWindow
    {
        IBootStrapper BootStrapper;

        public MainWindow(IBootStrapper bootStrapper)
        {
            InitializeComponent();
            var model = AppServiceProvider.GetServiceProvider().GetRequiredService<MainViewModel>();
            documentManagerService = (TabbedDocumentUIService)model.DocumentManagerService;
            this.DataContext = model;
            BootStrapper = bootStrapper;
            BootStrapper.LoadServices();
        }
    }
}
