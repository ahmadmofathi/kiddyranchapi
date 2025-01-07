using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiddyRanchWeb.BL
{
    public interface ICareerInterviewManager
    {
        IEnumerable<CareerInterviewDTO> GetPaginationCareerInterviews(string searchTerm, string statusFilter, string sortBy, string sortOrder, int pageNumber, int pageSize);

        IEnumerable<CareerInterviewDTO> GetCareerInterviews();
        CareerInterviewDTO GetCareerInterview(string id);
        string Add(CareerInterviewAddDTO careerInterview);
        bool Update(CareerInterviewDTO careerInterview);
        bool Delete(string id);
    }
}
