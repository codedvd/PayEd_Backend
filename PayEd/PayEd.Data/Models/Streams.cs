using PayEd.Data.Common;
using PayEd.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayEd.Data.Models
{
    public class Streams : BaseEntity
    {
        [Key]
        public Guid Stream_Id { get; set; }
        public string Stream_name { get; set; }
        public string Description { get; set; }
        public string? StreamCode { get; set; }

        public Guid UserId { get; set; } // Foreign Key to User
        public User User { get; set; }

        public ICollection<Income> Income { get; set; }
    }
}
