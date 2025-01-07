using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiddyRanchWeb.DAL
{
    public interface IUserRepo
    {
        List<User> GetUsers();
        User? GetById(string id);
        User? GetByUsername(string username);
        void Add(User user);
        void Update(User user);
        void Delete(User user);
        int SaveChanges();
    }
}
