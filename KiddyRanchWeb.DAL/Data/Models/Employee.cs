using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiddyRanchWeb.DAL
{
    public class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string EmployeeId { get; set; }
        public string Name {  get; set; }
        public string Role { get; set; }
        public string bioDescription { get; set; }
        public string? profilePic {  get; set; } 

    }
}
