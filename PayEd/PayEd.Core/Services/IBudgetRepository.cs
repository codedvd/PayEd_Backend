using PayEd.Data.Dto;
using PayEd.Data.Models;
using PayEd.Infrastructure.Helpers;

namespace PayEd.Core.Services
{
    public interface IBudgetRepository
    {
        Task<ApiResponse> GetBudgetsAsync();
        Task<ApiResponse> GetBudgetAsync(Guid budgetId);
        Task<ApiResponse> CreateBudgetAsync(BudgetDto budget);
        Task<ApiResponse> UpdateBudgetAsync(Guid budgetId, BudgetDto budget);
        Task<ApiResponse> DeleteBudgetAsync(Guid budgetId);
    }
}