using PresentationUI.Model.AbstractClasses;

namespace PresentationUI.Model
{
    public class DatePeriodReport : ReportBase
    {
        private readonly DateTime _startDate;
        private readonly DateTime _endDate;

        public DatePeriodReport(DateTime startDate, DateTime endDate, List<Income> incomes, 
            List<Expense> expenses, decimal totalIncomes, decimal totalExpenses) : base(incomes, expenses, totalIncomes, totalExpenses)
        {            
            _startDate = startDate;
            _endDate = endDate;
        }

        public DateTime StartDate => _startDate;
        public DateTime EndDate => _endDate;

        public override bool Equals(object? obj)
        {
            return obj is DatePeriodReport report &&
                   EqualityComparer<List<Income>>.Default.Equals(Incomes, report.Incomes) &&
                   EqualityComparer<List<Expense>>.Default.Equals(Expenses, report.Expenses) &&
                   TotalIncomes == report.TotalIncomes &&
                   TotalExpenses == report.TotalExpenses;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Incomes, Expenses, TotalIncomes, TotalExpenses, _startDate, _endDate, StartDate, EndDate);
        }
    }
}
