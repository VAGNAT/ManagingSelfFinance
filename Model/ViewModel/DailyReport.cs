using Model.ViewModel.AbstractClasses;

namespace Model.ViewModel
{
    public class DailyReport : ReportBase
    {
        private readonly DateTime _date;
                
        public DailyReport(DateTime date, List<Income> incomes, List<Expense> expenses, 
            decimal totalIncomes, decimal totalExpenses) : base(incomes, expenses, totalIncomes, totalExpenses)
        {
            _date = date;
        }

        public DateTime Date => _date;

        public override bool Equals(object? obj)
        {
            return obj is DailyReport report &&
                   EqualityComparer<List<Income>>.Default.Equals(Incomes, report.Incomes) &&
                   EqualityComparer<List<Expense>>.Default.Equals(Expenses, report.Expenses) &&
                   TotalIncomes == report.TotalIncomes &&
                   TotalExpenses == report.TotalExpenses;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Incomes, Expenses, TotalIncomes, TotalExpenses, _date, Date);
        }
    }
}
