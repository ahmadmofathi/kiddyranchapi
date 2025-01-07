using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiddyRanchWeb.DAL
{
    public interface ICareerInterviewRepo
    {
        List<CareerInterview> GetCareerInterviews();
        CareerInterview? GetById(string id);
        void Add(CareerInterview careerInterview);
        void Update(CareerInterview careerInterview);
        void Delete(CareerInterview careerInterview);
        int SaveChanges();
    }
}
