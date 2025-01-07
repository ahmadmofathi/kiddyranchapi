using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiddyRanchWeb.BL
{
    public class TokenDTO
    {
        public string Token { get; set; } = string.Empty;
        public DateTime ExpiryDate { get; set; }

        public string? Username { get; set; } 
        public string? User_id { get; set; }
    }
}
