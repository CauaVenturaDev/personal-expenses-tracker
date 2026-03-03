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
            [FromServices] IServices<Expense, CategorySumaryExpenseDto, ExpenseCreateRequest, MonthlyExpensesDto> expensesServices,
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

        //[HttpGet("sumario/mensalmente")]
        //public async Task<IActionResult> GetTotal(
        //    [FromServices] IExpansesService expansesService,

        //)
        //{
        //    var results = new List<object>();
        //    for (int year = 2025; year <= DateTime.Now.Year; year++)
        //    {
        //        for (int month = 1; month <= 12; month++)
        //        {
        //            var expenseTask = await expensesServices.GetTotalByMonthAsync(month, year);
        //            var incomeTask = await incomeServices.GetTotalByMonthAsync(month, year);

        //            if (expenseTask != 0m || incomeTask != 0m)
        //            {
        //                results.Add(new
        //                {
        //                    Month = month,
        //                    Year = year,
        //                    Incomes = incomeTask,
        //                    Expenses = expenseTask,
        //                    Balance = incomeTask - expenseTask
        //                });
        //            }
        //        }
        //    }
        //    return Ok(results);
        //}
        [HttpGet("Sumario/meses/detalhado")]
        public async Task<IActionResult> GetDetailedSummary(
         [FromServices] IServices<Expense, CategorySumaryExpenseDto, ExpenseCreateRequest, MonthlyExpensesDto> expensesServices,
         [FromServices] IServices<Income, CategorySumaryIncomeDto, IncomeCreateDTO, MonthlyIncomesDto> incomeServices)
        {
            var results = new List<object>();

            var allExpenseMonths = await expensesServices.GetAllDetailedAsync().ConfigureAwait(false) ?? new List<MonthlyExpensesDto>();
            var allIncomeMonths = await incomeServices.GetAllDetailedAsync().ConfigureAwait(false) ?? new List<MonthlyIncomesDto>();

            // Cria dicionários para facilitar busca
            var expensesDict = allExpenseMonths
                .Where(e => e != null)
                .ToDictionary(e => (e.Year, e.Month), e => e);

            var incomesDict = allIncomeMonths
                .Where(i => i != null)
                .ToDictionary(i => (i.Year, i.Month), i => i);

            // Junta todas as chaves (ano/mês)
            var allKeys = expensesDict.Keys
                .Union(incomesDict.Keys)
                .ToList();

            foreach (var key in allKeys)
            {
                expensesDict.TryGetValue(key, out var expMonth);
                incomesDict.TryGetValue(key, out var incMonth);

                var expensesList = expMonth?.Expenses ?? new List<Expense>();
                var incomesList = incMonth?.Incomes ?? new List<Income>();

                if (!expensesList.Any() && !incomesList.Any())
                    continue;

                decimal totalExpenses = expensesList.Sum(e => e?.Amount ?? 0m);
                decimal totalIncomes = incomesList.Sum(i => i?.Amount ?? 0m);

                results.Add(new
                {
                    Month = key.Month,
                    Year = key.Year,
                    Expenses = expensesList,
                    Incomes = incomesList,
                    TotalExpenses = totalExpenses,
                    TotalIncomes = totalIncomes,
                    Balance = totalIncomes - totalExpenses
                });
            }

            results = results
                .OrderBy(r => ((dynamic)r).Year)
                .ThenBy(r => ((dynamic)r).Month)
                .ToList();

            return Ok(results);
        }
    }
}
