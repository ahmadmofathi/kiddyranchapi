using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiddyRanchWeb.DAL
{
    public interface ICallRepo
    {
        List<Call> GetHisCalls(string callerId);
        List<Call> GetCalls();
        Call? GetById(string id);
        void Add(Call call);
        void Update(Call call);
        void Delete(Call call);
        int SaveChanges();
    }
}
