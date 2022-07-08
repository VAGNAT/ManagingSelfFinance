using Infrastructure.Interfaces;
using Model;
using Services.Interfaces;

namespace Services
{
    public class ExpenseService : ICRUD<Expense>
    {
        private readonly IUnitOfWork _unitOfWork;
        
        public ExpenseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork), "Parametr can't be null");            
        }

        public async Task<Expense> AddAsync(Expense item)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item), "Parametr can't be null");
            }

            await _unitOfWork.Expenses.CreateAsync(item);
            await _unitOfWork.SaveAsync();
            return item;
        }               

        public async Task<bool> Delete(int id)
        {
            Expense item = await _unitOfWork.Expenses.ReadAsync(id);
            if (item == null)
            {
                return false;
            }
            _unitOfWork.Expenses.Delete(item);
            await _unitOfWork.SaveAsync();
            return true;
        }

        public async Task<Expense> GetByIdAsync(int id)
        {
            return await _unitOfWork.Expenses.ReadAsync(id);
        }

        public Expense GetById(int id)
        {
            return _unitOfWork.Expenses.Read(id);
        }

        public IEnumerable<Expense> GetAll()
        {
            return _unitOfWork.Expenses.ReadAll();
        }

        public Expense Update(Expense item, Expense value)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item), "Parametr can't be null");
            }

            if (value is null)
            {
                throw new ArgumentNullException(nameof(value), "Parametr can't be null");
            }

            item.Date = value.Date;            
            item.Amount = value.Amount;
            _unitOfWork.Expenses.Update(item);
            _unitOfWork.SaveAsync();
            
            return item;
        }                
    }
}
