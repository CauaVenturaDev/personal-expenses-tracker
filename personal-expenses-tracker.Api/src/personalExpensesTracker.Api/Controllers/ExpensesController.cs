using Microsoft.AspNetCore.Mvc;
using personalExpensesTracker.Application.DTOs.ExpenseDTOs.Request;
using personalExpensesTracker.Application.Interfaces;
using personalExpensesTracker.Domain.Models;

namespace personalExpensesTracker.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpensesController(IServices<Expense, CategorySumaryExpenseDto, ExpenseCreateDTO> expensesServices) : ControllerBase
    {
        private readonly IServices<Expense, CategorySumaryExpenseDto, ExpenseCreateDTO> _expensesServices = expensesServices;

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ExpenseCreateDTO expenseCreateDTO)
        {
            var expense = new Expense
            {
                Description = expenseCreateDTO.Description,
                Amount = expenseCreateDTO.Amount,
                Category = expenseCreateDTO.Category,
                Date = expenseCreateDTO.Date
            };
            await _expensesServices.AddAsync(expense);
            return Ok(expense);
        }


        [HttpGet("resumo-das-despesas-de-todos-os-meses")]
        public async Task<IActionResult> GetTotal()
        {
            var results = new List<object>();

            for (int year = 2025; year <= DateTime.Now.Year; year++)
            {
                for (int month = 1; month <= 12; month++)
                {
                    var total = await _expensesServices.GetTotalByMonthAsync(month, year);

                    if (total != 0m)
                    {
                        results.Add(new
                        {
                            Month = month,
                            Year = year,
                            Total = total
                        });
                    }
                }
            }
            return Ok(results);
        }


        [HttpGet("Despesas-do-mês-Detalhado")]
        public async Task<IActionResult> Get(int month, int year)
        {
            var expense = await _expensesServices.GetByMonthAsync(month, year);
            if (expense == null)
            {
                return NotFound();
            }
            return Ok(expense);
        }


        [HttpGet("valor-total-do-mês")]
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


        [HttpGet("total-categoria")]
        public async Task<ActionResult<List<CategorySumaryExpenseDto>>> GetTotalByCategory(
            [FromQuery] int month, 
            [FromQuery] int year)
        {
            var resultado = await _expensesServices.GetTotalByCategoryAsync(month, year);
            return Ok(resultado);
        }


        [HttpPut("atualiza-de-despesa")]
        public async Task<IActionResult> Update(int id, [FromBody] ExpenseCreateDTO expenseCreateDTO)
        {
            var updated = await _expensesServices.UpdateAsync(id, expenseCreateDTO);
            if (expenseCreateDTO == null)
            {
                return NotFound();
            }

            return Ok(updated);
        }


        [HttpDelete("apaga-despesa")]
        public async Task<IActionResult> Delete(int id)
        {
            await _expensesServices.DeleteAsync(id);
            return NoContent();
        }


        [HttpDelete("apaga-all")]
        public async Task<IActionResult> DeleteAll()
        {
            await _expensesServices.DeleteAllAsync();
            return NoContent();
        }
    }
}
