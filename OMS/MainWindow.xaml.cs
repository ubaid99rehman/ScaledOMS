using DevExpress.Mvvm;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Docking;
using Microsoft.Extensions.DependencyInjection;
using OMS.ViewModels;
using System;
using System.Windows;

namespace OMS
{
    public partial class MainWindow : ThemedWindow
    {
        //Child Winodw Open Props
        bool isOrderOpen;
        bool isUserWindowOpen;
        
        //Constructor
        public MainWindow()
        {
            InitializeComponent();
            isOrderOpen = false;
            isUserWindowOpen = false;
            //Datacontext
            var model = AppServiceProvider.GetServiceProvider().GetRequiredService<MainViewModel>();
            //Binding Navigation Service
            documentManagerService = (TabbedDocumentUIService)model.DocumentManagerService;
            this.DataContext = model;
        }

        #region Child Window Open Click
        private void InfoIcon_Click(object sender, RoutedEventArgs e)
        {
            InfoPopup.IsOpen = !InfoPopup.IsOpen;

        }
        private void NewOrder_Click(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            if (!isOrderOpen)
            {
                AddOrder windo = new AddOrder();
                windo.Closed += OrderWindow_Closed;
                var Owner = AppServiceProvider.GetServiceProvider().GetRequiredService<MainWindow>(); ;
                windo.Owner = Owner;
                windo.Show();

                isOrderOpen = true;
            }
        }
        private void UserUpdate_Click(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            if (!isUserWindowOpen)
            {
                ProfileView windo = new ProfileView();
                windo.Closed += UserWindow_Closed;
                var Owner = AppServiceProvider.GetServiceProvider().GetRequiredService<MainWindow>(); ;
                windo.Owner = Owner;
                windo.Show();
                isUserWindowOpen = true;
            }
        } 
        #endregion

        #region Main Window Open/Close
        private void ThemedWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (this.DataContext is MainViewModel model)
            {
                model.SaveOpenedDocumentsState();
            }
        }
        private void ThemedWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (this.DataContext is MainViewModel model)
            {
                model.RestoreOpenedDocumentsState();
            }
        } 
        #endregion

        #region Child Window Closed
        private void OrderWindow_Closed(object sender, System.EventArgs e)
        {
            isOrderOpen = false;
        }
        private void UserWindow_Closed(object sender, EventArgs e)
        {
            isUserWindowOpen = false;
        } 
        #endregion
    }
}