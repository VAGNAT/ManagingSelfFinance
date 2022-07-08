using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Model;

namespace Infrastructure.Repositories
{
    public class IncomeRepository : IRepository<Income>
    {
        private readonly FinanceContext _context;

        public IncomeRepository(FinanceContext context)
        {
            _context = context;
        }
        public async Task CreateAsync(Income item)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item), "Parametr can't be null");
            }

            await _context.Incomes.AddAsync(item);
        }

        public void Delete(Income item)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item), "Parametr can't be null");
            }

            _context.Incomes.Remove(item);
        }

        public bool Empty(int id)
        {
            return true;
        }

        public async Task<Income?> ReadAsync(int id)
        {
            return await _context.Incomes.FindAsync(id);
        }

        public Income? Read(int id)
        {
            return _context.Incomes.Include(t => t.TypeIncome).ToList().Find(x => x.Id == id);
        }

        public IEnumerable<Income> ReadAll()
        {
            return _context.Incomes.Include(t => t.TypeIncome);
        }

        public void Update(Income item)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item), "Parametr can't be null");
            }

            _context.Incomes.Update(item);
        }
    }
}
