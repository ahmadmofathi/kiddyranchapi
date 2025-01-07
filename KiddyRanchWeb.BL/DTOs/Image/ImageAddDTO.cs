using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiddyRanchWeb.BL
{
    public class ImageAddDTO
    {
        public string imgName { get; set; }
        public string imgDescription { get; set; }
        public string? imgPath { get; set; }
        public string sectionId { get; set; }
    }
}
