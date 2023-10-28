using PayEd.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayEd.Data.Models
{
    public class Streams
    {
        [Key]
        public Guid Stream_Id { get; set; }
        public Stream_name Stream { get; set; }
        public string? Description { get; set; }

        public Guid User_Id { get; set; }
        public User User { get; set; }
    }

}
