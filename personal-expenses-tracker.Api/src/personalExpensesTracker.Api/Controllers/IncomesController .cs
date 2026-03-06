using Microsoft.AspNetCore.Mvc;
using personalExpensesTracker.Application.DTOs.IncomeDTOs.Requests;
using personalExpensesTracker.Application.Services.Interfaces;
using personalExpensesTracker.Domain.Entity.Models;

namespace personalExpensesTracker.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IncomeController(IIncomesServices incomeServices) : ControllerBase
    {
        private readonly IIncomesServices _incomeServices = incomeServices;

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] IncomeCreateRequest incomeCreateDTO)
        {
            var income = new Income
            {
                Description = incomeCreateDTO.Description,
                Amount = incomeCreateDTO.Amount,
                Category = incomeCreateDTO.Category,
                Date = incomeCreateDTO.Date
            };
            await _incomeServices.AddAsync(income);
            return Created();
        }

        [HttpGet]
        public async Task<ActionResult<List<MonthlyIncomesDto>>> GetAllDetailed()
        {
            var result = await _incomeServices.GetAllDetailed();
            return Ok(result);
        }

        [HttpGet("mes")]
        public async Task<IActionResult> GetMonthlyTotal(int month, int year)
        {
            var income = await _incomeServices.GetByMonth(month, year);
            if (income == null)
            {
                return NotFound();
            }
            return Ok(income);
        }

        [HttpGet("total")]
        public async Task<IActionResult> GetTotalByMonth(
            [FromQuery] int month,
            [FromQuery] int year)
        {
            var total = await _incomeServices.GetTotalByMonthAsync(month, year);
            return Ok(new
            {
                Month = month,
                Year = year,
                Total = total
            });
        }

        [HttpGet("sumario/categoria")]
        public async Task<ActionResult<List<CategorySumaryIncomeDto>>> GetTotalByCategory(
            [FromQuery] int month,
            [FromQuery] int year)
        {
            var resultado = await _incomeServices.GetTotalByCategoryAsync(month, year);
            return Ok(resultado);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] IncomeCreateRequest incomeCreateDTO)
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

        [HttpDelete("all")]
        public async Task<IActionResult> DeleteAll()
        {
            await _incomeServices.DeleteAllAsync();
            return NoContent();
        }
    }
}