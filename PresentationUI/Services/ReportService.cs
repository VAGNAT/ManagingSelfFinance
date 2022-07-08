using Newtonsoft.Json;
using PresentationUI.Helpers;
using PresentationUI.Model;
using PresentationUI.Services.Interfaces;
using System.Linq;

namespace PresentationUI.Services
{
    public class ReportService : IReport
    {
        private readonly IManagingSelfFinanceServiceAPI _api;

        public ReportService(IManagingSelfFinanceServiceAPI api)
        {
            _api = api ?? throw new ArgumentNullException(nameof(api), "Parametr can't be null");
        }

        public async Task<DailyReport> GetDailyReportAsync(DateTime date)
        {
            string url = $"report/{date:yyyy.MM.dd}";
            string content = await _api.GetContentFromApiAsync(url);
            DailyReport report = JsonConvert.DeserializeObject<DailyReport>(content);
            return report ?? new DailyReport(DateTime.MinValue, new List<Income>(), new List<Expense>(), 0, 0);
        }

        public async Task<DatePeriodReport> GetDatePeriodReportAsync(DateTime startDate, DateTime endDate)
        {            
            string url = $"report?startDate={startDate:yyyy.MM.dd}&endDate={endDate:yyyy.MM.dd}";
            string content = await _api.GetContentFromApiAsync(url);
            DatePeriodReport report = JsonConvert.DeserializeObject<DatePeriodReport>(content);
            return report ?? new DatePeriodReport(DateTime.MinValue, DateTime.MinValue, new List<Income>(), new List<Expense>(), 0, 0);
        }

        public async Task<DataChart> GetDataForChartAsync(int year)
        {
            DateTime startDate = new(year, 1, 1);
            DateTime endDate = new(year, 12, 31);

            DatePeriodReport report = await GetDatePeriodReportAsync(startDate, endDate);
            
            DataChart data = new DataChart();
            for (int i = 1; i < 12; i++)
            {
                IEnumerable<Income> incomes = report.Incomes.Where(inc => inc.Date >= new DateTime(year, i, 1) && inc.Date <= new DateTime(year, i + 1, 1).AddDays(-1));
                if (incomes.Any())
                {
                    data.TotalIncomes.Add(i, incomes.Select(x => x.Amount).Aggregate((x, y) => x + y));
                }
                else
                {
                    data.TotalIncomes.Add(i, 0);
                }

                IEnumerable<Expense> expenses = report.Expenses.Where(inc => inc.Date >= new DateTime(year, i, 1) && inc.Date <= new DateTime(year, i + 1, 1).AddDays(-1));
                if (expenses.Any())
                {
                    data.TotalExpenses.Add(i, expenses.Select(x => x.Amount).Aggregate((x, y) => x + y));
                }
                else
                {
                    data.TotalExpenses.Add(i, 0);
                }
                
            }
            IEnumerable<Income> incomesDec = report.Incomes.Where(inc => inc.Date >= new DateTime(year, 12, 1) && inc.Date <= new DateTime(year, 12, 31));
            if (incomesDec.Any())
            {
                data.TotalIncomes.Add(12, incomesDec.Select(x => x.Amount).Aggregate((x, y) => x + y));
            }
            else
            {
                data.TotalIncomes.Add(12, 0);
            }

            IEnumerable<Expense> expensesDec = report.Expenses.Where(inc => inc.Date >= new DateTime(year, 12, 1) && inc.Date <= new DateTime(year, 12, 31));
            if (expensesDec.Any())
            {
                data.TotalExpenses.Add(12, expensesDec.Select(x => x.Amount).Aggregate((x, y) => x + y));
            }
            else
            {
                data.TotalExpenses.Add(12, 0);
            }
            
            return data;
        }
    }
}
