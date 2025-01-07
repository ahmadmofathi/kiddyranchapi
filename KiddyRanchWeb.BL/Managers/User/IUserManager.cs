using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiddyRanchWeb.BL
{
    public interface IUserManager
    {
        IEnumerable<UserDTO> GetUsers();
        UserDTO GetUser(string id);
        string Add(UserAddDTO user);
        bool Update(UserDTO user);
        bool Delete(string id);
    }
}
