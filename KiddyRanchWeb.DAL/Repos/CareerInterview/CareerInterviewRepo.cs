using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiddyRanchWeb.DAL
{
    public class CareerInterviewRepo : ICareerInterviewRepo
    {
        private readonly AppDbContext _context;

        public CareerInterviewRepo(AppDbContext context)
        {
            _context = context;
        }
        public List<CareerInterview> GetCareerInterviews()
        {
            return _context.Set<CareerInterview>().ToList();
        }
        public CareerInterview? GetById(string id)
        {
            return _context.Set<CareerInterview>().Find(id);
        }
        public void Add(CareerInterview careerInterview)
        {
            _context.Set<CareerInterview>().Add(careerInterview);
        }

        public void Delete(CareerInterview careerInterview)
        {
            _context.Set<CareerInterview>().Remove(careerInterview);
        }
        public void Update(CareerInterview careerInterview)
        {
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

    }
}
