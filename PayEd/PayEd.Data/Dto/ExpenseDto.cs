using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayEd.Data.Dto
{
    public class ExpenseDto
    {
        public string? Description { get; set; }
        public double Amount { get; set; }
        public string Category { get; set; }
    }
}
