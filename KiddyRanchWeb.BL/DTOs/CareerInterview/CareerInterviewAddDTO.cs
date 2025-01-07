using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiddyRanchWeb.BL
{
    public class CareerInterviewAddDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string JobTitle { get; set; }
        public string NID { get; set; }
        public string PhoneNumber { get; set; }
        public string WhatsApp { get; set; }
        public string CvDescription { get; set; }
        public string Adderss { get; set; }
        public string Education { get; set; }
        public DateTime Birthday { get; set; }
        public DateTime? interviewDate { get; set; }
    }
}
