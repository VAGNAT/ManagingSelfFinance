using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.ViewModel;
using Moq;
using Presentation.UnitTests;
using Services;

namespace FinanceUnitTests.Services
{
    [TestClass()]
    public class ReportServiceTests : BaseUnitTest
    {
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException), "Exception was not thrown")]
        public void ReportService_ConstructorTest()
        {
            new ReportService(null);
        }

        [TestMethod()]
        public void GetDailyReportTest()
        {
            //arrange
            DateTime date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            MockUnitOfWork.Setup(u => u.Incomes.ReadAll()).Returns(FakeIncomes);
            MockUnitOfWork.Setup(u => u.Expenses.ReadAll()).Returns(FakeExpenses);

            //act
            DailyReport actualReport = ReportService.GetDailyReport(date);

            //assert
            MockUnitOfWork.Verify(u => u.Incomes.ReadAll(), Times.Once);
            MockUnitOfWork.Verify(u => u.Expenses.ReadAll(), Times.Once);
            Assert.AreEqual(FakeDailyReport.TotalIncomes, actualReport.TotalIncomes);
            Assert.AreEqual(FakeDailyReport.TotalExpenses, actualReport.TotalExpenses);
            CollectionAssert.AreEqual(FakeDailyReport.Incomes, actualReport.Incomes);
            CollectionAssert.AreEqual(FakeDailyReport.Expenses, actualReport.Expenses);
        }

        [TestMethod()]
        public void GetDatePeriodReportTest()
        {
            //arrange
            DateTime startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            DateTime endDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            MockUnitOfWork.Setup(u => u.Incomes.ReadAll()).Returns(FakeIncomes);
            MockUnitOfWork.Setup(u => u.Expenses.ReadAll()).Returns(FakeExpenses);

            //act
            DatePeriodReport actualReport = ReportService.GetDatePeriodReport(startDate, endDate);

            //assert
            MockUnitOfWork.Verify(u => u.Incomes.ReadAll(), Times.Once);
            MockUnitOfWork.Verify(u => u.Expenses.ReadAll(), Times.Once);
            Assert.AreEqual(FakeDailyReport.TotalIncomes, actualReport.TotalIncomes);
            Assert.AreEqual(FakeDailyReport.TotalExpenses, actualReport.TotalExpenses);
            CollectionAssert.AreEqual(FakeDailyReport.Incomes, actualReport.Incomes);
            CollectionAssert.AreEqual(FakeDailyReport.Expenses, actualReport.Expenses);
        }
    }
}