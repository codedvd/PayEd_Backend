using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayED.Data.Models
{
    public class Budgets : BaseEntity
    {
        [Key]
        public Guid Budget_id { get; set; }
        public string Budget_name { get; set; }
        public double Initial_balance { get; set; }
        public Users User_id { get; set; }
    }
}
