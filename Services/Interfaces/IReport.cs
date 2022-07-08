using Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IReport
    {
        DailyReport GetDailyReport(DateTime date);
        DatePeriodReport GetDatePeriodReport(DateTime startDate, DateTime endDate);
    }
}
