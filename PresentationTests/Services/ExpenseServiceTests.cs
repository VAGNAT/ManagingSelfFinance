using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using Moq;
using Presentation.UnitTests;
using Services;

namespace FinanceUnitTests.Services
{
    [TestClass()]
    public class ExpenseServiceTests : BaseUnitTest
    {
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException), "Exception was not thrown")]
        public void ExpenseService_ConstructorTest()
        {
            new ExpenseService(null);
        }

        [TestMethod()]
        public async Task AddAsyncTest_dependency()
        {
            //arrange            
            MockUnitOfWork.Setup(x => x.Expenses.CreateAsync(It.IsAny<Expense>()));

            //act
            Expense actual = await ExpenseService.AddAsync(FakeExpense);

            //assert
            MockUnitOfWork.Verify(u => u.Expenses.CreateAsync(It.Is<Expense>(val => val.Equals(FakeExpense))));
            MockUnitOfWork.Verify(u => u.SaveAsync());
            Assert.AreEqual(FakeExpense, actual);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException), "Exception was not thrown")]
        public async Task AddAsyncTest_parametr_null()
        {
            //act
            await ExpenseService.AddAsync(null);
        }

        [TestMethod()]
        public async Task DeleteTest_dependency()
        {
            //arrange
            int expected = PositiveRandomNumber;
            MockUnitOfWork.Setup(x => x.Expenses.ReadAsync(It.IsAny<int>())).Returns(Task.FromResult(FakeExpense));
            MockUnitOfWork.Setup(x => x.Expenses.Delete(It.IsAny<Expense>()));

            //act
            bool actual = await ExpenseService.Delete(expected);

            //assert
            MockUnitOfWork.Verify(u => u.Expenses.ReadAsync(It.Is<int>(val => val.Equals(expected))));
            MockUnitOfWork.Verify(u => u.Expenses.Delete(It.Is<Expense>(val => val.Equals(FakeExpense))));
            MockUnitOfWork.Verify(u => u.SaveAsync());
            Assert.IsTrue(actual);
        }

        [TestMethod()]
        public async Task DeleteTest_not_exist_item()
        {
            //arrange            
            int expected = PositiveRandomNumber;
            MockUnitOfWork.Setup(x => x.Expenses.ReadAsync(It.IsAny<int>()));

            //act
            bool actual = await ExpenseService.Delete(expected);

            //assert
            MockUnitOfWork.Verify(u => u.Expenses.ReadAsync(It.Is<int>(val => val.Equals(expected))));
            MockUnitOfWork.Verify(u => u.Expenses.Delete(It.Is<Expense>(val => val.Equals(FakeExpense))), Times.Never);
            MockUnitOfWork.Verify(u => u.SaveAsync(), Times.Never);
            Assert.IsFalse(actual);
        }

        [TestMethod()]
        public async Task GetByIdAsyncTest_dependency()
        {
            //arrange
            int expected = PositiveRandomNumber;
            MockUnitOfWork.Setup(x => x.Expenses.ReadAsync(It.IsAny<int>())).Returns(Task.FromResult(FakeExpense));

            //act
            Expense actualExpense = await ExpenseService.GetByIdAsync(expected);

            //assert
            MockUnitOfWork.Verify(u => u.Expenses.ReadAsync(It.Is<int>(val => val.Equals(expected))));
            Assert.AreEqual(FakeExpense, actualExpense);
        }

        [TestMethod()]
        public void GetByIdTest_dependency()
        {
            //arrange
            int expected = PositiveRandomNumber;
            MockUnitOfWork.Setup(x => x.Expenses.Read(It.IsAny<int>())).Returns(FakeExpense);

            //act
            Expense actualExpense = ExpenseService.GetById(expected);

            //assert
            MockUnitOfWork.Verify(u => u.Expenses.Read(It.Is<int>(val => val.Equals(expected))));
            Assert.AreEqual(FakeExpense, actualExpense);
        }

        [TestMethod()]
        public void GetAllTest_dependency()
        {
            //arrange            
            MockUnitOfWork.Setup(x => x.Expenses.ReadAll()).Returns(FakeExpenses);

            //act
            List<Expense> actualExpenses = ExpenseService.GetAll().ToList();

            //assert
            MockUnitOfWork.Verify(u => u.Expenses.ReadAll());
            CollectionAssert.AreEqual(FakeExpenses, actualExpenses);
        }

        [TestMethod()]
        public void UpdateTest_dependency()
        {
            //arrange
            Expense expense = new Expense { Date = DateTime.Now.AddDays(-1), Amount = 1 };
            MockUnitOfWork.Setup(x => x.Expenses.Update(It.IsAny<Expense>()));

            //act
            Expense actualExpense = ExpenseService.Update(FakeExpense, expense);
            FakeExpense.Date = expense.Date;
            FakeExpense.Amount = expense.Amount;

            //assert
            MockUnitOfWork.Verify(u => u.Expenses.Update(It.Is<Expense>(val => val.Equals(FakeExpense))));
            MockUnitOfWork.Verify(u => u.SaveAsync());
            Assert.AreEqual(FakeExpense, actualExpense);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException), "Exception was not thrown")]
        public void UpdateTest_first_parametr_null()
        {
            ExpenseService.Update(null, FakeExpense);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException), "Exception was not thrown")]
        public void UpdateTest_second_parametr_null()
        {
            ExpenseService.Update(FakeExpense, null);
        }
    }
}