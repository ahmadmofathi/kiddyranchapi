﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace KiddyRanchWeb.DAL
{
    public class StudentInterview
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string StudentInterviewId { get; set; }
        public string StudentName { get; set; }
        public string FatherName { get; set; }
        public string FatherJob { get; set; }
        public string MotherName { get; set; }
        public string? MotherJob { get; set; }
        public DateTime Birthday { get; set; }
        public string? ParentNID { get; set; }
        public string PhoneNumber1 { get; set; }
        public string? PhoneNumber2 { get; set; }
        public string WhatsApp { get; set; }
        public string? Address { get; set; }
        public string? WantedCourse { get; set; }
        public string? StageToJoin { get; set; }
        public string branch { get; set; }
        public string status {  get; set; }
        public DateTime? creationDate { get; set; }
        public DateTime? updateDate { get; set; }
        public string? comment { get; set; }
        public DateTime? interviewDate { get; set; }

    }
}
