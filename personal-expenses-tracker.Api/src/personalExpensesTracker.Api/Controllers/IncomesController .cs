using Microsoft.AspNetCore.Mvc;
using personalExpensesTracker.Application.DTOs.IncomeDTOs.Requests;
using personalExpensesTracker.Application.Interfaces;
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
        public async Task<IActionResult> GetTotal()
        {
            var retults = new List<object>();

            for (int year = 2025; year <= DateTime.Now.Year; year++) 
            {
                for (int month = 1; month <= 12; month++)
                {
                    var total = await _incomeServices.GetTotalByMonthAsync(month, year);
                    if (total != 0)
                    {
                        retults.Add(new
                        {
                            Month = month,
                            Year = year,
                            Total = total
                        });
                    }
                }
            }
            return Ok(retults);
        }

        [HttpGet("mes-ano")]
        public async Task<IActionResult> Get(int month, int year)
        {
            var income = await _incomeServices.GetByMonthAsync(month, year);
            if (income == null)
            {
                return NotFound();
            }
            return Ok(income);
        }
        [HttpGet("total")]
        public async Task<IActionResult> GetTotalByMonth([FromQuery] int month, [FromQuery] int year)
        {
            var total = await _incomeServices.GetTotalByMonthAsync(month, year);
            return Ok(new
            {
                Month = month,
                Year = year,
                Total = total
            });
        }

        [HttpGet("total/categoria")]
        public async Task<ActionResult<List<CategorySumaryIncomeDto>>> GetTotalByCategory([FromQuery] int month, [FromQuery] int year)
        {
            var resultado = await _incomeServices.GetTotalByCategoryAsync(month, year);
            return Ok(resultado);
        }

        [HttpPut("atualiza-receita")]
        public async Task<IActionResult> Update(int id, [FromBody] IncomeCreateDTO incomeCreateDTO)
        { 
            var updated = await _incomeServices.UpdateAsync(id, incomeCreateDTO);
            if (incomeCreateDTO == null)
            {
                return NotFound();
            }

            return Ok(updated);
        }


        [HttpDelete("apaga-receita")]
        public async Task<IActionResult> Delete(int id)
        {
            await _incomeServices.DeleteAsync(id);
            return NoContent();
        }

        [HttpDelete("apaga-todas-receitas")]
        public async Task<IActionResult> DeleteAll()
        {
            await _incomeServices.DeleteAllAsync();
            return NoContent();
        }
    }
}
