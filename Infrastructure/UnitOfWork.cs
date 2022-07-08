using Infrastructure.Data;
using Infrastructure.Interfaces;
using Model;

namespace Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly FinanceContext _context;
        private readonly IRepository<TypeIncome> _typeIncomeRepo;
        private readonly IRepository<TypeExpense> _typeExpenseRepo;
        private readonly IRepository<Income> _incomeRepo;
        private readonly IRepository<Expense> _expenseRepo;

        public IRepository<TypeIncome> TypeIncomes => _typeIncomeRepo;

        public IRepository<TypeExpense> TypeExpenses => _typeExpenseRepo;
       
        public IRepository<Income> Incomes => _incomeRepo;

        public IRepository<Expense> Expenses => _expenseRepo;        

        public UnitOfWork(FinanceContext context, IRepository<TypeIncome> typeIncomeRepo, 
            IRepository<TypeExpense> typeExpenseRepo, IRepository<Income> incomeRepo, IRepository<Expense> expenseRepo)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context), "Parametr can't be null");
            _typeIncomeRepo = typeIncomeRepo ?? throw new ArgumentNullException(nameof(typeIncomeRepo), "Parametr can't be null");
            _typeExpenseRepo = typeExpenseRepo ?? throw new ArgumentNullException(nameof(typeExpenseRepo), "Parametr can't be null");
            _incomeRepo = incomeRepo ?? throw new ArgumentNullException(nameof(incomeRepo), "Parametr can't be null");
            _expenseRepo = expenseRepo ?? throw new ArgumentNullException(nameof(expenseRepo), "Parametr can't be null");            
        }

        public void Dispose() => _context.Dispose();

        public async Task SaveAsync() => await _context.SaveChangesAsync();
    }
}