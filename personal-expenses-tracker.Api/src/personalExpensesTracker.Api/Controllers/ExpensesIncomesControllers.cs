using Microsoft.AspNetCore.Mvc;
using personalExpensesTracker.Application.Interfaces;

namespace personalExpensesTracker.Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class IncomeExpensesController() : ControllerBase
    {
        [HttpGet]
        public IActionResult GetTotalByMonth(
            [FromQuery] int? month,
            [FromQuery] int? year,
            [FromServices] IExpensesServices expensesServices,
            [FromServices] IIncomeServices incomeServices)
        {
            month ??= DateTime.Now.Month;
            year ??= DateTime.Now.Year;

            var expense = expensesServices.GetTotalByMonthAsync(month.Value, year.Value).Result;
            var income = incomeServices.GetTotalByMonthAsync(month.Value, year.Value).Result;

            return Ok(new
            {
                Mês = month,
                Ano = year,
                Incomes = income,
                Expenses = expense,
                Total = income - expense
            });
        }
    }
}
