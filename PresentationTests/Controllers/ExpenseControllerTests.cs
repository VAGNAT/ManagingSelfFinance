using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using Moq;
using Presentation.UnitTests;

namespace Presentation.Controllers.Tests
{
    [TestClass()]
    public class ExpenseControllerTests : BaseUnitTest
    {
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException), "Exception was not thrown")]
        public void ExpenseController_ConstructorTest_FirstParametrNull() =>
            new ExpenseController(null, MockTypeExpenseService.Object, MockLoggerExpenseController.Object);

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException), "Exception was not thrown")]
        public void ExpenseController_ConstructorTest_SecondParametrNull() =>
            new ExpenseController(MockExpenseService.Object, null, MockLoggerExpenseController.Object);

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException), "Exception was not thrown")]
        public void ExpenseController_ConstructorTest_ThirdParametrNull() =>
            new ExpenseController(MockExpenseService.Object, MockTypeExpenseService.Object, null);

        [TestMethod()]
        public void GetTest_dependency()
        {
            // act
            ExpenseController.Get();

            // assert            
            MockExpenseService.Verify(x => x.GetAll());
        }

        [TestMethod()]
        public void GetTest_WithParametr_dependency()
        {
            //arrange
            int expected = PositiveRandomNumber;

            // act
            ExpenseController.Get(expected);

            // assert            
            MockExpenseService.Verify(x => x.GetById(It.Is<int>(val=>val.Equals(expected))));
        }

        [TestMethod()]
        public async Task PostTest_dependency()
        {
            //arrange
            MockExpenseService.Setup(e => e.GetByIdAsync(It.IsAny<int>())).Returns(Task.FromResult(FakeExpense));
            MockTypeExpenseService.Setup(e => e.GetByIdAsync(It.IsAny<int>())).Returns(Task.FromResult(FakeTypeExpense));

            // act
            await ExpenseController.Post(FakeExpense);

            //assert
            MockExpenseService.Verify(x => x.GetByIdAsync(It.Is<int>(val => val.Equals(FakeExpense.Id))));
            MockTypeExpenseService.Verify(x => x.GetByIdAsync(It.Is<int>(val => val.Equals(FakeExpense.TypeExpense.Id))));
            MockExpenseService.Verify(x => x.Update(It.Is<Expense>(val => val.Equals(FakeExpense)), It.Is<Expense>(val => val.Equals(FakeExpense))));
        }

        [TestMethod()]
        public async Task PostTest_type_not_exist_dependency()
        {
            //arrange
            MockExpenseService.Setup(e => e.GetByIdAsync(It.IsAny<int>())).Returns(Task.FromResult(FakeExpense));

            // act
            await ExpenseController.Post(FakeExpense);

            //assert
            MockExpenseService.Verify(x => x.GetByIdAsync(It.Is<int>(val => val.Equals(FakeExpense.Id))));
            MockTypeExpenseService.Verify(x => x.GetByIdAsync(It.Is<int>(val => val.Equals(FakeExpense.TypeExpense.Id))));
            MockExpenseService.Verify(x => x.Update(It.IsAny<Expense>(), It.IsAny<Expense>()), Times.Never);
        }

        [TestMethod()]
        public async Task PostTest_not_exist_value_dependency()
        {
            // act
            await ExpenseController.Post(FakeExpense);

            //assert
            MockExpenseService.Verify(x => x.GetByIdAsync(It.Is<int>(val => val.Equals(FakeExpense.Id))));
            MockTypeExpenseService.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Never);
            MockExpenseService.Verify(x => x.Update(It.IsAny<Expense>(), It.IsAny<Expense>()), Times.Never);
        }

        [TestMethod()]
        public async Task PostTest_parametr_null_dependency()
        {
            // act
            await ExpenseController.Post(null);

            //assert
            MockExpenseService.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Never);
            MockTypeExpenseService.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Never);
            MockExpenseService.Verify(x => x.Update(It.IsAny<Expense>(), It.IsAny<Expense>()), Times.Never);
        }

        [TestMethod()]
        public async Task PutTest_dependency()
        {
            //arrange            
            MockTypeExpenseService.Setup(e => e.GetByIdAsync(It.IsAny<int>())).Returns(Task.FromResult(FakeTypeExpense));

            // act
            await ExpenseController.Put(FakeExpense);

            //assert            
            MockTypeExpenseService.Verify(x => x.GetByIdAsync(It.Is<int>(val => val.Equals(FakeExpense.TypeExpense.Id))));
            MockExpenseService.Verify(x => x.AddAsync(It.Is<Expense>(val => val.Equals(FakeExpense))));
        }

        [TestMethod()]
        public async Task PutTest_type_not_exist_dependency()
        {
            // act
            await ExpenseController.Put(FakeExpense);

            //assert            
            MockTypeExpenseService.Verify(x => x.GetByIdAsync(It.Is<int>(val => val.Equals(FakeExpense.TypeExpense.Id))));
            MockExpenseService.Verify(x => x.AddAsync(It.IsAny<Expense>()), Times.Never);
        }

        [TestMethod()]
        public async Task PutTest_parametr_null_dependency()
        {
            // act
            await ExpenseController.Put(null);

            //assert            
            MockTypeExpenseService.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Never);
            MockExpenseService.Verify(x => x.AddAsync(It.IsAny<Expense>()), Times.Never);
        }

        [TestMethod()]
        public async Task DeleteTest_dependency()
        {
            //arrange
            int expected = PositiveRandomNumber;

            // act
            await ExpenseController.Delete(expected);

            //assert                        
            MockExpenseService.Verify(x => x.Delete(It.Is<int>(val => val.Equals(expected))));
        }
    }
}