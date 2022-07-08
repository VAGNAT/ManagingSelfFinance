using Infrastructure.Interfaces;
using Model;
using Services.Interfaces;

namespace Services
{
    public class TypeIncomeService : ICRUD<TypeIncome>
    {
        private readonly IUnitOfWork _unitOfWork;

        public TypeIncomeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork), "Parametr can't be null");
        }

        public async Task<TypeIncome> AddAsync(TypeIncome item)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item), "Parametr can't be null");
            }

            await _unitOfWork.TypeIncomes.CreateAsync(item);
            await _unitOfWork.SaveAsync();
            return item;
        }

        public async Task<bool> Delete(int id)
        {
            TypeIncome item = await _unitOfWork.TypeIncomes.ReadAsync(id);
            if (item == null)
            {
                return false;
            }
            _unitOfWork.TypeIncomes.Delete(item);
            await _unitOfWork.SaveAsync();
            return true;
        }

        public async Task<TypeIncome> GetByIdAsync(int id)
        {
            return await _unitOfWork.TypeIncomes.ReadAsync(id);
        }

        public TypeIncome GetById(int id)
        {
            return _unitOfWork.TypeIncomes.Read(id);
        }

        public IEnumerable<TypeIncome> GetAll()
        {
            return _unitOfWork.TypeIncomes.ReadAll();
        }

        public TypeIncome Update(TypeIncome item, TypeIncome value)
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
            _unitOfWork.TypeIncomes.Update(item);
            _unitOfWork.SaveAsync();
            return item;
        }
    }
}
