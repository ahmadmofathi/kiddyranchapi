using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiddyRanchWeb.DAL
{
    public interface IEmployeeRepo
    {
        List<Employee> GetEmployees();
        Employee? GetById(string id);
        void Add(Employee user);
        void Update(Employee user);
        void Delete(Employee user);
        int SaveChanges();
    }
}
