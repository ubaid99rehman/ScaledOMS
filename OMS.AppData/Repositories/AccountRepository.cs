using OMS.Core.Models.Account;
using System.Collections.Generic;
using OMS.DataAccess.Repositories.AppRepositories;
using AutoMapper;
using System.Linq;
using OMS.SqlData.Model;

namespace OMS.SqlData.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        IMapper Mapper;
        OMSContext Context;
        //Constructor
        public AccountRepository(IMapper imapper)
        {
            Mapper = imapper;
            Context = new OMSContext();
        }

        //Public Data Access Methods Implementation
        public IEnumerable<IAccount> GetAll()
        {
            var accountsList = Context.Accounts.ToList();
            if (accountsList.Count < 0 || accountsList == null)
            {
               return new List<IAccount>();
            }
            var mappedAccounts = accountsList.Select(a => Mapper.Map<IAccount>(a)).ToList();
            return mappedAccounts;
        }
        public IAccount GetById(int id)
        {
            Accounts accountEntity= Context.Accounts.Where(a => a.AccountID == id).FirstOrDefault();
            if (accountEntity == null)
            {
                return new Account();
            }
            var account = Mapper.Map<IAccount>(accountEntity);
            return account;
        }
    }
}
