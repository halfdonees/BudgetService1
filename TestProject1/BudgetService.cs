using System.Security.Cryptography.X509Certificates;

namespace TestProject1;

public class BudgetService
{
    private readonly IBudgetRepo _repo;

    public BudgetService(IBudgetRepo repo)
    {
        _repo = repo;
    }


    public decimal Query(DateTime start, DateTime end)
    {
        if (end >= start)
        {
            var allBudget = _repo.GetAll();
            var totalMonths = (end.Month - start.Month + 1) + 12 * (end.Year - start.Year);
            var totalAmount = 0M;
            for (var i = 0; i < totalMonths; i++)
            {
                var currentMonth = start.AddMonths(i);
                var fullMonthBudget = allBudget.FirstOrDefault(x => x.YearMonth == $"{currentMonth.Year}{currentMonth:MM}", new Budget());
                var dailyBudget = fullMonthBudget.Amount / GetDaysInMonth(currentMonth);

                var tempStart = currentMonth.Month == start.Month && currentMonth.Year == start.Year
                    ? start
                    : new DateTime(currentMonth.Year, currentMonth.Month, 1);
                var tempEnd = currentMonth.Month == end.Month && currentMonth.Year == end.Year
                    ? end
                    : new DateTime(currentMonth.Year, currentMonth.Month, GetDaysInMonth(currentMonth));


                var days = (tempEnd - tempStart).Days + 1;
                totalAmount += dailyBudget * days;
            }

            return totalAmount;
        }

        return 0;
    }

    private int GetDaysInMonth(DateTime end)
    {
        return DateTime.DaysInMonth(end.Year, end.Month);
    }
}

public interface IBudgetRepo
{
    public List<Budget> GetAll();
}