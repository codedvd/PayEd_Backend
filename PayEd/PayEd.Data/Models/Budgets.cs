using PayEd.Data.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayEd.Data.Models
{
    public class Budgets : BaseEntity
    {
        [Key]
        public Guid Budget_Id { get; set; }
        public string Budget_name { get; set; }
        public double Initial_balance { get; set; }

        public Guid UserId { get; set; } // Foreign Key to User
        public User User { get; set; }

        public ICollection<Expenses> Expenses { get; set; }
    }
}
