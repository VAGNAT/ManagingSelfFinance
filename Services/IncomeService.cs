using Infrastructure.Interfaces;
using Model;
using Services.Interfaces;

namespace Services
{
    public class IncomeService : ICRUD<Income>
    {
        private readonly IUnitOfWork _unitOfWork;
        
        public IncomeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork), "Parametr can't be null");
                    }

        public async Task<Income> AddAsync(Income item)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item), "Parametr can't be null");
            }

            await _unitOfWork.Incomes.CreateAsync(item);
            await _unitOfWork.SaveAsync();
            return item;
        }

        public async Task<bool> Delete(int id)
        {
            Income item = await _unitOfWork.Incomes.ReadAsync(id);
            if (item == null)
            {
                return false;
            }
            _unitOfWork.Incomes.Delete(item);
            await _unitOfWork.SaveAsync();
            return true;
        }

        public async Task<Income> GetByIdAsync(int id)
        {
            return await _unitOfWork.Incomes.ReadAsync(id);
        }

        public Income GetById(int id)
        {
            return _unitOfWork.Incomes.Read(id);
        }

        public IEnumerable<Income> GetAll()
        {
            return _unitOfWork.Incomes.ReadAll();
        }

        public Income Update(Income item, Income value)
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
            _unitOfWork.Incomes.Update(item);
            _unitOfWork.SaveAsync();
            return item;
        }
    }
}
