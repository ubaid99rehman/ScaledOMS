using OMS.DataAccess.Repositories.AppRepositories;
using System.Collections.Generic;
using System.Data;
using OMS.Core.Models.User;
using OMS.SqlData.Model;
using System.Linq;
using AutoMapper;
using OMS.Core.Core.Models.User;

namespace OMS.SqlData.Repositories
{
    public class UserRepository : IUserRepository
    {
        IMapper Mapper;
        OMSContext Context;

        //Constructor
        public UserRepository(IMapper mapper)
        {
            Mapper = mapper;
            Context = new OMSContext();
        }

        //Public Access Methods
        public IUser AuthenticateUser(UserCredentials credentials)
        {
            Users userEntity =  Context.Users.Where(u=> u.Password == credentials.Password 
            && u.UserName == credentials.Username).FirstOrDefault();
            if (userEntity == null)
            {
                return null;
            }
            foreach(UserRoles roles in userEntity.UserRoles)
            {
                foreach(Permissions permission in roles.Roles.Permissions)
                {
                    var screen = permission.Screens;
                    var name = permission.Screens.ScreenName;
                }
            }
            IUser user = Mapper.Map<IUser>(userEntity);
            if(user != null)
            {
                return user;
            }
            return null;
        }
        public IEnumerable<IUser> GetAll()
        {
            var usersList = Context.Users.ToList();
            if (usersList.Count < 0 || usersList == null)
            {
                return new List<IUser>();
            }
            var users = usersList.Select(u => Mapper.Map<IUser>(u)).ToList();
            return users;
        }
        public IUser GetById(int id)
        {
            Users userEntity = Context.Users.Where(u => u.UserID == id).FirstOrDefault();
            if (userEntity == null)
            {
                return new User();
            }
            var user = Mapper.Map<IUser>(userEntity);
            return user;
        }
        public bool UpdateUser(IUser user)
        {
            bool isUpdated = false;
            var toUpdate = Mapper.Map<Users>(user);
            var userEntity = Context.Users.Where(u => u.UserID == user.UserID).FirstOrDefault();
            if (userEntity != null)
            {
                userEntity = toUpdate;
                Context.SaveChanges();
            }
            return false;
        }
    }
}
