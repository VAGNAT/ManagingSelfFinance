using Newtonsoft.Json;
using PresentationUI.Model;
using PresentationUI.Services.Interfaces;

namespace PresentationUI.Services
{
    public class ExpenseService : ICRUD<Expense>
    {
        private readonly IManagingSelfFinanceServiceAPI _api;

        public ExpenseService(IManagingSelfFinanceServiceAPI api)
        {
            _api = api ?? throw new ArgumentNullException(nameof(api), "Parametr can't be null");
        }

        public async Task CreateAsync(Expense item)
        {
            string url = "expense";
            await _api.CreateAsync(url, item);
        }

        public async Task DeleteByIdAsync(int id)
        {
            string url = $"expense/{id}";
            await _api.DeleteAsync(url);
        }

        public async Task<IEnumerable<Expense>> GetAllAsync()
        {
            string url = "expense";
            string content = await _api.GetContentFromApiAsync(url);
            IEnumerable<Expense>? expenses = JsonConvert.DeserializeObject<IEnumerable<Expense>>(content)?.OrderByDescending(e => e.Date);            
            return expenses ?? new List<Expense>();
        }                

        public async Task UpdateAsync(Expense item)
        {
            string url = "expense";
            await _api.UpdateAsync(url, item);
        }
    }
}
