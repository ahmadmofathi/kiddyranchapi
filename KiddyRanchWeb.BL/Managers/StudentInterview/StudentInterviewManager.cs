using Azure;
using KiddyRanchWeb.DAL;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiddyRanchWeb.BL
{
    public class StudentInterviewManager:IStudentInterviewManager
    {
        private readonly IStudentInterviewRepo _studentInterviewRepo;

        public StudentInterviewManager(IStudentInterviewRepo studentInterviewRepo)
        {
            _studentInterviewRepo = studentInterviewRepo;
        }
        private double CalculateAge(DateTime birthday)
        {


            DateTime today = DateTime.Today;

            int years = today.Year - birthday.Year;
            int months = today.Month - birthday.Month;
            int days = today.Day - birthday.Day;

            if (days < 0)
            {
                months--;
                days += DateTime.DaysInMonth(today.Year, today.Month - 1);
            }

            if (months < 0)
            {
                years--;
                months += 12;
            }

            double age = years + (months / 12.0);
            return age;
        }
        public DateTime GetEgyptTime()
        {
            // Get the Egypt Standard Time zone
            TimeZoneInfo egyptTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");

            // Convert current UTC time to Egypt time
            DateTime egyptTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, egyptTimeZone);

            return egyptTime;
        }


        public IEnumerable<StudentInterviewDTO> GetStudentInterviews()
        {
            IEnumerable<StudentInterview> studentInterviewsDB = _studentInterviewRepo.GetStudentInterviews();
            return studentInterviewsDB
                .Select(u => new StudentInterviewDTO
                {
                    StudentInterviewId = u.StudentInterviewId,
                    FatherName = u.FatherName,
                    MotherName = u.MotherName,
                    FatherJob = u.FatherJob,
                    MotherJob = u.MotherJob,
                    StudentName = u.StudentName,
                    ParentNID = u.ParentNID,
                    Birthday = u.Birthday,
                    PhoneNumber1 = u.PhoneNumber1,
                    PhoneNumber2 = u.PhoneNumber2,
                    WhatsApp = u.WhatsApp,
                    Address = u.Address,
                    WantedCourse = u.WantedCourse,
                    StageToJoin = u.StageToJoin,
                    branch = u.branch,
                    Age = CalculateAge(u.Birthday),
                    creationDate = u.creationDate,
                    updateDate = u.updateDate,
                    comment = u.comment,
                    status = u.status,
                    interviewDate = u.interviewDate,
                }).OrderByDescending(u => u.updateDate);
        }
        public IEnumerable<StudentInterviewDTO> GetPaginationStudentInterviews(
            string searchTerm,
            int pageNumber,
            int pageSize,
            string sortBy,
            string sortDirection,
            string statusFilter,
            string branchFilter)
        {
            var searchLower = searchTerm.ToLower();
            IEnumerable<StudentInterview> studentInterviewsDB = _studentInterviewRepo.GetStudentInterviews();

            // Filter by search term
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                studentInterviewsDB = studentInterviewsDB.Where(ci =>
                    ci.StudentName.ToLower().Contains(searchLower) ||
                    ci.FatherName.ToLower().Contains(searchLower) ||
                    ci.ParentNID.ToLower().Contains(searchLower) ||
                    ci.PhoneNumber1.ToLower().Contains(searchLower) ||
                    ci.PhoneNumber2?.ToLower().Contains(searchLower) == true);
            }

            // Filter by status
            if (statusFilter != "All")
            {
                studentInterviewsDB = studentInterviewsDB.Where(ci =>
                    ci.status.Equals(statusFilter, StringComparison.OrdinalIgnoreCase));
            }

            // Filter by branch
            if (branchFilter != "All")
            {
                studentInterviewsDB = studentInterviewsDB.Where(ci =>
                    ci.branch.Equals(branchFilter, StringComparison.OrdinalIgnoreCase));
            }

            // Sorting logic
            switch (sortBy.ToLower())
            {
                case "studentname":
                    studentInterviewsDB = sortDirection.ToLower() == "asc"
                        ? studentInterviewsDB.OrderBy(ci => ci.StudentName)
                        : studentInterviewsDB.OrderByDescending(ci => ci.StudentName);
                    break;

                case "age":
                    studentInterviewsDB = sortDirection.ToLower() == "asc"
                        ? studentInterviewsDB.OrderBy(ci => CalculateAge(ci.Birthday))
                        : studentInterviewsDB.OrderByDescending(ci => CalculateAge(ci.Birthday));
                    break;

                case "interviewdate":
                    studentInterviewsDB = sortDirection.ToLower() == "asc"
                        ? studentInterviewsDB.OrderBy(ci => ci.interviewDate)
                        : studentInterviewsDB.OrderByDescending(ci => ci.interviewDate);
                    break;

                case "branch":
                    studentInterviewsDB = sortDirection.ToLower() == "asc"
                        ? studentInterviewsDB.OrderBy(ci => ci.branch)
                        : studentInterviewsDB.OrderByDescending(ci => ci.branch);
                    break;

                default:
                    studentInterviewsDB = sortDirection.ToLower() == "asc"
                        ? studentInterviewsDB.OrderBy(ci => ci.creationDate)
                        : studentInterviewsDB.OrderByDescending(ci => ci.creationDate);
                    break;
            }

            return studentInterviewsDB
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(u => new StudentInterviewDTO
                {
                    StudentInterviewId = u.StudentInterviewId,
                    FatherName = u.FatherName,
                    MotherName = u.MotherName,
                    FatherJob = u.FatherJob,
                    MotherJob = u.MotherJob,
                    StudentName = u.StudentName,
                    ParentNID = u.ParentNID,
                    Birthday = u.Birthday,
                    PhoneNumber1 = u.PhoneNumber1,
                    PhoneNumber2 = u.PhoneNumber2,
                    WhatsApp = u.WhatsApp,
                    Address = u.Address,
                    WantedCourse = u.WantedCourse,
                    StageToJoin = u.StageToJoin,
                    branch = u.branch,
                    Age = CalculateAge(u.Birthday),
                    creationDate = u.creationDate,
                    updateDate = u.updateDate,
                    comment = u.comment,
                    status = u.status,
                    interviewDate = u.interviewDate,
                });
        }




        public string Add(StudentInterviewAddDTO u)
        {
            StudentInterview studentInterview = new StudentInterview
            {
                StudentInterviewId = Guid.NewGuid().ToString(),
                FatherJob = u.FatherJob,
                FatherName = u.FatherName,
                MotherName = u.MotherName,
                MotherJob = u.MotherJob,
                StudentName = u.StudentName,
                ParentNID = u.ParentNID,
                Birthday = u.Birthday,
                PhoneNumber1 = u.PhoneNumber1,
                PhoneNumber2 = u.PhoneNumber2,
                WhatsApp = u.WhatsApp,
                Address = u.Address,
                WantedCourse = u.WantedCourse,
                StageToJoin = u.StageToJoin,
                branch = u.branch,
                status = "Waiting",
                interviewDate = u.interviewDate,
                creationDate = GetEgyptTime(),
                updateDate = GetEgyptTime(),
            };
            _studentInterviewRepo.Add(studentInterview);
            _studentInterviewRepo.SaveChanges();
            return studentInterview.StudentInterviewId;
        }

        public bool Delete(string id)
        {
            StudentInterview? studentInterview = _studentInterviewRepo.GetById(id);
            if (studentInterview == null)
            {
                return false;
            }
            _studentInterviewRepo.Delete(studentInterview);
            _studentInterviewRepo.SaveChanges();
            return true;
        }

        public StudentInterviewDTO GetStudentInterview(string id)
        {
            StudentInterview? u = _studentInterviewRepo.GetById(id);
            if (u is null)
            {
                return null;
            }

            return new StudentInterviewDTO
            {
                StudentInterviewId = id,
                FatherName = u.FatherName,
                MotherName = u.MotherName,
                FatherJob = u.FatherJob,
                MotherJob = u.MotherJob,
                StudentName = u.StudentName,
                ParentNID = u.ParentNID,
                Birthday = u.Birthday,
                PhoneNumber1 = u.PhoneNumber1,
                PhoneNumber2 = u.PhoneNumber2,
                WhatsApp = u.WhatsApp,
                Address = u.Address,
                WantedCourse = u.WantedCourse,
                StageToJoin = u.StageToJoin,
                Age = CalculateAge(u.Birthday),
                branch = u.branch,
                creationDate = u.creationDate,
                updateDate = u.updateDate,
                status = u.status,
                interviewDate = u.interviewDate,
                comment = u.comment
            };
        }


        public bool Update(StudentInterviewDTO studentInterviewRequest)
        {
            if (studentInterviewRequest.StudentInterviewId == null)
            {
                return false;
            }
            StudentInterview? studentInterview = _studentInterviewRepo.GetById(studentInterviewRequest.StudentInterviewId);
            if (studentInterview is null)
            {
                return false;
            }
            studentInterview.FatherName = studentInterviewRequest.FatherName;
            studentInterview.FatherJob = studentInterviewRequest.FatherJob;
            studentInterview.MotherName = studentInterviewRequest.MotherName;
            studentInterview.MotherJob = studentInterviewRequest.MotherJob;
            studentInterview.ParentNID = studentInterviewRequest.ParentNID;
            studentInterview.StudentName = studentInterviewRequest.StudentName;
            studentInterview.WantedCourse = studentInterviewRequest.WantedCourse;
            studentInterview.PhoneNumber1 = studentInterviewRequest.PhoneNumber1;
            studentInterview.PhoneNumber2 = studentInterviewRequest.PhoneNumber2;
            studentInterview.Address = studentInterviewRequest.Address;
            studentInterview.WhatsApp = studentInterviewRequest.WhatsApp;
            studentInterview.Birthday = studentInterviewRequest.Birthday;
            studentInterview.StageToJoin = studentInterviewRequest.StageToJoin;
            studentInterview.branch = studentInterviewRequest.branch;
            studentInterview.updateDate = GetEgyptTime();
            studentInterview.status = studentInterviewRequest.status;
            studentInterview.interviewDate = studentInterviewRequest.interviewDate;
            studentInterview.comment = studentInterviewRequest.comment;
            _studentInterviewRepo.Update(studentInterview);
            _studentInterviewRepo.SaveChanges();
            return true;

        }
    }
}
