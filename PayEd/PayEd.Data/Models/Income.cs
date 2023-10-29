using PayEd.Data.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayEd.Data.Models
{
    public class Income : BaseEntity
    {
        [Key]
        public Guid Income_Id { get; set; }
        public string Description { get; set; }
        public double Amount { get; set; }

        public Guid StreamId { get; set; } // Foreign Key to Streams
        public Streams Stream { get; set; }

        public Guid UserId { get; set; } // Foreign Key to User
        public User User { get; set; }
    }

}
