using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiddyRanchWeb.BL
{
    public interface IStudentInterviewManager
    {
        IEnumerable<StudentInterviewDTO> GetPaginationStudentInterviews(string searchTerm,
                int pageNumber,
                int pageSize,
                string sortBy,
                string sortDirection,
                string statusFilter,
                string branchFilter);
        IEnumerable<StudentInterviewDTO> GetStudentInterviews();
        StudentInterviewDTO GetStudentInterview(string id);
        string Add(StudentInterviewAddDTO studentInterview);
        bool Update(StudentInterviewDTO studentInterview);
        bool Delete(string id);
    }
}
