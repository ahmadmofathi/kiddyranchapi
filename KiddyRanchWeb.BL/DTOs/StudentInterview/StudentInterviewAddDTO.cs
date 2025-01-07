using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiddyRanchWeb.BL
{
    public class StudentInterviewAddDTO
    {
        public string StudentName { get; set; }
        public string FatherName { get; set; }
        public string FatherJob { get; set; }
        public string MotherName { get; set; }
        public string? MotherJob { get; set; }
        public DateTime Birthday { get; set; }
        public DateTime? interviewDate { get; set; }
        public string ParentNID { get; set; }
        public string PhoneNumber1 { get; set; }
        public string? PhoneNumber2 { get; set; }
        public string WhatsApp { get; set; }
        public string? Address { get; set; }
        public string? WantedCourse { get; set; }
        public string? StageToJoin { get; set; }
        public string branch { get; set; }

    }
}
