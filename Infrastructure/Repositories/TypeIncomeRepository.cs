using Infrastructure.Data;
using Infrastructure.Interfaces;
using Model;

namespace Infrastructure.Repositories
{
    public class TypeIncomeRepository : IRepository<TypeIncome>
    {
        private readonly FinanceContext _context;

        public TypeIncomeRepository(FinanceContext context)
        {
            _context = context;
        }
        public async Task CreateAsync(TypeIncome item)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item), "Parametr can't be null");
            }

            await _context.TypeIncomes.AddAsync(item);
        }

        public void Delete(TypeIncome item)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item), "Parametr can't be null");
            }

            _context.TypeIncomes.Remove(item);
        }

        public bool Empty(int id)
        {
            return _context.Incomes.Any(ex => ex.TypeIncome.Id == id);
        }

        public async Task<TypeIncome?> ReadAsync(int id)
        {
            
            return await _context.TypeIncomes.FindAsync(id);
        }

        public TypeIncome? Read(int id)
        {
            return _context.TypeIncomes.Find(id);
        }

        public IEnumerable<TypeIncome> ReadAll()
        {
            return _context.TypeIncomes;
        }

        public void Update(TypeIncome item)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item), "Parametr can't be null");
            }

            _context.TypeIncomes.Update(item);
        }
    }
}
