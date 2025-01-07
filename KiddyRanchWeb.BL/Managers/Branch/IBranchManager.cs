using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiddyRanchWeb.BL
{
    public interface IBranchManager
    {
        IEnumerable<BranchDTO> GetBranchs();
        BranchDTO GetBranch(string id);
        string Add(BranchAddDTO branch);
        bool Update(BranchDTO branch);
        bool Delete(string id);
    }
}
