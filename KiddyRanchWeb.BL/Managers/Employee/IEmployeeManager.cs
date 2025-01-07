using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiddyRanchWeb.BL
{
    public interface IEmployeeManager
    {
        IEnumerable<EmployeeDTO> GetEmployees();
        EmployeeDTO GetEmployee(string id);
        string Add(EmployeeAddDTO user);
        bool Update(EmployeeDTO user);
        bool Delete(string id);
    }
}
