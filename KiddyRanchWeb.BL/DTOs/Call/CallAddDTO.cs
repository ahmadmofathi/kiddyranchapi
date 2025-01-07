using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiddyRanchWeb.BL
{
    public class CallAddDTO
    {
        public string callerName { get; set; }
        public string calledId { get; set; }
        public string callDescription { get; set; }
        public string callDate { get; set; }
    }
}
