namespace TestProject1;

public class BudgetService
{
    public decimal Query(DateTime start, DateTime end)
    {
        if (end >= start)
        {
            var days = (end - start).Days + 1;
            return 10 * days;
        }

        return 0;
    }
}