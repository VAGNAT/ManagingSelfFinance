using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Model;

namespace Infrastructure.Repositories
{
    public class ExpenseRepository : IRepository<Expense>
    {
        private readonly FinanceContext _context;

        public ExpenseRepository(FinanceContext context)
        {
            _context = context;
        }
        public async Task CreateAsync(Expense item)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item), "Parametr can't be null");
            }

            await _context.Expenses.AddAsync(item);
        }

        public void Delete(Expense item)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item), "Parametr can't be null");
            }

            _context.Expenses.Remove(item);
        }

        public bool Empty(int id)
        {
            return true;
        }

        public async Task<Expense?> ReadAsync(int id)
        {
            return await _context.Expenses.FindAsync(id);
        }

        public Expense? Read(int id)
        {
            return _context.Expenses.Include(t => t.TypeExpense).ToList().Find(x => x.Id == id);
        }

        public IEnumerable<Expense> ReadAll()
        {
            return _context.Expenses.Include(t => t.TypeExpense);
        }

        public void Update(Expense item)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item), "Parametr can't be null");
            }

            _context.Expenses.Update(item);
        }
    }
}
