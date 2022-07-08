namespace Model.ViewModel.AbstractClasses
{
    public abstract class ReportBase
    {
        private readonly List<Income> _incomes;
        private readonly List<Expense> _expenses;
        private readonly decimal _totalIncomes;
        private readonly decimal _totalExpenses;
        
        public List<Income> Incomes => _incomes;
        public List<Expense> Expenses => _expenses;
        public decimal TotalIncomes => _totalIncomes;
        public decimal TotalExpenses => _totalExpenses;

        public ReportBase(List<Income> incomes, List<Expense> expenses, decimal totalIncomes, decimal totalExpenses)
        {
            _incomes = incomes;
            _expenses = expenses;
            _totalIncomes = totalIncomes;
            _totalExpenses = totalExpenses;
        }
    }
}
