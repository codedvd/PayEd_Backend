using PayEd.Data.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayEd.Data.Models
{
    public class Expenses : BaseEntity
    {
        [Key]
        public Guid Expense_Id { get; set; }
        public string Description { get; set; }
        public double Amount { get; set; }
        public string? Category { get; set; }

        public Guid BudgetId { get; set; } 
        public Budgets Budget { get; set; }
    }
}
