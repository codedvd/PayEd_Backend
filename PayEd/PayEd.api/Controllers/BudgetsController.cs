using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PayEd.Core.Services;
using PayEd.Data.Dto;
using PayEd.Data.Models;

namespace PayEd.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BudgetsController : ControllerBase
    {
        private readonly IBudgetRepository _budget;
        public BudgetsController(IBudgetRepository budget)
        {
            _budget = budget;
        }

        [HttpGet("get-all-budget")]
        public async Task<IActionResult> GetBudgets()
        {
            var response = await _budget.GetBudgetsAsync();
            if(response.Suceeded)
            {
                return Ok(response);
            }
            return NoContent();
        }

        [HttpGet("{budgetID}")]
        public async Task<IActionResult> GetBudget(Guid budgetID)
        {
           var response = await _budget.GetBudgetAsync(budgetID);
            if(response != null)
            {
                return Ok(response);
            }
            return NoContent();
        }

        [HttpPost("create-a-budget")]
        public async Task<IActionResult> CreateBudget([FromBody] BudgetDto budget)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest("Invalid Input");
            }
            var response = await _budget.CreateBudgetAsync(budget);
            if(response.Suceeded)
            {
                return Ok(response);
            }
            return BadRequest();
        }

        [HttpPut("{budgetID}")]
        public async Task<IActionResult> UpdateBudget(Guid budgetID, [FromBody] BudgetDto budget)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid Inputs");
            }
            var response = await _budget.UpdateBudgetAsync(budgetID, budget);
            if (response.Suceeded)
            {
                return Ok(response);
            }
            return BadRequest();
        }

        [HttpDelete("{budgetID}")]
        public async Task<IActionResult> DeleteBudget(Guid budgetID)
        {
           var response = await _budget.DeleteBudgetAsync(budgetID);
            if (response.Suceeded)
            {
                return Ok(response);
            }
            return BadRequest();
        }
    }
}
