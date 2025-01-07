using ClosedXML.Excel;
using KiddyRanchWeb.BL;
using KiddyRanchWeb.DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KiddyRanchWeb.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CareerInterviewsController : ControllerBase
    {

        private readonly AppDbContext _context;
        private readonly ICareerInterviewManager _careerInterviewManager;
        public CareerInterviewsController(AppDbContext context, ICareerInterviewManager careerInterviewManager)
        {
            _context = context;
            _careerInterviewManager = careerInterviewManager;
        }


        [HttpGet("export")]
        public IActionResult ExportToExcel()
        {
            var careerInterviews = _careerInterviewManager.GetCareerInterviews().ToList();

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("CareerInterviews");

                // Add headers
                worksheet.Cell(1, 1).Value = "Career Interview ID";
                worksheet.Cell(1, 2).Value = "First Name";
                worksheet.Cell(1, 3).Value = "Last Name";
                worksheet.Cell(1, 4).Value = "Job Title";
                worksheet.Cell(1, 5).Value = "NID";
                worksheet.Cell(1, 6).Value = "Address";
                worksheet.Cell(1, 7).Value = "Phone Number";
                worksheet.Cell(1, 8).Value = "Birthday";
                worksheet.Cell(1, 9).Value = "WhatsApp";
                worksheet.Cell(1, 10).Value = "CV Description";
                worksheet.Cell(1, 11).Value = "Education";
                worksheet.Cell(1, 12).Value = "Interview Date";
                worksheet.Cell(1, 13).Value = "Age";
                worksheet.Cell(1, 14).Value = "Status";
                worksheet.Cell(1, 15).Value = "Creation Date";
                worksheet.Cell(1, 16).Value = "Update Date";
                worksheet.Cell(1, 17).Value = "Comment";

                // Write data
                int row = 2;
                foreach (var interview in careerInterviews)
                {
                    worksheet.Cell(row, 1).Value = interview.careerInterviewId;
                    worksheet.Cell(row, 2).Value = interview.FirstName;
                    worksheet.Cell(row, 3).Value = interview.LastName;
                    worksheet.Cell(row, 4).Value = interview.JobTitle;
                    worksheet.Cell(row, 5).Value = interview.NID;
                    worksheet.Cell(row, 6).Value = interview.Adderss;
                    worksheet.Cell(row, 7).Value = interview.PhoneNumber;
                    worksheet.Cell(row, 8).Value = interview.Birthday.ToString("yyyy-MM-dd");
                    worksheet.Cell(row, 9).Value = interview.WhatsApp;
                    worksheet.Cell(row, 10).Value = interview.CvDescription;
                    worksheet.Cell(row, 11).Value = interview.Education;
                    worksheet.Cell(row, 12).Value = interview.InterviewDate?.ToString("yyyy-MM-dd") ?? string.Empty;
                    worksheet.Cell(row, 13).Value = interview.Age;
                    worksheet.Cell(row, 14).Value = interview.Status;
                    worksheet.Cell(row, 15).Value = interview.creationDate?.ToString("yyyy-MM-dd") ?? string.Empty;
                    worksheet.Cell(row, 16).Value = interview.updateDate?.ToString("yyyy-MM-dd") ?? string.Empty;
                    worksheet.Cell(row, 17).Value = interview.comment ?? string.Empty;
                    row++;
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "CareerInterviews.xlsx");
                }
            }
        }
        [HttpGet]
        public IActionResult GetAllCareerInterviews(string searchTerm = "", string statusFilter = "All", string sortBy = "", string sortOrder = "", int pageNumber = 1, int pageSize = 10)
        {
            var careerInterviews = _careerInterviewManager.GetPaginationCareerInterviews(searchTerm, statusFilter, sortBy, sortOrder, pageNumber, pageSize);

            var totalCount = _careerInterviewManager.GetCareerInterviews()
                .Where(ci => (string.IsNullOrWhiteSpace(searchTerm) ||
                             ci.FirstName.ToLower().Contains(searchTerm.ToLower()) ||
                             ci.LastName.ToLower().Contains(searchTerm.ToLower()) ||
                             ci.PhoneNumber.ToLower().Contains(searchTerm.ToLower()) ||
                             ci.Education.ToLower().Contains(searchTerm.ToLower())) &&
                             (statusFilter == "All" || ci.Status == statusFilter))
                .Count();

            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

            return Ok(new
            {
                TotalCount = totalCount,
                TotalPages = totalPages,
                PageNumber = pageNumber,
                PageSize = pageSize,
                Data = careerInterviews
            });
        }




        [HttpGet("{id}")]
        public IActionResult GetCareerInterviewById(string id)
        {
            var CareerInterview = _careerInterviewManager.GetCareerInterview(id);
            if (CareerInterview == null)
            {
                return BadRequest("CareerInterview not found");
            }
            return Ok(CareerInterview);
        }
        [HttpGet("stats")]
        public IActionResult GetStats()
        {
            var careerInterviews = _careerInterviewManager.GetCareerInterviews();

            var totalInterviews = careerInterviews.Count();
            var interviewsByJobTitle = careerInterviews
                .GroupBy(ci => ci.JobTitle)
                .Select(group => new { JobTitle = group.Key, Count = group.Count() })
                .ToList();
            var interviewsByStatus = careerInterviews
                .GroupBy(ci => ci.Status)
                .Select(group => new { Status = group.Key, Count = group.Count() })
                .ToList();

            var currentMonth = DateTime.Now.Month;
            var currentYear = DateTime.Now.Year;
            var interviewsThisMonth = careerInterviews
                .Where(ci => ci.InterviewDate.HasValue &&
                             ci.InterviewDate.Value.Month == currentMonth &&
                             ci.InterviewDate.Value.Year == currentYear)
                .Count();

            return Ok(new
            {
                TotalInterviews = totalInterviews,
                InterviewsByJobTitle = interviewsByJobTitle,
                InterviewsByStatus = interviewsByStatus,
                InterviewsThisMonth = interviewsThisMonth
            });
        }


        [HttpGet("isToday")]
        public IActionResult TodayInterviews()
        {
            var careerInterviews = _careerInterviewManager.GetCareerInterviews();
            var today = DateTime.Today;
            var interviewsToday = careerInterviews.Where(ci => ci.InterviewDate.HasValue && ci.InterviewDate.Value.Date == today);
            return Ok(interviewsToday);
        }


        [HttpPost]
        public IActionResult AddCareerInterview(CareerInterviewAddDTO careerInterview)
        {
            var emp = _careerInterviewManager.Add(careerInterview);
            if (emp != null)
            {
                return Ok(emp);
            }
            return BadRequest("Error Adding CareerInterview");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCareerInterview(string id)
        {
            CareerInterviewDTO? CareerInterview = _careerInterviewManager.GetCareerInterview(id);
            if (CareerInterview == null)
            {
                return NotFound("CareerInterview not found");
            }
            _careerInterviewManager.Delete(id);
            return Ok("CareerInterview " + id + " has been deleted successfully");
        }

        [HttpPut("{id}")]
        public ActionResult<CareerInterview> UpdateCareerInterview(string id, CareerInterviewDTO CareerInterview)
        {
            if (id != CareerInterview.careerInterviewId)
            {
                return BadRequest();
            }
            var result =_careerInterviewManager.Update(CareerInterview);
            var std = _careerInterviewManager.GetCareerInterview(id);
            return Ok(result?std:"Error during Updating");
        }

        [HttpPut("status-update/{id}")]
        public ActionResult<CareerInterview> UpdateCareerInterviewStatus(string id, string newStatus)
        {
            if (id == null)
            {
                return BadRequest("ID can't be null");
            }
            var emp = _careerInterviewManager.GetCareerInterview(id);
            if (emp == null)
            {
                return BadRequest("Employee not found");
            }
            emp.Status = newStatus;
            var result = _careerInterviewManager.Update(emp);
            return Ok(result?emp:"Error updating status");
        }
        [HttpPut("comment-update/{id}")]
        public ActionResult<CareerInterview> commentUpdate(string id, string comment)
        {
            if (id == null)
            {
                return BadRequest("ID can't be null");
            }
            var emp = _careerInterviewManager.GetCareerInterview(id);
            if (emp == null)
            {
                return BadRequest("Employee not found");
            }
            emp.comment = comment;
            var result = _careerInterviewManager.Update(emp);
            return Ok(result?emp:"Error updating comment");
        }
        [HttpPut("interview-date-update/{id}")]
        public ActionResult<CareerInterview> interviewDateUpdate(string id, DateTime interviewDate)
        {
            if (id == null)
            {
                return BadRequest("ID can't be null");
            }
            var emp = _careerInterviewManager.GetCareerInterview(id);
            if (emp == null)
            {
                return BadRequest("Employee not found");
            }
            emp.InterviewDate = interviewDate;
            var result = _careerInterviewManager.Update(emp);
            return Ok(result ? emp : "Error updating interview date");
        }
    }
}
