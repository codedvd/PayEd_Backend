using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PayEd.Core.Services;
using PayEd.Data.Dto;
using PayEd.Data.Models;
using PayEd.Infrastructure.Helpers;

namespace PayEd.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpensesController : ControllerBase
    {
        private readonly IExpenseRepository _expense;

        public ExpensesController(IExpenseRepository expense)
        {
            _expense = expense;
        }

        //[Authorize("Admin")]
        [HttpGet("get-all-expense")]
        public async Task<IActionResult> GetExpenses()
        {
            var expenses = await _expense.GetExpensesAsync();
            if(expenses.Suceeded)
                return Ok(expenses);
            return BadRequest(expenses);
        }

        [HttpGet("{expenseID}")]
        public async Task<IActionResult> GetExpense(Guid expenseID)
        {
            var expense = await _expense.GetExpenseAsync(expenseID);
            if (!expense.Suceeded)
                return NoContent();
            return Ok(expense);
        }

        [HttpPost("create-an-expense")]
        public async Task<ActionResult<Guid>> CreateExpense(Guid userId, [FromBody] ExpenseDto expense)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid Inputs in Fields");
            }
            var expenseID = await _expense.CreateExpenseAsync(userId, expense);
            return CreatedAtAction(nameof(GetExpense), new { expenseID }, expenseID);
        }

        [HttpPut("{expenseID}")]
        public async Task<ActionResult> UpdateExpense(Guid expenseID, [FromBody] ExpenseDto expense)
        {
            var updated = await _expense.UpdateExpenseAsync(expenseID, expense);
            if (!updated.Suceeded)
                return BadRequest(updated);
            return Ok(updated);
        }

        [HttpDelete("{expenseID}")]
        public async Task<ActionResult> DeleteExpense(Guid expenseID)
        {
            var deleted = await _expense.DeleteExpenseAsync(expenseID);
            if (!deleted.Suceeded)
                return BadRequest(deleted);
            return Ok(deleted);
        }
    }
}
