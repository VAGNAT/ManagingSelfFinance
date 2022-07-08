using Newtonsoft.Json;
using PresentationUI.Model;
using PresentationUI.Services.Interfaces;

namespace PresentationUI.Services
{
    public class TypeIncomeService : ICRUD<TypeIncome>
    {
        private readonly IManagingSelfFinanceServiceAPI _api;

        public TypeIncomeService(IManagingSelfFinanceServiceAPI api)
        {
            _api = api ?? throw new ArgumentNullException(nameof(api), "Parametr can't be null");
        }

        public async Task CreateAsync(TypeIncome item)
        {
            string url = "typeIncome";
            await _api.CreateAsync(url, item);
        }

        public async Task DeleteByIdAsync(int id)
        {
            string url = $"typeIncome/{id}";
            await _api.DeleteAsync(url);
        }

        public async Task<IEnumerable<TypeIncome>> GetAllAsync()
        {
            string url = "typeIncome";
            string content = await _api.GetContentFromApiAsync(url);
            IEnumerable<TypeIncome> typeIncomes = JsonConvert.DeserializeObject<IEnumerable<TypeIncome>>(content);
            return typeIncomes ?? new List<TypeIncome>();
        }                

        public async Task UpdateAsync(TypeIncome item)
        {
            string url = "typeIncome";
            await _api.UpdateAsync(url, item);
        }
    }
}
