using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiddyRanchWeb.BL
{
    public class SectionUpdateDTO
    {
        public string sectionId { get; set; }
        public string sectionTitle { get; set; }
        public string? sectionSubtitle { get; set; }
        public string sectionDescription { get; set; }
        public string styleCode { get; set; }
    }
}
