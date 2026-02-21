using Microsoft.AspNetCore.Mvc;
using personalExpensesTracker.Application.DTOs.IncomeDTOs.Requests;
using personalExpensesTracker.Application.Interfaces;
using personalExpensesTracker.Application.Services;
using personalExpensesTracker.Domain.Models;

namespace personalExpensesTracker.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IncomeController(IIncomeServices incomeServices) : ControllerBase
    {
        private readonly IIncomeServices _incomeServices = incomeServices;


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] IncomeCreateDTO incomeCreateDTO)
        {
            var income = new Income
            {
                Description = incomeCreateDTO.Description,
                Amount = incomeCreateDTO.Amount,
                Category = incomeCreateDTO.Category,
                Date = incomeCreateDTO.Date
            };
            await _incomeServices.AddAsync(income);
            return Ok(income);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var incomes = _incomeServices.GetAllIncomesAsync().Result;
            return Ok(incomes);
        }

        [HttpGet("mes-ano")]
        public async Task<IActionResult> Get(int mês, int ano)
        {
            var income = await _incomeServices.GetByMonthAsync(mês, ano);
            if (income == null)
            {
                return NotFound();
            }
            return Ok(income);
        }
        [HttpGet("total")]
        public async Task<IActionResult> GetTotalByMonth([FromQuery] int mês, [FromQuery] int ano)
        {
            var total = await _incomeServices.GetTotalByMonthAsync(mês, ano);
            return Ok(new
            {
                Mes = mês,
                Ano = ano,
                Total = total
            });
        }

        [HttpGet("total/categoria")]
        public async Task<ActionResult<List<CategorySumaryIncomeDto>>> GetTotalByCategory([FromQuery] int mês, [FromQuery] int ano)
        {
            var resultado = await _incomeServices.GetTotalByCategoryAsync(mês, ano);
            return Ok(resultado);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] IncomeCreateDTO incomeCreateDTO)
        { 
            var updated = await _incomeServices.UpdateAsync(id, incomeCreateDTO);
            if (incomeCreateDTO == null)
            {
                return NotFound();
            }

            return Ok(updated);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _incomeServices.DeleteAsync(id);
            return NoContent();
        }

        [HttpDelete("delete-all")]
        public async Task<IActionResult> DeleteAll()
        {
            await _incomeServices.DeleteAllAsync();
            return NoContent();
        }
    }
}
