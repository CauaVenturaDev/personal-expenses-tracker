using Microsoft.AspNetCore.Mvc;
using personalExpensesTracker.Application.DTOs.ExpenseDTOs.Request;
using personalExpensesTracker.Application.Interfaces;
using personalExpensesTracker.Domain.Models;
using personalExpensesTracker.Infrastructure.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace personalExpensesTracker.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpensesController(IExpensesServices expensesServices) : ControllerBase
    {
        private readonly IExpensesServices _expensesServices = expensesServices;


        [HttpGet("mes-ano")]
        public async Task<IActionResult> Get(int mês, int ano)
        {
            var expense = await _expensesServices.GetByMonthAsync(mês, ano);
            if (expense == null)
            {
                return NotFound();
            }
            return Ok(expense);
        }
        [HttpGet("total")]
        public async Task<IActionResult> GetTotalByMonth([FromQuery] int mês, [FromQuery] int ano)
        {
            var total = await _expensesServices.GetTotalByMonthAsync(mês, ano);
            return Ok(new
            {
                Mes = mês,
                Ano = ano,
                Total = total
            });
        }


        [HttpGet("total/categoria")]
        public async Task<ActionResult<List<CategorySumaryDto>>> GetTotalByCategory([FromQuery] int mês, [FromQuery] int ano)
        {
            var resultado = await _expensesServices.GetTotalByCategoryAsync(mês, ano);
            return Ok(resultado);
        }


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


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ExpenseCreateDTO expenseCreateDTO)
        { 
            var updated = await _expensesServices.UpdateAsync(id, expenseCreateDTO);
            if (expenseCreateDTO == null)
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
    }
}
