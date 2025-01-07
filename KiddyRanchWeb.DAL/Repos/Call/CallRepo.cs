using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiddyRanchWeb.DAL
{
    public class CallRepo: ICallRepo 
    {
        private readonly AppDbContext _context;

        public CallRepo(AppDbContext context)
        {
            _context = context;
        }
        public List<Call> GetCalls()
        {
            return _context.Set<Call>().ToList();
        }
        public List<Call> GetHisCalls(string calledId)
        {
            return _context.Set<Call>().Where(c => c.calledId == calledId).ToList();
        }
        public Call? GetById(string id)
        {
            return _context.Set<Call>().Find(id);
        }
        public void Add(Call call)
        {
            _context.Set<Call>().Add(call);
        }

        public void Delete(Call call)
        {
            _context.Set<Call>().Remove(call);
        }
        public void Update(Call call)
        {
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }
    }
}
