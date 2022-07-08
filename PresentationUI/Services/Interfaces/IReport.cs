using PresentationUI.Helpers;
using PresentationUI.Model;

namespace PresentationUI.Services.Interfaces
{
    public interface IReport
    {
        Task<DailyReport> GetDailyReportAsync(DateTime date);
        Task<DatePeriodReport> GetDatePeriodReportAsync(DateTime startDate, DateTime endDate);
        Task<DataChart> GetDataForChartAsync(int year);
    }
}
