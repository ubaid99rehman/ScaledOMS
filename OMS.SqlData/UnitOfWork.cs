using OMS.DataAccess.Repositories;
using OMS.DataAccess.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.SqlData
{
    public class UnitOfWork : IUnitOfWork
    {
        public IOrderRepository Orders => throw new NotImplementedException();

        public IAccountRepository Accounts => throw new NotImplementedException();

        public void Complete()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
