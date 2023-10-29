using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayEd.Data.Dto
{
    public class Response
    {
        public Guid UserId { get; set; }
        public string Fullname { get; set; }
        public string Token { get; set; }
    }
}
