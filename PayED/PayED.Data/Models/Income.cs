using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayED.Data.Models
{
    public class Income : BaseEntity
    {
        [Key]
        public Guid Income_id { get; set; }
        public DateTime Income_date { get; set; }
        public string Description { get; set; }
        public double Amount { get; set; }
        public Streams Stream_id { get; set; }
        public Users User_id { get; set; }
    }
}
