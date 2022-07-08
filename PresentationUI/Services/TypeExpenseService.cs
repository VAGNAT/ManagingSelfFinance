using Newtonsoft.Json;
using PresentationUI.Model;
using PresentationUI.Services.Interfaces;

namespace PresentationUI.Services
{
    public class TypeExpenseService : ICRUD<TypeExpense>
    {
        private readonly IManagingSelfFinanceServiceAPI _api;

        public TypeExpenseService(IManagingSelfFinanceServiceAPI api)
        {
            _api = api ?? throw new ArgumentNullException(nameof(api), "Parametr can't be null");
        }

        public async Task CreateAsync(TypeExpense item)
        {
            string url = "typeExpense";
            await _api.CreateAsync(url, item);
        }

        public async Task DeleteByIdAsync(int id)
        {
            string url = $"typeExpense/{id}";
            await _api.DeleteAsync(url);
        }

        public async Task<IEnumerable<TypeExpense>> GetAllAsync()
        {
            string url = "typeExpense";
            string content = await _api.GetContentFromApiAsync(url);
            IEnumerable<TypeExpense> typeExpenses = JsonConvert.DeserializeObject<IEnumerable<TypeExpense>>(content);
            return typeExpenses ?? new List<TypeExpense>();
        }               

        public async Task UpdateAsync(TypeExpense item)
        {
            string url = "typeExpense";
            await _api.UpdateAsync(url, item);
        }
    }
}
