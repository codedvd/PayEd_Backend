using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayED.Data.Models
{
    public class Users : IdentityUser
    {
        [Key]
        public Guid User_id { get; set; }
    }
}
