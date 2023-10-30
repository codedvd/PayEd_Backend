using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PayEd.Core.Services;
using PayEd.Data.AppContext;
using PayEd.Data.Dto;
using PayEd.Data.Models;
using PayEd.Infrastructure.Helpers;
using Supabase.Storage;

namespace PayEd.Core.Implementation
{
    public class ExpenseRepository : IExpenseRepository
    {
        private readonly AppDbContext _context;
        public ExpenseRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<ApiResponse> CreateExpenseAsync(Guid userId, ExpenseDto expense)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.User_Id == userId);
            if (user == null)
            {
                return ApiResponse.Error("User with this Id does not exist");
            }
            //Default Budget
            var budget = await _context.Budgets.FirstOrDefaultAsync(b => b.UserId == userId && !b.isDeleted);

            if (budget == null)
            {
                return ApiResponse.Error("User does not have a budget");
            }

            var newExpense = new Expenses
            {
                Expense_Id = Guid.NewGuid(),
                Description = expense.Description,
                Amount = expense.Amount,
                BudgetId = budget.Budget_Id,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                isDeleted = false,
            };

            await _context.Expenses.AddAsync(newExpense);
            await _context.SaveChangesAsync();

            return ApiResponse.Success(newExpense, "Expense has been successfully added");
        }

        public async Task<ApiResponse> DeleteExpenseAsync(Guid expenseID)
        {
            var checkForExpense = await _context.Expenses.FirstOrDefaultAsync(d => d.Expense_Id == expenseID && !d.isDeleted);

            if (checkForExpense == null)
            {
                return ApiResponse.Error("Expense with this ID does not exist");
            }
            checkForExpense.isDeleted = true;
            checkForExpense.UpdatedAt = DateTime.UtcNow;

            _context.Expenses.Update(checkForExpense);
            await _context.SaveChangesAsync();

            return ApiResponse.Success(checkForExpense.Expense_Id, "Expense successfully deleted");
        }

        public async Task<ApiResponse> GetExpenseAsync(Guid expenseID)
        {
            var expense = await _context.Expenses
                .Include(e => e.Budget) // Include the associated User entity
                .FirstOrDefaultAsync(dat => dat.Expense_Id == expenseID && !dat.isDeleted);

            if (expense == null)
            {
                return ApiResponse.Error("Expense with this Id does not exist");
            }

            return ApiResponse.Success(new
            {
                Description = expense.Description,
                Amount = expense.Amount,
                CreatedAt = expense.CreatedAt,
                Expese_Id = expense.Expense_Id,
                Budget_Id = expense.BudgetId 
            }, "Expense successfully retrieved");
        }


        public async Task<ApiResponse> GetExpensesAsync()
        {
            var expense = await _context.Expenses
                .Where(d => !d.isDeleted)
                .Select(e => new
                {
                    Description = e.Description,
                    Amount = e.Amount,
                    CreatedAt = e.CreatedAt,
                    Expense_Id = e.Expense_Id,
                    Budget_Id = e.BudgetId
                })
                .ToListAsync();
            if (expense == null || !expense.Any())
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
                return ApiResponse.Error("Expense with this ID does not exist");
            }

            existingExpense.Description = expense.Description;
            existingExpense.Amount = expense.Amount;

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
