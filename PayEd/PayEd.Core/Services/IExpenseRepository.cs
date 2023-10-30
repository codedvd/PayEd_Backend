using PayEd.Data.Dto;
using PayEd.Data.Models;
using PayEd.Infrastructure.Helpers;

namespace PayEd.Core.Services
{
    public interface IExpenseRepository
    {
        Task<ApiResponse> GetExpensesAsync();
        Task<ApiResponse> GetExpenseAsync(Guid expenseID);
        Task<ApiResponse> CreateExpenseAsync(Guid userId, ExpenseDto expense);
        Task<ApiResponse> UpdateExpenseAsync(Guid expenseID, ExpenseDto expense);
        Task<ApiResponse> DeleteExpenseAsync(Guid expenseID);
    }
}