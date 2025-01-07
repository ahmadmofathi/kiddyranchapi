using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiddyRanchWeb.BL
{
    public class EmployeeDTO
    {
        public string EmployeeId { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public string bioDescription { get; set; }
        public string? profilePic { get; set; }

    }
}
