using KiddyRanchWeb.DAL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiddyRanchWeb.BL
{
    public class CareerInterviewManager :ICareerInterviewManager
    {
        private readonly ICareerInterviewRepo _careerInterviewRepo;

        public CareerInterviewManager(ICareerInterviewRepo careerInterviewRepo)
        {
            _careerInterviewRepo = careerInterviewRepo;
        }
        public IEnumerable<CareerInterviewDTO> GetPaginationCareerInterviews(string searchTerm, string statusFilter, string sortBy, string sortOrder, int pageNumber, int pageSize)
        {
            IEnumerable<CareerInterview> careerInterviewsDB = _careerInterviewRepo.GetCareerInterviews();

            // Search filter
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                var searchLower = searchTerm.ToLower(); // Convert search term to lower case

                careerInterviewsDB = careerInterviewsDB
                    .Where(ci => ci.FirstName.ToLower().Contains(searchLower) ||
                                 ci.LastName.ToLower().Contains(searchLower) ||
                                 ci.PhoneNumber.ToLower().Contains(searchLower) ||
                                 ci.Education.ToLower().Contains(searchLower));
            }

            if (statusFilter != "All")
            {
                careerInterviewsDB = careerInterviewsDB.Where(ci => ci.Status == statusFilter);
            }

            switch (sortBy.ToLower())
            {
                case "interviewdate":
                    careerInterviewsDB = sortOrder.ToLower() == "asc" ?
                        careerInterviewsDB.OrderBy(ci => ci.InterviewDate) :
                        careerInterviewsDB.OrderByDescending(ci => ci.InterviewDate);
                    break;
                case "name":
                    careerInterviewsDB = sortOrder.ToLower() == "asc" ?
                        careerInterviewsDB.OrderBy(ci => ci.FirstName).ThenBy(ci => ci.LastName) :
                        careerInterviewsDB.OrderByDescending(ci => ci.FirstName).ThenByDescending(ci => ci.LastName);
                    break;
                case "age":
                    careerInterviewsDB = sortOrder.ToLower() == "asc" ?
                        careerInterviewsDB.OrderBy(ci => CalculateAge(ci.Birthday)) :
                        careerInterviewsDB.OrderByDescending(ci => CalculateAge(ci.Birthday));
                    break;
                default: // Default sort by creationDate
                    careerInterviewsDB = sortOrder.ToLower() == "asc" ?
                        careerInterviewsDB.OrderBy(ci => ci.creationDate) :
                        careerInterviewsDB.OrderByDescending(ci => ci.creationDate);
                    break;
            }

            // Pagination
            return careerInterviewsDB
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(u => new CareerInterviewDTO
                {
                    careerInterviewId = u.careerInterviewId,
                    CvDescription = u.CvDescription,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    JobTitle = u.JobTitle,
                    NID = u.NID,
                    InterviewDate = u.InterviewDate,
                    PhoneNumber = u.PhoneNumber,
                    WhatsApp = u.WhatsApp,
                    Adderss = u.Adderss,
                    Education = u.Education,
                    Birthday = u.Birthday,
                    Age = CalculateAge(u.Birthday),
                    Status = u.Status!,
                    creationDate = u.creationDate,
                    updateDate = u.updateDate,
                    comment = u.comment,
                });
        }





        public IEnumerable<CareerInterviewDTO> GetCareerInterviews()
        {
            IEnumerable<CareerInterview> careerInterviewsDB = _careerInterviewRepo.GetCareerInterviews();
            return careerInterviewsDB.Select(u => new CareerInterviewDTO
            {
                careerInterviewId = u.careerInterviewId,
                CvDescription = u.CvDescription,
                FirstName = u.FirstName,
                LastName = u.LastName,
                JobTitle = u.JobTitle,
                NID = u.NID,
                InterviewDate = u.InterviewDate,
                PhoneNumber = u.PhoneNumber,
                WhatsApp = u.WhatsApp,
                Adderss = u.Adderss,
                Education = u.Education,
                Birthday = u.Birthday,
                Age = CalculateAge(u.Birthday),
                Status = u.Status!,
                creationDate = u.creationDate,
                updateDate = u.updateDate,
                comment = u.comment,
            }).OrderByDescending(u=>u.updateDate);
        }
        
        private int CalculateAge(DateTime birthday)
        {
            DateTime today = DateTime.Today;
            int age = today.Year - birthday.Year;
            if (birthday.Date > today.AddYears(-age)) age--;
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

        public string Add(CareerInterviewAddDTO u)
        {
            CareerInterview careerInterview = new CareerInterview
            {
                careerInterviewId = Guid.NewGuid().ToString(),
                CvDescription = u.CvDescription,
                FirstName = u.FirstName,
                LastName = u.LastName,
                JobTitle = u.JobTitle,
                NID = u.NID,
                InterviewDate = u.interviewDate,
                PhoneNumber = u.PhoneNumber,
                WhatsApp = u.WhatsApp,
                Adderss = u.Adderss,
                Birthday = u.Birthday,
                Education = u.Education,
                Status = "Waiting",
                creationDate = GetEgyptTime(),
                updateDate = GetEgyptTime(),
            };
            _careerInterviewRepo.Add(careerInterview);
            _careerInterviewRepo.SaveChanges();
            return careerInterview.careerInterviewId;
        }

        public bool Delete(string id)
        {
            CareerInterview? careerInterview = _careerInterviewRepo.GetById(id);
            if (careerInterview == null)
            {
                return false;
            }
            _careerInterviewRepo.Delete(careerInterview);
            _careerInterviewRepo.SaveChanges();
            return true;
        }

        public CareerInterviewDTO GetCareerInterview(string id)
        {
            CareerInterview? careerInterview = _careerInterviewRepo.GetById(id);
            if (careerInterview is null)
            {
                return null;
            }

            return new CareerInterviewDTO
            {
                careerInterviewId = careerInterview.careerInterviewId,
                CvDescription = careerInterview.CvDescription,
                FirstName = careerInterview.FirstName,
                LastName = careerInterview.LastName,
                JobTitle = careerInterview.JobTitle,
                NID = careerInterview.NID,
                InterviewDate = careerInterview.InterviewDate,
                PhoneNumber = careerInterview.PhoneNumber,
                WhatsApp = careerInterview.WhatsApp,
                Adderss = careerInterview.Adderss,
                Birthday = careerInterview.Birthday,
                Education = careerInterview.Education,
                Age = CalculateAge(careerInterview.Birthday),
                creationDate = careerInterview.creationDate,
                updateDate = careerInterview.updateDate,
                Status = careerInterview.Status!,
                comment= careerInterview.comment,
            };
        }


        public bool Update(CareerInterviewDTO careerInterviewRequest)
        {
            if (careerInterviewRequest.careerInterviewId == null)
            {
                return false;
            }
            CareerInterview? careerInterview = _careerInterviewRepo.GetById(careerInterviewRequest.careerInterviewId);
            if (careerInterview is null)
            {
                return false;
            }
            careerInterview.CvDescription = careerInterviewRequest.CvDescription;
            careerInterview.FirstName = careerInterviewRequest.FirstName;
            careerInterview.LastName = careerInterviewRequest.LastName;
            careerInterview.PhoneNumber = careerInterviewRequest.PhoneNumber;
            careerInterview.WhatsApp = careerInterviewRequest.WhatsApp;
            careerInterview.InterviewDate = careerInterviewRequest.InterviewDate;
            careerInterview.NID = careerInterviewRequest.NID;
            careerInterview.Adderss = careerInterviewRequest.Adderss;
            careerInterview.JobTitle = careerInterviewRequest.JobTitle;
            careerInterview.Education = careerInterviewRequest.Education;
            careerInterview.Birthday = careerInterviewRequest.Birthday;
            careerInterview.updateDate = GetEgyptTime();
            careerInterview.Status = careerInterviewRequest.Status;
            careerInterview.comment = careerInterviewRequest.comment;
            _careerInterviewRepo.Update(careerInterview);
            _careerInterviewRepo.SaveChanges();
            return true;

        }
    }
}
