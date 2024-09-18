using DevExpress.Xpf.Core;
using DevExpress.Xpf.Docking;
using Microsoft.Extensions.DependencyInjection;
using OMS.Logging;
using OMS.ViewModels;
using System.Windows;

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
            LogHelper.LogInfo("Loading Services Data....");
            BootStrapper.LoadServices();
            LogHelper.LogInfo("Services Data Loaded");
        }

        private void InfoIcon_Click(object sender, RoutedEventArgs e)
        {
            InfoPopup.IsOpen = !InfoPopup.IsOpen;

        }
    }
}
