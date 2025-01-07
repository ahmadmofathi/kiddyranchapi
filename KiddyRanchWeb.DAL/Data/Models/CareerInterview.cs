using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiddyRanchWeb.DAL
{
    public class CareerInterview
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string careerInterviewId {  get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string JobTitle { get; set; }
        public string NID { get; set; }
        public string Adderss {  get; set; }
        public string PhoneNumber { get; set; }
        public DateTime Birthday { get; set; }
        public string WhatsApp {  get; set; }
        public string CvDescription { get; set; }
        public string Education { get; set; }
        public DateTime? InterviewDate {  get; set; }
        public string? Status { get; set; }
        public DateTime? creationDate {  get; set; }
        public DateTime? updateDate { get; set; }
        public string? comment { get; set; }
    }
}
