using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Presentation.UnitTests;

namespace Presentation.Controllers.Tests
{
    [TestClass()]
    public class ReportControllerTests : BaseUnitTest
    {
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException), "Exception was not thrown")]
        public void ReportController_First_parametr_null_ConstructorTest()
        {
            new ReportController(null, MockLoggerReportController.Object);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException), "Exception was not thrown")]
        public void ReportController_Second_parametr_null_ConstructorTest()
        {
            new ReportController(MockReportService.Object, null);
        }

        [TestMethod()]
        public void GetTest_DailyReport()
        {
            //arrange
            string inputDate = DateTime.Now.ToShortDateString();
            DateTime.TryParse(inputDate, out DateTime date);

            //act
            ReportController.Get(inputDate);

            //assert
            MockReportService.Verify(s => s.GetDailyReport(It.Is<DateTime>(val => val.Equals(date))));
        }

        [TestMethod()]
        public void GetTest_DatePeriodReport()
        {
            //arrange
            string inputStartDate = DateTime.Now.ToShortDateString();
            string inputEndDate = DateTime.Now.AddDays(1).ToShortDateString();
            DateTime.TryParse(inputStartDate, out DateTime startDate);
            DateTime.TryParse(inputEndDate, out DateTime endDate);

            //act
            ReportController.Get(inputStartDate, inputEndDate);

            //assert
            MockReportService.Verify(s => s.GetDatePeriodReport(It.Is<DateTime>(val => val.Equals(startDate)), It.Is<DateTime>(val => val.Equals(endDate))));
        }
    }
}