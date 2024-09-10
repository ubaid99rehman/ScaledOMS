using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.Core.Services
{
    public interface IRealtimeService
    {
        void StartSession();
        void Refresh(object sender, EventArgs e);
    }
}
