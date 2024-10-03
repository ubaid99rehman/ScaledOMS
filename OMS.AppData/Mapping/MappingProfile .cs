using AutoMapper;
using OMS.Core.Models.Account;
using OMS.Core.Models.App;
using OMS.Core.Models.Orders;
using OMS.Core.Models.Permissions;
using OMS.Core.Models.Roles;
using OMS.Core.Models.Stocks;
using OMS.Core.Models.Trade;
using OMS.Core.Models.User;
using OMS.SqlData.Model;

namespace OMS.SqlData.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Accounts,IAccount>().ReverseMap();
            CreateMap<Orders,IOrder>().ReverseMap();
            CreateMap<Permissions,IPermission>().ReverseMap();
            CreateMap<Roles,IRole>().ReverseMap();
            CreateMap<Screens, IScreen>().ReverseMap();
            CreateMap<Stocks,IStock>().ReverseMap();
            CreateMap<Trades,ITrade>().ReverseMap();
            CreateMap<UserAccounts, IUserAccount>().ReverseMap();
            CreateMap<UserPermissions, IUserPermission>().ReverseMap();
            CreateMap<UserRoles, IUserRole>().ReverseMap();
            CreateMap<Users,IUser>().ReverseMap();
        }
    }

}
