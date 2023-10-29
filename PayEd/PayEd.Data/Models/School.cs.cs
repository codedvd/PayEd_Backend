using Microsoft.AspNetCore.Identity;
using PayEd.Data.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayEd.Data.Models
{
    public class School : IdentityUser<Guid>
    {
        [Key]
        public Guid School_Id { get; set; }
        public string School_name { get; set; }
        public string School_email { get; set; }
        public bool Verified { get; set; }
    }
}
