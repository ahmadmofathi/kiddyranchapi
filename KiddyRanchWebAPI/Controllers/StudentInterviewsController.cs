using ClosedXML.Excel;
using KiddyRanchWeb.BL;
using KiddyRanchWeb.DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;

namespace KiddyRanchWeb.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentInterviewsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IStudentInterviewManager _studentInterviewManager;
        public StudentInterviewsController(AppDbContext context, IStudentInterviewManager studentInterviewManager)
        {
            _context = context;
            _studentInterviewManager = studentInterviewManager;
        }

        [HttpGet("export")]
        public IActionResult ExportToExcel()
        {
            var studentInterviews = _studentInterviewManager.GetStudentInterviews().ToList();

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("StudentInterviews");

                // Add headers
                worksheet.Cell(1, 1).Value = "Student Interview ID";
                worksheet.Cell(1, 2).Value = "Student Name";
                worksheet.Cell(1, 3).Value = "Father Name";
                worksheet.Cell(1, 4).Value = "Father Job";
                worksheet.Cell(1, 5).Value = "Mother Name";
                worksheet.Cell(1, 6).Value = "Mother Job";
                worksheet.Cell(1, 7).Value = "Birthday";
                worksheet.Cell(1, 8).Value = "Age";
                worksheet.Cell(1, 9).Value = "Parent NID";
                worksheet.Cell(1, 10).Value = "Phone Number 1";
                worksheet.Cell(1, 11).Value = "Phone Number 2";
                worksheet.Cell(1, 12).Value = "WhatsApp";
                worksheet.Cell(1, 13).Value = "Address";
                worksheet.Cell(1, 14).Value = "Wanted Course";
                worksheet.Cell(1, 15).Value = "Stage To Join";
                worksheet.Cell(1, 16).Value = "Branch";
                worksheet.Cell(1, 17).Value = "Creation Date";
                worksheet.Cell(1, 18).Value = "Update Date";
                worksheet.Cell(1, 19).Value = "Comment";
                worksheet.Cell(1, 20).Value = "Status";
                worksheet.Cell(1, 21).Value = "Interview Date";

                // Write data
                int row = 2;
                foreach (var interview in studentInterviews)
                {
                    worksheet.Cell(row, 1).Value = interview.StudentInterviewId;
                    worksheet.Cell(row, 2).Value = interview.StudentName;
                    worksheet.Cell(row, 3).Value = interview.FatherName;
                    worksheet.Cell(row, 4).Value = interview.FatherJob;
                    worksheet.Cell(row, 5).Value = interview.MotherName;
                    worksheet.Cell(row, 6).Value = interview.MotherJob ?? string.Empty;
                    worksheet.Cell(row, 7).Value = interview.Birthday.ToString("yyyy-MM-dd");
                    worksheet.Cell(row, 8).Value = interview.Age;
                    worksheet.Cell(row, 9).Value = interview.ParentNID;
                    worksheet.Cell(row, 10).Value = interview.PhoneNumber1;
                    worksheet.Cell(row, 11).Value = interview.PhoneNumber2 ?? string.Empty;
                    worksheet.Cell(row, 12).Value = interview.WhatsApp;
                    worksheet.Cell(row, 13).Value = interview.Address ?? string.Empty;
                    worksheet.Cell(row, 14).Value = interview.WantedCourse ?? string.Empty;
                    worksheet.Cell(row, 15).Value = interview.StageToJoin ?? string.Empty;
                    worksheet.Cell(row, 16).Value = interview.branch;
                    worksheet.Cell(row, 17).Value = interview.creationDate?.ToString("yyyy-MM-dd") ?? string.Empty;
                    worksheet.Cell(row, 18).Value = interview.updateDate?.ToString("yyyy-MM-dd") ?? string.Empty;
                    worksheet.Cell(row, 19).Value = interview.comment ?? string.Empty;
                    worksheet.Cell(row, 20).Value = interview.status;
                    worksheet.Cell(row, 21).Value = interview.interviewDate?.ToString("yyyy-MM-dd") ?? string.Empty;
                    row++;
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "StudentInterviews.xlsx");
                }
            }
        }



        [HttpGet("birthdays")]
        public async Task<IActionResult> UpcomingBirthdays(int noOfDays, int pageNumber = 1, int pageSize = 10)
        {
            var today = DateTime.Today;
            var next30Days = today.AddDays(noOfDays);

            // Retrieve users whose DateOfBirth is not null or empty
            var users = _studentInterviewManager.GetStudentInterviews()
                .Where(u => u.Birthday != null)
                .ToList();

            var upcomingBirthdays = users
                .Where(u => IsBirthdayInNext30Days(u.Birthday, today, next30Days))
                .OrderByDescending(u => u.Birthday)
                .ToList();

            // Calculate total count for pagination
            var totalCount = upcomingBirthdays.Count;

            // Apply pagination
            var paginatedBirthdays = upcomingBirthdays
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            // Calculate total pages
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

            return Ok(new
            {
                TotalCount = totalCount,
                TotalPages = totalPages,
                PageNumber = pageNumber,
                PageSize = pageSize,
                Data = paginatedBirthdays
            });
        }

        private bool IsBirthdayInNext30Days(DateTime dateOfBirth, DateTime today, DateTime next30Days)
        {
            var monthDay = new DateTime(today.Year, dateOfBirth.Month, dateOfBirth.Day);

            // Check if the birthday this year is within the next 30 days
            if (monthDay < today)
            {
                monthDay = monthDay.AddYears(1);
            }

            return monthDay >= today && monthDay <= next30Days;
        }
        //[Authorize]
        [HttpGet]
        public IActionResult GetAllCareerInterviews(
                        string searchTerm = "",
                        int pageNumber = 1,
                        int pageSize = 10,
                        string sortBy = "", // Parameter for sorting
                        string sortDirection = "", // Parameter for sort direction
                        string statusFilter = "All", // Parameter for status filtering
                        string branchFilter = "All" // Added parameter for branch filtering
        )
        {
            // Get filtered and paginated interviews
            var careerInterviews = _studentInterviewManager.GetPaginationStudentInterviews(
                searchTerm, pageNumber, pageSize, sortBy, sortDirection, statusFilter, branchFilter);

            var searchLower = searchTerm.ToLower();

            // Calculate total count with filters applied
            var totalCount = _studentInterviewManager.GetStudentInterviews()
                .Where(ci =>
                    (string.IsNullOrWhiteSpace(searchTerm) ||
                     ci.StudentName.ToLower().Contains(searchLower) ||
                     ci.FatherName.ToLower().Contains(searchLower) ||
                     ci.ParentNID.ToLower().Contains(searchLower) ||
                     ci.PhoneNumber1.ToLower().Contains(searchLower) ||
                     (ci.PhoneNumber2 != null && ci.PhoneNumber2.ToLower().Contains(searchLower))) &&
                    (statusFilter == "All" || ci.status.Equals(statusFilter, StringComparison.OrdinalIgnoreCase)) &&
                    (branchFilter == "All" || ci.branch.Equals(branchFilter, StringComparison.OrdinalIgnoreCase))
                )
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



        [HttpGet("stats")]
        public IActionResult GetStats()
        {
            var studentInterviews = _studentInterviewManager.GetStudentInterviews();

            var totalStudents = studentInterviews.Count();
            var studentsByStageToJoin = studentInterviews
                .GroupBy(s => s.StageToJoin)
                .Select(group => new { StageToJoin = group.Key, Count = group.Count() })
                .ToList();
            var studentsByBranch = studentInterviews
                .GroupBy(s => s.branch)
                .Select(group => new { Branch = group.Key, Count = group.Count() })
                .ToList();
            var studentsByStatus = studentInterviews
                .GroupBy(s => s.status)
                .Select(group => new { Status = group.Key, Count = group.Count() })
                .ToList();

            var currentMonth = DateTime.Now.Month;
            var currentYear = DateTime.Now.Year;
            var studentsWithInterviewsThisMonth = studentInterviews
                .Where(s => s.interviewDate.HasValue &&
                             s.interviewDate.Value.Month == currentMonth &&
                             s.interviewDate.Value.Year == currentYear)
                .Count();

            return Ok(new
            {
                TotalStudents = totalStudents,
                StudentsByStageToJoin = studentsByStageToJoin,
                StudentsByBranch = studentsByBranch,
                StudentsByStatus = studentsByStatus,
                StudentsWithInterviewsThisMonth = studentsWithInterviewsThisMonth
            });
        }



        [HttpGet("{id}")]
        public IActionResult GetStudentInterviewById(string id)
        {
            var StudentInterview = _studentInterviewManager.GetStudentInterview(id);
            if (StudentInterview == null)
            {
                return BadRequest("StudentInterview not found");
            }
            return Ok(StudentInterview);
        }
        [HttpPost]
        public IActionResult AddStudentInterview(StudentInterviewAddDTO studentInterview)
        {
            var emp = _studentInterviewManager.Add(studentInterview);
            if (emp != null)
            {
                return Ok(emp);
            }
            return BadRequest("Error Adding StudentInterview");
        }
        [HttpGet("isToday")]
        public IActionResult TodayInterviews()
        {
            var careerInterviews = _studentInterviewManager.GetStudentInterviews();
            var today = DateTime.Today;
            var interviewsToday = careerInterviews.Where(ci => ci.interviewDate.HasValue && ci.interviewDate.Value.Date == today);
            return Ok(interviewsToday);
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteStudentInterview(string id)
        {
            StudentInterviewDTO? StudentInterview = _studentInterviewManager.GetStudentInterview(id);
            if (StudentInterview == null)
            {
                return NotFound("StudentInterview not found");
            }
            _studentInterviewManager.Delete(id);
            return Ok("StudentInterview " + id + " has been deleted successfully");
        }

        [HttpPut("{id}")]
        public ActionResult<StudentInterview> UpdateStudentInterview(string id, StudentInterviewDTO StudentInterview)
        {
            if (id != StudentInterview.StudentInterviewId)
            {
                return BadRequest();
            }
            var result = _studentInterviewManager.Update(StudentInterview);
            var std = _studentInterviewManager.GetStudentInterview(id);

            return Ok(result?std:"Error updating student");
        }


        [HttpPut("status-update/{id}")]
        public ActionResult<StudentInterview> UpdateStudentInterviewStatus(string id, string newStatus)
        {
            if (id == null)
            {
                return BadRequest("ID can't be null");
            }
            var std = _studentInterviewManager.GetStudentInterview(id);
            if(std == null)
            {
                return BadRequest("Student not found");
            }
            std.status = newStatus; 
            var result = _studentInterviewManager.Update(std);
            return Ok(result?std:"Student update result an unknown error");
        }

        [HttpPut("status-update-all")]
        public ActionResult<StudentInterview> UpdateAllStudentInterviewStatus(string newStatus)
        {

            var stds = _studentInterviewManager.GetStudentInterviews();
            foreach (var std in stds)
            {
                if(std.status != newStatus)
                {
                    std.status = newStatus;
                    _studentInterviewManager.Update(std);
                }
            }
            return NoContent();
        }

        [HttpPut("interview-date-update/{id}")]
        public ActionResult<StudentInterview> interviewDateUpdate(string id, DateTime newStatus)
        {
            if (id == null)
            {
                return BadRequest("ID can't be null");
            }
            var std = _studentInterviewManager.GetStudentInterview(id);
            if (std == null)
            {
                return BadRequest("Student not found");
            }
            std.interviewDate = newStatus;
            _studentInterviewManager.Update(std);
            return NoContent();
        }

        [HttpPut("comment-update/{id}")]
        public ActionResult<StudentInterview> commentUpdate(string id, string comment)
        {
            if (id == null)
            {
                return BadRequest("ID can't be null");
            }
            var std = _studentInterviewManager.GetStudentInterview(id);
            if (std == null)
            {
                return BadRequest("Employee not found");
            }
            std.comment = comment;
            var result = _studentInterviewManager.Update(std);
            return Ok(result?std:"Error update comment");
        }
    }
}
