using Infrastructure.Data;
using Infrastructure.Interfaces;
using Model;

namespace Infrastructure.Repositories
{
    public class TypeExpenseRepository : IRepository<TypeExpense>
    {
        private readonly FinanceContext _context;

        public TypeExpenseRepository(FinanceContext context)
        {
            _context = context;
        }
        public async Task CreateAsync(TypeExpense item)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item), "Parametr can't be null");
            }

            await _context.TypeExpenses.AddAsync(item);
        }

        public void Delete(TypeExpense item)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item), "Parametr can't be null");
            }

            _context.TypeExpenses.Remove(item);
        }

        public bool Empty(int id)
        {
            return _context.Expenses.Any(ex => ex.TypeExpense.Id == id);
        }

        public async Task<TypeExpense?> ReadAsync(int id)
        {
            return await _context.TypeExpenses.FindAsync(id);
        }

        public TypeExpense? Read(int id)
        {
            return _context.TypeExpenses.Find(id);
        }

        public IEnumerable<TypeExpense> ReadAll()
        {
            return _context.TypeExpenses;
        }

        public void Update(TypeExpense item)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item), "Parametr can't be null");
            }

            _context.TypeExpenses.Update(item);
        }                
    }
}
