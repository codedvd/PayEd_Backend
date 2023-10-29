using PayEd.Data.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayEd.Data.Models
{
    public class User : IdentityUser<Guid>
    {
        public Guid User_Id { get; set; }
        public Usertype User_type { get; set; }
        public string School_name { get; set; }
        public string Fullname { get; set; }
        public string Address { get; set; }

        public ICollection<Streams> Streams { get; set; }
        public ICollection<Income> Income { get; set; }
        public ICollection<Budgets> Budgets { get; set; }
    }
}
