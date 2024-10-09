using DevExpress.Mvvm;
using OMS.Core.Services.AppServices;
using System.Collections.ObjectModel;
using OMS.Core.Models.User;
using OMS.Core.Models.Roles;
using System.Linq;
using System.ComponentModel;
using System.Windows.Data;

namespace OMS.ViewModels
{
    public class OrderHistoryViewModel : ViewModelBase
    {
        // Service
        private readonly IUserService _userService;
        private readonly IOrderService OrderService;
        private readonly IPermissionService PermissionService;

        // Private Data Member
        private ObservableCollection<IUser> users;
        private bool _ShowUsers;
        private IUser _SelectedUser;

        private ICollectionView orders;
        public ICollectionView Orders
        {
            get => orders;
            private set => SetProperty(ref orders, value, nameof(Orders));
        }

        public ObservableCollection<IUser> Users
        {
            get => users ?? (users = new ObservableCollection<IUser>());
            set => SetProperty(ref users, value, nameof(Users));
        }

        public bool ShowUsers
        {
            get => _ShowUsers;
            set => SetProperty(ref _ShowUsers, value, nameof(ShowUsers));
        }

        public IUser SelectedUser
        {
            get => _SelectedUser;
            set
            {
                if (SetProperty(ref _SelectedUser, value, nameof(SelectedUser)))
                {
                    if (SelectedUser != null)
                    {
                        RefreshOrders();
                    }
                }
            }
        }

        // Constructor
        public OrderHistoryViewModel(IOrderService orderService, IUserService userService,
            IPermissionService permissionService)
        {
            OrderService = orderService;
            _userService = userService;
            PermissionService = permissionService;
            LoadUsers();
        }

        private void LoadUsers()
        {
            IUser user = _userService.GetUser();

            ObservableCollection<IUserRole> userRoles = PermissionService.GetUserRoles();
            foreach (IUserRole role in userRoles)
            {
                if (role.RoleID == 3)
                {
                    ShowUsers = true;
                    Users = _userService.GetAll();
                    SelectedUser = Users.FirstOrDefault();
                    break;
                }
            }
            if (SelectedUser == null)
            {
                SelectedUser = user;
            }
        }
        private void RefreshOrders()
        {
            if (SelectedUser != null && SelectedUser.UserID > 0)
            {
                var ordersList = OrderService.GetOrdersByUser(SelectedUser.UserID);
                ordersList.GroupDescriptions.Clear();
                ordersList.GroupDescriptions.Add(new PropertyGroupDescription("Order_Statuses"));
                ordersList.SortDescriptions.Clear();
                ordersList.SortDescriptions.Add(new SortDescription("Order_Statuses", ListSortDirection.Ascending));
                ordersList.SortDescriptions.Add(new SortDescription("OrderDate", ListSortDirection.Descending));
                Orders = ordersList;
            }
        }
    }
}