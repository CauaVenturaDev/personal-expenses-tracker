using Microsoft.AspNetCore.Mvc;
using personalExpensesTracker.Application.DTOs.ExpenseDTOs.Request;
using personalExpensesTracker.Application.Services;
using personalExpensesTracker.Domain.Models;

namespace personalExpensesTracker.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpensesController : ControllerBase
    {

        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] ExpenseCreateRequest expenseCreateDTO
        )
        {
            var expense = new Expense
            {
                Description = expenseCreateDTO.Description,
                Amount = expenseCreateDTO.Amount,
                Category = expenseCreateDTO.Category,
                Date = expenseCreateDTO.Date
            };
            await _expensesServices.AddAsync(expense);
            return Created();
        }

        [HttpGet("detailed")]
        public async Task<ActionResult<IEnumerable<MonthlyExpensesDto>>> GetAllDetailed(
            [FromServices] IExpensesServices expensesServices
        )
        {
            var result = await expensesServices.GetAllDetailed();
            return Ok(result);
        }


        [HttpGet("mes")]
        public async Task<IActionResult> GetMonthlyTotal(
            [FromQuery] int month,
            [FromQuery] int year,
            [FromServices] IExpensesServices expensesServices
        )
        {
            var expense = await expensesServices.GetByMonth(month, year);

            return Ok(expense);
        }


        [HttpGet("total")]
        public async Task<IActionResult> GetTotalByMonth(
            [FromQuery] int month, 
            [FromQuery] int year)
        {
            var total = await _expensesServices.GetTotalByMonthAsync(month, year);
            return Ok(new
            {
                Month = month,
                Year = year,
                Total = total
            });
        }


        [HttpGet("sumario/categoria")]
        public async Task<ActionResult<List<CategorySumaryExpenseDto>>> GetTotalByCategory(
            [FromQuery] int month, 
            [FromQuery] int year)
        {
            var resultado = await _expensesServices.GetTotalByCategoryAsync(month, year);
            return Ok(resultado);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ExpenseCreateRequest expenseCreateDTO)
        {
            var updated = await _expensesServices.UpdateAsync(id, expenseCreateDTO);
            if (updated == null)
            {
                return NotFound();
            }

            return Ok(updated);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _expensesServices.DeleteAsync(id);
            return NoContent();
        }


        [HttpDelete("all")]
        public async Task<IActionResult> DeleteAll()
        {
            await _expensesServices.DeleteAllAsync();
            return NoContent();
        }
    }
}
