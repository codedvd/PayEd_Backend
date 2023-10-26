﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayED.Data.Models
{
    public class Expenses : BaseEntity
    {
        [Key]
        public Guid Expense_id { get; set; }
        public DateTime Expense_date { get; set; }
        public string Description { get; set; }
        public double Amount { get; set; }
        public string Category { get; set; }
        public Budgets Budget_id { get; set; }
    }
}
