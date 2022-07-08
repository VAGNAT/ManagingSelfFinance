using Model;

namespace Infrastructure.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<TypeIncome> TypeIncomes { get; }
        IRepository<TypeExpense> TypeExpenses { get; }
        IRepository<Income> Incomes { get; }
        IRepository<Expense> Expenses { get; }
        void Dispose();
        Task SaveAsync();
    }
}
