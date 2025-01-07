using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiddyRanchWeb.DAL
{
    public interface IStudentInterviewRepo
    {
        List<StudentInterview> GetStudentInterviews();
        StudentInterview? GetById(string id);
        void Add(StudentInterview studentInterview);
        void Update(StudentInterview studentInterview);
        void Delete(StudentInterview studentInterview);
        int SaveChanges();
    }
}
