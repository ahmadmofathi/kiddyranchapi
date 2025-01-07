using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiddyRanchWeb.DAL
{
    public class BranchRepo : IBranchRepo
    {
        private readonly AppDbContext _context;

        public BranchRepo(AppDbContext context)
        {
            _context = context;
        }
        public List<Branch> GetBranchs()
        {
            return _context.Set<Branch>().ToList();
        }
        public Branch? GetById(string id)
        {
            return _context.Set<Branch>().Find(id);
        }
        public void Add(Branch branch)
        {
            _context.Set<Branch>().Add(branch);
        }

        public void Delete(Branch branch)
        {
            _context.Set<Branch>().Remove(branch);
        }
        public void Update(Branch branch)
        {
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }
    }
}
