using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiddyRanchWeb.BL
{
    public class IncomeReadDTO
    {
        public string? IncomeId { get; set; }
        public string? IncomeName { get; set; }
        public string? IncomeType { get; set; }
        public string? IncomeDescription { get; set; }
        public string? PaymentType { get; set; }
        public DateTime? PaymentDate { get; set; }
        public int? PaymentAmount { get; set; }
        public DateTime? nextPaymentDate { get; set; }
        public int? TotalPaymentAmount { get; set; }
        public int? RemainingAmount { get; set; }
        public DateTime? RemainingDeadlineDate { get; set; }
        public string? Branch { get; set; }

    }
}
