using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayED.Data.Models
{
    public class Streams : BaseEntity
    {
        [Key]
        public Guid Stream_id { get; set; }
        public string Stream_name { get; set; }
        public string Description { get; set; }
        public Users User_id { get; set; }
    }
}
