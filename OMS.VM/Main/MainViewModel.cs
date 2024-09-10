using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Xpf.Core.Native;
using DevExpress.Xpf.Docking;
using OMS.Core.Models.User;
using OMS.Core.Services;
using System;
using System.Windows.Threading;

namespace OMS.ViewModels
{
    public partial class MainViewModel : ViewModelBase
    {
        [ServiceProperty(Key = "documentManagerService")]
        public virtual IDocumentManagerService DocumentManagerService { get; }

        private AddOrderModel _orderModel;
        public AddOrderModel NewOrderModel
        {
            get { return _orderModel; }
            set { SetProperty(ref _orderModel, value, nameof(NewOrderModel)); }
        }

        private readonly Random _random;

        public const int Tick = 500;
        readonly DispatcherTimer updateTimer;
        public virtual DateTime CurrentTime { get; protected set; }

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

        public MainViewModel()
        {
            Title = "OMS";
            IsPopupOpen = false;
            _random = new Random();

            NewOrderModel = new AddOrderModel();
            updateTimer = new DispatcherTimer(DispatcherPriority.ApplicationIdle);
            InitTimer();
        }

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

        #region Views Navigations

        public void Dashboard()
        {
            AddView("DashboardView", "Dashboard");
        }

        public void Portfolio()
        {
            AddView("AccountPortfolioView", "Portfolio");
        }

        public void StockMarket()
        {
            AddView("StockMarketView", "Market Watch");
        }

        public void NewOrder()
        {
            ShowAddOrderForm();
        }

        public void ShowAddOrderForm()
        {
            IsPopupOpen = true;
        }

        public void CloseAddOrderForm()
        {
            IsPopupOpen = false;
        }

        public void OpenOrders()
        {
            AddView("NewOrder", "Manage Orders");
        }

        public void ManageOrders()
        {
            AddView("ManageOrderView", "Manage Orders");
        }

        public void OrderHistory()
        {
            AddView("OrderHistoryView", "Orders History");
        }

        public void Profile()
        {
            AddView("ProfileView", "Appearance");
        }

        public void Appearance()
        {
            AddView("AppearanceView", "Appearance");
        }
        #endregion

        private void OnDocumentActivated(object sender, ActiveDocumentChangedEventArgs e)
        {
            Title = (string)e.NewDocument?.Title ?? "HOME";
        }

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

        void UpdateOnTimer(object sender, EventArgs e)
        {
            CurrentTime = DateTime.Now;
        }

        void InitTimer()
        {
            updateTimer.Interval = TimeSpan.FromMilliseconds(Tick);
            updateTimer.Tick += new EventHandler(UpdateOnTimer);
            updateTimer.Start();
        }
    }
}
