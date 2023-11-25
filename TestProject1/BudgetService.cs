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
            var fullMonthBudget = allBudget.FirstOrDefault(x => x.YearMonth == $"{end.Year}{end.Month}", new Budget()
            {
            });

            var dailyBudget = fullMonthBudget.Amount / GetDaysInMonth(end);
            var days = (end - start).Days + 1;

            return dailyBudget * days;
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