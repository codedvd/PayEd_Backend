using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PayEd.Core.Services;
using PayEd.Data.AppContext;
using PayEd.Data.Dto;
using PayEd.Data.Models;
using PayEd.Infrastructure.Helpers;

namespace PayEd.Core.Implementation
{
    public class ExpenseRepository : IExpenseRepository
    {
        private readonly AppDbContext _context;
        public ExpenseRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<ApiResponse> CreateExpenseAsync(ExpenseDto expense)
        {
            var newExpense = new Expenses
            {
                Expense_Id = Guid.NewGuid(),
                Description = expense.Description,
                Amount = expense.Amount,
                Category = expense.Category,

                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                isDeleted = false,
            };

            await _context.Expenses.AddAsync(newExpense);
            await _context.SaveChangesAsync();

            return ApiResponse.Success(newExpense, "Expense has been successfully Added");
        }

        public async Task<ApiResponse> DeleteExpenseAsync(Guid expenseID)
        {
            var checkForExpense = await _context.Expenses.FirstOrDefaultAsync(d => d.Expense_Id == expenseID && !d.isDeleted);

            if (checkForExpense == null)
            {
                return ApiResponse.Error("Expense with this ID does not exist");
            }
            checkForExpense.isDeleted = true;
            checkForExpense.UpdatedAt = DateTime.Now;

            _context.Expenses.Update(checkForExpense);
            await _context.SaveChangesAsync();

            return ApiResponse.Success(checkForExpense, "Expense successfully deleted");
        }

        public async Task<ApiResponse> GetExpenseAsync(Guid expenseID)
        {
            var expense = await _context.Expenses.FirstOrDefaultAsync(dat => dat.Expense_Id == expenseID && !dat.isDeleted);
            if (expense == null)
            {
                return ApiResponse.Error("Budget with this Id does not exist");
            }
            return ApiResponse.Success(expense, "Budget successfully retrieved");
        }

        public async Task<ApiResponse> GetExpensesAsync()
        {
            var expense = await _context.Expenses.ToListAsync();
            if (expense == null)
            {
                return ApiResponse.Error("Budget does not exist");
            }
            return ApiResponse.Success(expense, "All budget successfully retrieved");
        }

        public async Task<ApiResponse> UpdateExpenseAsync(Guid expenseID, ExpenseDto expense)
        {
            var existingExpense = await _context.Expenses.FirstOrDefaultAsync(d => d.Expense_Id == expenseID && !d.isDeleted);
            if (existingExpense == null)
            {
                return ApiResponse.Error("Budget with this ID does not exist");
            }

            existingExpense.Description = expense.Description;
            existingExpense.Amount = expense.Amount;
            existingExpense.Category = expense.Category;

            existingExpense.UpdatedAt = DateTime.UtcNow;
            existingExpense.isDeleted = false;

            try
            {
                _context.Expenses.Update(existingExpense);
                await _context.SaveChangesAsync();

                return ApiResponse.Success(existingExpense, "Budget successfully updated");
            }
            catch (DbUpdateException ex)
            {
                return ApiResponse.Error("Failed to update the budget: " + ex.Message);
            }
        }
    }
}
