using OMS.Core.Core.Models.User;
using OMS.DataAccess.Repositories.AppRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.SqlData.Repositories
{
    public class UserRepository : IUserRepository
    {
        public bool AuthenticateUser(string username, string password, out string message, out int isDisabled, out int userID)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetAll()
        {
            throw new NotImplementedException();
        }

        public User GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
