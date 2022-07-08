namespace Model.ViewModel
{
    public struct DataReport
    {
        private List<Income> _incomes;
        private List<Expense> _expenses;
        private decimal _totalIncomes;
        private decimal _totalExpenses;
        public List<Income> Incomes => _incomes;
        public List<Expense> Expenses => _expenses;
        public decimal TotalIncomes => _totalIncomes;
        public decimal TotalExpenses => _totalExpenses;

        public DataReport(List<Income> incomes, List<Expense> expenses, decimal totalIncomes, decimal totalExpenses)
        {
            _incomes = incomes;
            _expenses = expenses;
            _totalIncomes = totalIncomes;
            _totalExpenses = totalExpenses;
        }
    }
}
