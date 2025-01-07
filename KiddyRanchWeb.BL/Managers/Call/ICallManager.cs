using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiddyRanchWeb.BL
{
    public interface ICallManager
    {
        IEnumerable<CallDTO> GetCalls();
        IEnumerable<CallDTO> GetHisCalls(string calledId);
        CallDTO GetCall(string id);
        string Add(CallAddDTO call);
        bool Update(CallDTO call);
        bool Delete(string id);
    }
}
