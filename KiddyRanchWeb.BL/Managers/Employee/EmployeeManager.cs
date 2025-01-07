using KiddyRanchWeb.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiddyRanchWeb.BL
{
    public class EmployeeManager :IEmployeeManager
    {
        private readonly IEmployeeRepo _employeeRepo;

        public EmployeeManager(IEmployeeRepo employeeRepo)
        {
            _employeeRepo = employeeRepo;
        }
        public IEnumerable<EmployeeDTO> GetEmployees()
        {
            IEnumerable<Employee> employeesDB = _employeeRepo.GetEmployees();
            return employeesDB.Select(u => new EmployeeDTO
            {
                EmployeeId = u.EmployeeId,
                Name = u.Name,
                Role = u.Role,
                bioDescription = u.bioDescription,
                profilePic = u.profilePic,
            });
        }
        public string Add(EmployeeAddDTO u)
        {
            Employee employee = new Employee
            {
                EmployeeId = Guid.NewGuid().ToString(),
                Name = u.Name,
                Role = u.Role,
                bioDescription = u.bioDescription,
                profilePic=u.profilePic,
            };
            _employeeRepo.Add(employee);
            _employeeRepo.SaveChanges();
            return employee.EmployeeId;
        }

        public bool Delete(string id)
        {
            Employee? employee = _employeeRepo.GetById(id);
            if (employee == null)
            {
                return false;
            }
            _employeeRepo.Delete(employee);
            _employeeRepo.SaveChanges();
            return true;
        }

        public EmployeeDTO GetEmployee(string id)
        {
            Employee? employee = _employeeRepo.GetById(id);
            if (employee is null)
            {
                return null;
            }

            return new EmployeeDTO
            {
                EmployeeId = id,
                Name = employee.Name,
                Role = employee.Role,
                bioDescription = employee.bioDescription,
                profilePic = employee.profilePic,
            };
        }
        public Employee GetEmployeeById(string id)
        {
            Employee? employee = _employeeRepo.GetById(id) as Employee;
            if (employee == null)
            {
                return null;
            }
            return employee;
        }

        public bool Update(EmployeeDTO employeeRequest)
        {
            if (employeeRequest.EmployeeId == null)
            {
                return false;
            }
            Employee? employee = _employeeRepo.GetById(employeeRequest.EmployeeId);
            if (employee is null)
            {
                return false;
            }
            employee.Name = employeeRequest.Name;
            employee.Role = employeeRequest.Role;
            employee.bioDescription = employeeRequest.bioDescription;
            employee.profilePic = employeeRequest.profilePic;
            _employeeRepo.Update(employee);
            _employeeRepo.SaveChanges();
            return true;

        }

    }
}
