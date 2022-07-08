using Newtonsoft.Json;
using PresentationUI.Model;
using PresentationUI.Services.Interfaces;

namespace PresentationUI.Services
{
    public class CheckTypesService : ICheck
    {
        private readonly IManagingSelfFinanceServiceAPI _api;

        public CheckTypesService(IManagingSelfFinanceServiceAPI api)
        {
            _api = api ?? throw new ArgumentNullException(nameof(api), "Parametr can't be null");
        }

        public async Task<bool> PossibleToRemoveTypeExpenseAsync(int id)
        {
            string url = "Expense";
            string content = await _api.GetContentFromApiAsync(url);
            IEnumerable<Expense>? expenses = JsonConvert.DeserializeObject<IEnumerable<Expense>>(content)?.Where(e=>e.TypeExpenseId == id);
            return expenses?.Any() ?? false;
        }

        public async Task<bool> PossibleToRemoveTypeIncomeAsync(int id)
        {
            string url = "Income";
            string content = await _api.GetContentFromApiAsync(url);
            IEnumerable<Income>? incomes = JsonConvert.DeserializeObject<IEnumerable<Income>>(content)?.Where(e => e.TypeIncomeId == id);
            return incomes?.Any() ?? false;
        }
    }
}
