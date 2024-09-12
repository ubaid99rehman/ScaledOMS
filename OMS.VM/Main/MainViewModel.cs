using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using OMS.Core.Models;
using OMS.Core.Services.AppServices.RealtimeServices;
using System;

namespace OMS.ViewModels
{
    public partial class MainViewModel : ViewModelBase
    {
        [ServiceProperty(Key = "documentManagerService")]
        public virtual IDocumentManagerService DocumentManagerService
        { 
            get{ return GetService<IDocumentManagerService>(); } 
        }

        public IAppTimerService AppTimerService { get; }

        private AddOrderModel _orderModel;
        public AddOrderModel NewOrderModel
        {
            get { return _orderModel; }
            set { SetProperty(ref _orderModel, value, nameof(NewOrderModel)); }
        }

        #region Bindable Props
        public AppTime CurrentTime 
        {
            get { return AppTimerService.GetCurrentDateTime(); }
        }

        private bool _isPopupOpen;
        public bool IsPopupOpen
        {
            get => _isPopupOpen;
            set => SetProperty(ref _isPopupOpen, value, nameof(IsPopupOpen));
        }

        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value, nameof(Title)); }
        }

        private bool _isLoadingScreen;
        public bool IsLoadingScreen
        {
            get
            {
                return this._isLoadingScreen;
            }

            set
            {
                SetProperty(ref _isLoadingScreen, value, nameof(IsLoadingScreen));
            }
        } 
        #endregion

        #region Constructor
        public MainViewModel(IAppTimerService timerService)
        {
            AppTimerService = timerService;
            AppTimerService.StartSession();
            Title = "OMS";
            IsPopupOpen = false;
            NewOrderModel = new AddOrderModel();
        } 
        #endregion

        #region Loading Decorator Methods
        private void Navigating()
        {
            IsLoadingScreen = true;
        }

        private void Navigated()
        {
            IsLoadingScreen = false;
        }
        #endregion

        #region Views Navigation Commands
        [Command]
        public void Dashboard()
        {
            AddView("DashboardView", "Dashboard");
        }

        [Command]
        public void Portfolio()
        {
            AddView("AccountPortfolioView", "Portfolio");
        }

        [Command]
        public void StockMarket()
        {
            AddView("StockMarketView", "Market Watch");
        }

        [Command]
        public void NewOrder()
        {
            ShowAddOrderForm();
        }

        [Command]
        public void ShowAddOrderForm()
        {
            IsPopupOpen = true;
        }

        [Command]
        public void CloseAddOrderForm()
        {
            IsPopupOpen = false;
        }

        [Command]
        public void OpenOrders()
        {
            AddView("NewOrder", "Manage Orders");
        }

        [Command]
        public void ManageOrders()
        {
            AddView("ManageOrderView", "Manage Orders");
        }

        [Command]
        public void OrderHistory()
        {
            AddView("OrderHistoryView", "Orders History");
        }

        [Command]
        public void Profile()
        {
            AddView("ProfileView", "Appearance");
        }
        
        [Command]
        public void Appearance()
        {
            AddView("AppearanceView", "Appearance");
        }
        
        [Command]
        public void OnViewLoaded()
        {
            if (DocumentManagerService != null)
            {
                DocumentManagerService.ActiveDocumentChanged += OnDocumentActivated;
                Dashboard();
            }
            else
            {
                throw new InvalidOperationException("DocumentManagerService is not available.");
            }
        }
        #endregion

        #region Event Handler

        private void OnDocumentActivated(object sender, ActiveDocumentChangedEventArgs e)
        {
            Title = (string)e.NewDocument?.Title ?? "HOME";
        } 
        #endregion

        public void AddView(string view, string header)
        {
            Navigating();
            if (DocumentManagerService == null)
            {
                throw new InvalidOperationException("DocumentManagerService is not available.");
            }

            IDocument document = DocumentManagerService.CreateDocument(view, null, this);
            document.Title = header;
            document.Show();
            Title = header;
            Navigated();
        }

    }
}
