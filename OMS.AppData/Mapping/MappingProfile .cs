using AutoMapper;
using OMS.Core.Models.Account;
using OMS.Core.Models.App;
using OMS.Core.Models.Orders;
using OMS.Core.Models.Permissions;
using OMS.Core.Models.Roles;
using OMS.Core.Models.Stocks;
using OMS.Core.Models.Trade;
using OMS.Core.Models.User;
using OMS.Enums;
using OMS.SqlData.Model;

namespace OMS.SqlData.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Accounts,IAccount>().ReverseMap();
            CreateMap<Permissions, IPermission>()
            .ForMember(dest => dest.Screen, opt => opt.MapFrom(src => new Screens
            {
                ScreenID = src.Screens.ScreenID,
                ScreenName = src.Screens.ScreenName,
                ScreenDescription = src.Screens.ScreenDescription,
                CreatedBy = src.Screens.CreatedBy,
                CreatedDate = src.Screens.CreatedDate,
                UpdatedBy = src.Screens.UpdatedBy,
                UpdatedDate = src.Screens.UpdatedDate,
                Permissions = src.Screens.Permissions,

            }))
            .ReverseMap()
            .ForMember(dest => dest.Screens,opt=>opt.MapFrom(src=> new Screen
            {
                ScreenID = src.Screen.ScreenID,
                ScreenName = src.Screen.ScreenName,
                CreatedDate = src.Screen.CreatedDate,
                CreatedBy = src.Screen.CreatedBy,
                UpdatedDate = src.Screen.UpdatedDate,
                UpdatedBy = src.Screen.UpdatedBy,
            }));
            CreateMap<Roles,IRole>().ReverseMap();
            CreateMap<Screens, IScreen>().ReverseMap();
            CreateMap<Stocks,IStock>().ReverseMap();
            CreateMap<Trades,ITrade>().ReverseMap();
            CreateMap<UserAccounts, IUserAccount>().ReverseMap();
            CreateMap<UserPermissions, IUserPermission>().ReverseMap();
            CreateMap<UserRoles, IUserRole>().ReverseMap();
            CreateMap<Users,IUser>().ReverseMap();
            CreateMap<Orders, IOrder>()
           .ForMember(dest => dest.Order_Statuses, opt => opt.MapFrom(src => (OrderStatus)src.Order_Statuses.ID))
           .ForMember(dest => dest.Order_Types, opt => opt.MapFrom(src => (OrderType)src.Order_Types.ID))
           .ForMember(dest => dest.Account, opt => opt.MapFrom(src => new Account
           {
               AccountID = src.Accounts.AccountID,
               AccountName = src.Accounts.AccountName,
               AccountNumber = src.Accounts.AccountNumber,
               CreatedDate = src.Accounts.CreatedDate,
           } ))
           .ReverseMap()
           .ForMember(dest => dest.Order_Statuses, opt => opt.MapFrom(src => new Order_Statuses { 
                ID = (int)src.Order_Statuses,
                Name = src.Order_Statuses.ToString()
            }))
           .ForMember(dest => dest.Order_Types, opt => opt.MapFrom(src => new Order_Types{
               ID = (int)src.Order_Types,
               Name = src.Order_Types.ToString()
            }))
           .ForMember(dest => dest.Accounts, opt => opt.MapFrom(src => new Accounts
           {
               AccountID = src.Account.AccountID,
               AccountName = src.Account.AccountName,
               AccountNumber = src.Account.AccountNumber,
               CreatedDate = src.Account.CreatedDate
           }));
        }
    }
}
