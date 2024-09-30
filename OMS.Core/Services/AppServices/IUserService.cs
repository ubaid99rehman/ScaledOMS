using OMS.Core.Core.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.Core.Services.AppServices
{
    public interface IUserService
    {
        bool UpdateUser(User user);

        User GetUser();
    }

}
