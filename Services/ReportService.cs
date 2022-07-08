using Infrastructure.Interfaces;
using Model;
using Model.ViewModel;
using Services.Interfaces;

namespace Services
{
    public class ReportService : IReport
    {
        private readonly IUnitOfWork _unitOfWork;
        
        public ReportService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));            
        }
        public DailyReport GetDailyReport(DateTime date)
        {            
            DateTime endDate = new DateTime(date.Year, date.Month, date.Day, 23, 59, 59);
            DataReport dataReport = GetDataReport(date, endDate);

            return new DailyReport(date, dataReport.Incomes, dataReport.Expenses, dataReport.TotalIncomes, dataReport.TotalExpenses);
        }

        public DatePeriodReport GetDatePeriodReport(DateTime startDate, DateTime inputEndDate)
        {
            DateTime endDate = new DateTime(inputEndDate.Year, inputEndDate.Month, inputEndDate.Day, 23, 59, 59);
            DataReport dataReport = GetDataReport(startDate, endDate);

            return new DatePeriodReport(startDate, endDate, dataReport.Incomes, dataReport.Expenses, dataReport.TotalIncomes, dataReport.TotalExpenses);
        }

        private DataReport GetDataReport(DateTime startDate, DateTime endDate)
        {
            decimal totalIncomes = default, totalExpenses = default;
            List<Income> incomes = _unitOfWork.Incomes.ReadAll().Where(x => x.Date >= startDate & x.Date <= endDate).OrderBy(i=>i.Date).ToList();
            if (incomes.Any())
            {
                totalIncomes = incomes.Select(x => x.Amount).Aggregate((x, y) => x + y);
            }
            List<Expense> expenses = _unitOfWork.Expenses.ReadAll().Where(x => x.Date >= startDate & x.Date <= endDate).OrderBy(e=>e.Date).ToList();
            if (expenses.Any())
            {
                totalExpenses = expenses.Select(x => x.Amount).Aggregate((x, y) => x + y);
            }
            return new DataReport(incomes,expenses,totalIncomes,totalExpenses);
        }
    }
}
