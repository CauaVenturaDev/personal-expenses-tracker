using Microsoft.AspNetCore.Mvc;
using personalExpensesTracker.Application.DTOs.ExpenseDTOs.Request;
using personalExpensesTracker.Application.DTOs.IncomeDTOs.Requests;
using personalExpensesTracker.Application.Interfaces;
using personalExpensesTracker.Domain.Models;

namespace personalExpensesTracker.Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class IncomeExpensesController() : ControllerBase
    {
        [HttpGet("mes")]
        public IActionResult GetTotalByMonth(
            [FromQuery] int? month,
            [FromQuery] int? year,
            [FromServices] IServices<Expense, CategorySumaryExpenseDto, ExpenseCreateDTO, MonthlyExpensesDto> expensesServices,
            [FromServices] IServices<Income, CategorySumaryIncomeDto, IncomeCreateDTO, MonthlyIncomesDto> incomeServices)
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

        [HttpGet("sumario/mensalmente")]
        public async Task<IActionResult> GetTotal(
         [FromServices] IServices<Expense, CategorySumaryExpenseDto, ExpenseCreateDTO, MonthlyExpensesDto> expensesServices,
         [FromServices] IServices<Income, CategorySumaryIncomeDto, IncomeCreateDTO, MonthlyIncomesDto> incomeServices)
        {
            var results = new List<object>();
            for (int year = 2025; year <= DateTime.Now.Year; year++)
            {
                for (int month = 1; month <= 12; month++)
                {
                    var expenseTask = await expensesServices.GetTotalByMonthAsync(month, year);
                    var incomeTask = await  incomeServices.GetTotalByMonthAsync(month, year);

                    if (expenseTask != 0m || incomeTask != 0m)
                    {
                        results.Add(new
                        {
                            Month = month,
                            Year = year,
                            Incomes = incomeTask,
                            Expenses = expenseTask,
                            Balance = incomeTask - expenseTask
                        });
                    }
                }
            }
            return Ok(results);
        }
    }
}
