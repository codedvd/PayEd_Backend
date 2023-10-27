using PayEd.Data.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayEd.Data.Models
{
    public class User : IdentityUser
    {
        public Guid User_id { get; set; }
        public Usertype User_type { get; set; }
        public string Fullname { get; set; }
        public string Phone_number { get; set; }
    }

}
