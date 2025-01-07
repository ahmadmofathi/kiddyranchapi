using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiddyRanchWeb.DAL
{
    public class StudentInterviewRepo : IStudentInterviewRepo
    {
        private readonly AppDbContext _context;

        public StudentInterviewRepo(AppDbContext context)
        {
            _context = context;
        }
        public List<StudentInterview> GetStudentInterviews()
        {
            return _context.Set<StudentInterview>().ToList();
        }
        public StudentInterview? GetById(string id)
        {
            return _context.Set<StudentInterview>().Find(id);
        }
        public void Add(StudentInterview user)
        {
            _context.Set<StudentInterview>().Add(user);
        }

        public void Delete(StudentInterview user)
        {
            _context.Set<StudentInterview>().Remove(user);
        }
        public void Update(StudentInterview user)
        {
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }
    }
}
