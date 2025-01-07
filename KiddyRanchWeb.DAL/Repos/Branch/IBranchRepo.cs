using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiddyRanchWeb.DAL
{
    public interface IBranchRepo
    {
        List<Branch> GetBranchs();
        Branch? GetById(string id);
        void Add(Branch branch);
        void Update(Branch branch);
        void Delete(Branch branch);
        int SaveChanges();
    }
}
