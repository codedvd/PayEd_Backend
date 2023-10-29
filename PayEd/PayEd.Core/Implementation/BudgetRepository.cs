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
    public class BudgetRepository : IBudgetRepository
    {
        private readonly AppDbContext _context;
        public BudgetRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<ApiResponse> CreateBudgetAsync(Guid userId, BudgetDto budget)
        {
            var user = _context.Users.FirstOrDefault(u => u.User_Id == userId);
            if (user == null)
            {
                return ApiResponse.Error("User does not exist");
            }

            var newbudget = new Budgets
            {
                Budget_Id = Guid.NewGuid(),
                Budget_name = budget.Budget_name,
                Description = budget.Description,
                Amount = budget.Amount,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                isDeleted = false,
                Status = Data.Enums.BudgetStatus.Pending,
                User = user,
                UserId = user.User_Id
            };

            await _context.Budgets.AddAsync(newbudget);
            await _context.SaveChangesAsync();

            return ApiResponse.Success(newbudget, "Budget has been successfully created");
        }

        public async Task<ApiResponse> DeleteBudgetAsync(Guid budgetId)
        {
            var checkForOrder = await _context.Budgets.FirstOrDefaultAsync(d => d.Budget_Id == budgetId && !d.isDeleted);

            if (checkForOrder == null)
            {
                return ApiResponse.Error("Budget with this ID does not exist");
            }
            checkForOrder.isDeleted = true;
            checkForOrder.UpdatedAt = DateTime.UtcNow;

            _context.Budgets.Update(checkForOrder);
            await _context.SaveChangesAsync();

            return ApiResponse.Success(checkForOrder, "Budget successfully deleted");
        }


        public async Task<ApiResponse> GetBudgetAsync(Guid budgetId)
        {
            var budget = await _context.Budgets
                .Include(b => b.User)
                .FirstOrDefaultAsync(dat => dat.Budget_Id == budgetId && !dat.isDeleted);

            if (budget == null)
            {
                return ApiResponse.Error("Budget with this Id does not exist");
            }

            return ApiResponse.Success(new
            {
                DateCreated = budget.CreatedAt,
                Description = budget.Description,
                Amount = budget.Amount,
                Budget_Id = budget.Budget_Id,
                UserId = budget.UserId, 
            }, "Budget and user information successfully retrieved");
        }


        public async Task<ApiResponse> GetBudgetsAsync()
        {
            var budgets = await _context.Budgets
                .Where(b => !b.isDeleted)
                .Select(b => new
                {
                    DateCreated = b.CreatedAt,
                    Description = b.Description,
                    Budget_Id = b.Budget_Id,
                    User_Id = b.User.User_Id
                })
                .ToListAsync();

            if (budgets == null || !budgets.Any())
            {
                return ApiResponse.Error("No budgets exist");
            }

            return ApiResponse.Success(budgets, "All budgets with user information successfully retrieved");
        }



        public async Task<ApiResponse> UpdateBudgetAsync(Guid budgetId, BudgetDto budget)
        {
            var existingBudget = await _context.Budgets.FirstOrDefaultAsync(d => d.Budget_Id == budgetId && !d.isDeleted);
            if (existingBudget == null)
            {
                return ApiResponse.Error("Budget with this ID does not exist");
            }

            existingBudget.Budget_name = budget.Budget_name;
            existingBudget.Description = budget.Description;
            existingBudget.Amount = budget.Amount;
            existingBudget.UpdatedAt = DateTime.UtcNow;
            existingBudget.isDeleted = false;

            try
            {
                _context.Update(existingBudget);
                await _context.SaveChangesAsync();

                return ApiResponse.Success(existingBudget, "Budget successfully updated");
            }
            catch (DbUpdateException ex)
            {
                return ApiResponse.Error("Failed to update the budget: " + ex.Message);
            }
        }

    }
}
