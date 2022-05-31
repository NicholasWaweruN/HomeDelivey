
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.ViewModels
{
    public class Response
    {
        public string Token { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public DateTime Expiration { get; set; }
        public string StatusMessage { get; set; } = string.Empty;
    }
}
