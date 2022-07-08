using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using Moq;
using Presentation.UnitTests;

namespace Presentation.Controllers.Tests
{
    [TestClass()]
    public class TypeExpenseControllerTests : BaseUnitTest
    {
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException), "Exception was not thrown")]
        public void TypeExpenseController_FirstParametrNull_ConstructorTest()
        {
            new TypeExpenseController(null, MockLoggerTypeExpenseController.Object);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException), "Exception was not thrown")]
        public void TypeExpenseController_SecondParametrNull_ConstructorTest()
        {
            new TypeExpenseController(MockTypeExpenseService.Object, null);
        }

        [TestMethod()]
        public void GetTest_dependency()
        {
            //act
            TypeExpenseController.Get();

            //assert
            MockTypeExpenseService.Verify(s => s.GetAll());
        }

        [TestMethod()]
        public async Task GetTest_With_parametr_dependency()
        {
            //arrange
            int expected = PositiveRandomNumber;

            //act
            await TypeExpenseController.Get(expected);

            //assert
            MockTypeExpenseService.Verify(s => s.GetByIdAsync(It.Is<int>(val => val.Equals(expected))));
        }

        [TestMethod()]
        public async Task PostTest_dependency()
        {
            //arrange
            MockTypeExpenseService.Setup(s => s.GetByIdAsync(It.IsAny<int>())).Returns(Task.FromResult(FakeTypeExpense));

            //act
            await TypeExpenseController.Post(FakeTypeExpense);

            //assert
            MockTypeExpenseService.Verify(s => s.GetByIdAsync(It.Is<int>(val => val.Equals(FakeTypeExpense.Id))));
            MockTypeExpenseService.Verify(s => s.Update(It.Is<TypeExpense>(val => val.Equals(FakeTypeExpense)),
                It.Is<TypeExpense>(val => val.Equals(FakeTypeExpense))));
        }

        [TestMethod()]
        public async Task PostTest_not_exist_value_dependency()
        {
            //act
            await TypeExpenseController.Post(FakeTypeExpense);

            //assert
            MockTypeExpenseService.Verify(s => s.GetByIdAsync(It.Is<int>(val => val.Equals(FakeTypeExpense.Id))));
            MockTypeExpenseService.Verify(s => s.Update(It.IsAny<TypeExpense>(), It.IsAny<TypeExpense>()), Times.Never);
        }

        [TestMethod()]
        public async Task PostTest_parametr_null_dependency()
        {
            //act
            await TypeExpenseController.Post(null);

            //assert
            MockTypeExpenseService.Verify(s => s.GetByIdAsync(It.IsAny<int>()), Times.Never);
            MockTypeExpenseService.Verify(s => s.Update(It.IsAny<TypeExpense>(), It.IsAny<TypeExpense>()), Times.Never);
        }

        [TestMethod()]
        public async Task PutTest_dependency()
        {
            //act
            await TypeExpenseController.Put(FakeTypeExpense);

            //assert
            MockTypeExpenseService.Verify(s => s.AddAsync(It.Is<TypeExpense>(val => val.Equals(FakeTypeExpense))));
        }

        [TestMethod()]
        public async Task PutTest_parametr_null_dependency()
        {
            //act
            await TypeExpenseController.Put(null);

            //assert
            MockTypeExpenseService.Verify(s => s.AddAsync(It.IsAny<TypeExpense>()), Times.Never);
        }

        [TestMethod()]
        public async Task DeleteTest_dependency()
        {
            //arrange
            int expected = PositiveRandomNumber;

            //act
            await TypeExpenseController.Delete(expected);

            //assert
            MockTypeExpenseService.Verify(s => s.Delete(expected));
        }
    }
}