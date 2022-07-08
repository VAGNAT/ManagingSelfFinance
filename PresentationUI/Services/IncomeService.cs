using Newtonsoft.Json;
using PresentationUI.Model;
using PresentationUI.Services.Interfaces;

namespace PresentationUI.Services
{
    public class IncomeService : ICRUD<Income>
    {
        private readonly IManagingSelfFinanceServiceAPI _api;

        public IncomeService(IManagingSelfFinanceServiceAPI api)
        {
            _api = api ?? throw new ArgumentNullException(nameof(api), "Parametr can't be null");
        }

        public async Task CreateAsync(Income item)
        {
            string url = "income";
            await _api.CreateAsync(url, item);
        }

        public async Task DeleteByIdAsync(int id)
        {
            string url = $"income/{id}";
            await _api.DeleteAsync(url);
        }

        public async Task<IEnumerable<Income>> GetAllAsync()
        {
            string url = "income";
            string content = await _api.GetContentFromApiAsync(url);
            IEnumerable<Income>? incomes = JsonConvert.DeserializeObject<IEnumerable<Income>>(content)?.OrderByDescending(e => e.Date);
            return incomes ?? new List<Income>();
        }

        public async Task UpdateAsync(Income item)
        {
            string url = "income";
            await _api.UpdateAsync(url, item);
        }
    }
}
