using OMS.Core.Models.Account;
using OMS.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.SqlData.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        public IEnumerable<Account> GetAll()
        {
            throw new NotImplementedException();
        }

        public Account GetById(int orderID)
        {
            throw new NotImplementedException();
        }
    }
}
