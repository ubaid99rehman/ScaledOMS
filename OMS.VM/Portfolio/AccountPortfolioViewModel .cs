using DevExpress.Mvvm;
using OMS.Core.Models.Account;
using OMS.Core.Models.Orders;
using OMS.Core.Services.AppServices;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace OMS.ViewModels
{
    public class AccountPortfolioViewModel : ViewModelBase
    {
        //Service
        IAccountService AccountService;
        IOrderService OrderService;

        //Private Memebers
        private int _account;
        private IAccount _selectedAccount;
        private ObservableCollection<int> _accountsList;
        private ObservableCollection<IAccount> _accounts;
        private ObservableCollection<IOrder> _orders;

        //Public Members
        public int Account
        {
            get { return _account; }
            set
            {
                if(SetProperty(ref _account, value, nameof(Account)))
                {
                    SelectedAccountChange();    
                }
            }
        }
        public IAccount SelectedAccount
        {
            get { return _selectedAccount; }
            set
            {
                SetProperty(ref _selectedAccount, value, nameof(SelectedAccount));
            }
        }
        public ObservableCollection<int> AccountsList
        {
            get { return _accountsList; }
            set { SetProperty(ref _accountsList, value, nameof(AccountsList)); }
        }
        public ObservableCollection<IAccount> Accounts
        {
            get { return _accounts; }
            set { SetProperty(ref _accounts, value, nameof(Accounts)); }
        }
        public ICollectionView Orders
        {
            get { return OrderService.GetOrdersByAccount(SelectedAccount.AccountID); }
            //set { SetProperty(ref _orders, value, nameof(Orders)); }
        }
        public ObservableCollection<StockHolding> StockHoldingsData { get; set; }

        //Constructor
        public AccountPortfolioViewModel(IAccountService accountService, IOrderService orderService) 
        {
            AccountService = accountService;
            OrderService = orderService;
            AccountsList = new ObservableCollection<int>();
            Accounts = new ObservableCollection<IAccount>();
            StockHoldingsData = new ObservableCollection<StockHolding>
        {
            new StockHolding { Symbol = "AAPL", Volume = 150000, Units = 500 },
            new StockHolding { Symbol = "MSFT", Volume = 120000, Units = 400 },
            new StockHolding { Symbol = "GOOGL", Volume = 100000, Units = 350 },
            new StockHolding { Symbol = "AMZN", Volume = 80000, Units = 280 },
            new StockHolding { Symbol = "NVDA", Volume = 80000, Units = 300 }
        };
            LoadAccountsData();
        }

        //Data Loading Method
        private void LoadAccountsData()
        {
            var list = AccountService.GetAccountsList();
            if(list != null && list.Count > 0)
            {
                AccountsList = list;
            }

            var accounts = AccountService.GetAll();
            if (accounts != null && accounts.Count > 0)
            {
                Accounts = accounts;
            }
            Account =AccountsList.FirstOrDefault();
        }
        private void SelectedAccountChange()
        {
            //Fetch Account Instance
            var account = Accounts.Where(a => a.AccountID == _account).FirstOrDefault();
            if (account != null)
            {
                SelectedAccount = account;
            }
            //Fetch Account related Orders
            
            //Orders = OrderService.GetOrdersByAccount(SelectedAccount.AccountID);
        }
    }
}

