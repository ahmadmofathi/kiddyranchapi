using ImagesTry;
using KiddyRanchWeb.BL;
using KiddyRanchWeb.DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KiddyRanchWeb.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IEmployeeManager _employeeManager;
        public EmployeesController(AppDbContext context,IEmployeeManager employeeManager) {
            _context = context;
            _employeeManager = employeeManager;
        }

        [HttpGet]
        public IActionResult GetAllEmployees()
        {
            var Employees = _employeeManager.GetEmployees();
            return Ok(Employees);
        }

        [HttpGet("{id}")]
        public IActionResult GetEmployeeById(string id)
        {
            var Employee = _employeeManager.GetEmployee(id);
            if (Employee == null)
            {
                return BadRequest("Employee not found");
            }
            return Ok(Employee);
        }
        [HttpPost]
        public IActionResult AddEmployee([FromForm] EmployeeAddDTO employee, IFormFile file)
        {

            #region Checking Extension
            var extension = Path.GetExtension(file.FileName);
            var allowedExtensions = new string[]
            {
                ".jpg",".png",".jpeg"
            };
            bool isExtensionAllowed = allowedExtensions.Contains(extension, StringComparer.InvariantCultureIgnoreCase);
            if (!isExtensionAllowed)
            {
                return BadRequest(new UploadFileDTO(false, "", "Extension is not allowed"));
            }
            #endregion
            #region Checking Size
            bool isSizeAllowed = file.Length is > 0 and < 70_000_000;
            if (!isSizeAllowed)
            {
                return BadRequest(new UploadFileDTO(false, "", "Size is not allowed"));
            }
            #endregion

            #region GUID
            var newFileName = $"{Guid.NewGuid()}{extension}";
            var imagesPath = Path.Combine(Environment.CurrentDirectory, "Uploads/StaticContent/");
            var FullFilePath = Path.Combine(imagesPath, newFileName);
            #endregion

            using var Stream = new FileStream(FullFilePath, FileMode.Create);
            file.CopyTo(Stream);
            var URL = $"{Request.Scheme}://{Request.Host}/FileManager/GetImage?imageName={newFileName}";
            employee.profilePic = URL;
            var emp = _employeeManager.Add(employee);
            if (emp!=null)
            {
            return Ok(emp);
            }
            return BadRequest("Error Adding Employee");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteEmployee(string id)
        {
            EmployeeDTO? Employee = _employeeManager.GetEmployee(id);
            if (Employee == null)
            {
                return NotFound("Employee not found");
            }
            _employeeManager.Delete(id);
            return Ok("Employee " + id + " has been deleted successfully");
        }

        [HttpPut("{id}")]
        public ActionResult<Employee> UpdateEmployee(string id, EmployeeDTO Employee)
        {
            if (id != Employee.EmployeeId)
            {
                return BadRequest();
            }
            _employeeManager.Update(Employee);
            return NoContent();
        }

    }
}
