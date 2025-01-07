using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiddyRanchWeb.BL
{
    public class BranchDTO
    {
        public string branchId { get; set; }
        public string branchName { get; set; }
        public string? locationDescription { get; set; }
        public string? locationLink { get; set; }
        public string? whatsAppLink { get; set; }
        public string whatsAppNum { get; set; }
        public string? slogan { get; set; }
        public string? facebook { get; set; }
        public string? instagram { get; set; }
        public string? tiktok { get; set; }
        public string logo1 { get; set; }
        public string? logo2 { get; set; }
        public string color1 { get; set; }
        public string color2 { get; set; }
        public string? color3 { get; set; }
        public DateTime startedAt { get; set; }
        public string? payment { get; set; }

    }
}
