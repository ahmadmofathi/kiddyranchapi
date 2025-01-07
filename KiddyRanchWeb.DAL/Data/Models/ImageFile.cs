using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiddyRanchWeb.DAL
{
    public class ImageFile
    {
        public string imageFileId {  get; set; }
        public string imgName { get; set; }
        public string imgDescription { get; set; }
        public string imgPath { get; set; }
        public string sectionId { get; set; }
        public Section section { get; set; }

    }
}
