using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiddyRanchWeb.DAL { 
    public class EmployeeRepo : IEmployeeRepo 
    {
        private readonly AppDbContext _context;

        public EmployeeRepo(AppDbContext context)
        {
            _context = context;
        }
        public List<Employee> GetEmployees()
        {
            return _context.Set<Employee>().ToList();
        }
        public Employee? GetById(string id)
        {
            return _context.Set<Employee>().Find(id);
        }
        public void Add(Employee user)
        {
            _context.Set<Employee>().Add(user);
        }

        public void Delete(Employee user)
        {
            _context.Set<Employee>().Remove(user);
        }
        public void Update(Employee user)
        {
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }
    }
}
