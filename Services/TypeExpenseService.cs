using Infrastructure.Interfaces;
using Model;
using Services.Interfaces;

namespace Services
{
    public class TypeExpenseService : ICRUD<TypeExpense>
    {
        private readonly IUnitOfWork _unitOfWork;

        public TypeExpenseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork), "Parametr can't be null");
        }

        public async Task<TypeExpense> AddAsync(TypeExpense item)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item), "Parametr can't be null");
            }

            await _unitOfWork.TypeExpenses.CreateAsync(item);
            await _unitOfWork.SaveAsync();
            return item;
        }

        public async Task<bool> Delete(int id)
        {
            TypeExpense item = await _unitOfWork.TypeExpenses.ReadAsync(id);
            if (item == null)
            {
                return false;
            }
            _unitOfWork.TypeExpenses.Delete(item);
            await _unitOfWork.SaveAsync();
            return true;
        }

        public async Task<TypeExpense> GetByIdAsync(int id)
        {
            return await _unitOfWork.TypeExpenses.ReadAsync(id);
        }

        public TypeExpense GetById(int id)
        {
            return _unitOfWork.TypeExpenses.Read(id);
        }

        public IEnumerable<TypeExpense> GetAll()
        {
            return _unitOfWork.TypeExpenses.ReadAll();
        }

        public TypeExpense Update(TypeExpense item, TypeExpense value)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item), "Parametr can't be null");
            }

            if (value is null)
            {
                throw new ArgumentNullException(nameof(value), "Parametr can't be null");
            }

            item.Name = value.Name;
            _unitOfWork.TypeExpenses.Update(item);
            _unitOfWork.SaveAsync();
            return item;
        }      
    }
}