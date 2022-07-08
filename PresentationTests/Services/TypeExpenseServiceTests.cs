using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using Moq;
using Presentation.UnitTests;
using Services;

namespace FinanceUnitTests.Services
{
    [TestClass()]
    public class TypeExpenseServiceTests : BaseUnitTest
    {
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException), "Exception was not thrown")]
        public void TypeExpenseService_ConstructorTest()
        {
            new TypeExpenseService(null);
        }

        [TestMethod()]
        public async Task AddAsyncTest_dependency()
        {
            //arrange            
            MockUnitOfWork.Setup(x => x.TypeExpenses.CreateAsync(It.IsAny<TypeExpense>()));

            //act
            TypeExpense actual = await TypeExpenseService.AddAsync(FakeTypeExpense);

            //assert
            MockUnitOfWork.Verify(u => u.TypeExpenses.CreateAsync(It.Is<TypeExpense>(val => val.Equals(FakeTypeExpense))));
            MockUnitOfWork.Verify(u => u.SaveAsync());
            Assert.AreEqual(FakeTypeExpense, actual);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException), "Exception was not thrown")]
        public async Task AddAsyncTest_parametr_null()
        {
            //act
            await TypeExpenseService.AddAsync(null);
        }

        [TestMethod()]
        public async Task DeleteTest_dependency()
        {
            //arrange
            int expected = PositiveRandomNumber;
            MockUnitOfWork.Setup(x => x.TypeExpenses.ReadAsync(It.IsAny<int>())).Returns(Task.FromResult(FakeTypeExpense));
            MockUnitOfWork.Setup(x => x.TypeExpenses.Delete(It.IsAny<TypeExpense>()));

            //act
            bool actual = await TypeExpenseService.Delete(expected);

            //assert
            MockUnitOfWork.Verify(u => u.TypeExpenses.ReadAsync(It.Is<int>(val => val.Equals(expected))));
            MockUnitOfWork.Verify(u => u.TypeExpenses.Delete(It.Is<TypeExpense>(val => val.Equals(FakeTypeExpense))));
            MockUnitOfWork.Verify(u => u.SaveAsync());
            Assert.IsTrue(actual);
        }

        [TestMethod()]
        public async Task DeleteTest_not_exist_item()
        {
            //arrange            
            int expected = PositiveRandomNumber;
            MockUnitOfWork.Setup(x => x.TypeExpenses.ReadAsync(It.IsAny<int>()));

            //act
            bool actual = await TypeExpenseService.Delete(expected);

            //assert
            MockUnitOfWork.Verify(u => u.TypeExpenses.ReadAsync(It.Is<int>(val => val.Equals(expected))));
            MockUnitOfWork.Verify(u => u.TypeExpenses.Delete(It.Is<TypeExpense>(val => val.Equals(FakeTypeExpense))), Times.Never);
            MockUnitOfWork.Verify(u => u.SaveAsync(), Times.Never);
            Assert.IsFalse(actual);
        }

        [TestMethod()]
        public async Task GetByIdAsyncTest_dependency()
        {
            //arrange
            int expected = PositiveRandomNumber;
            MockUnitOfWork.Setup(x => x.TypeExpenses.ReadAsync(It.IsAny<int>())).Returns(Task.FromResult(FakeTypeExpense));

            //act
            TypeExpense actualTypeExpense = await TypeExpenseService.GetByIdAsync(expected);

            //assert
            MockUnitOfWork.Verify(u => u.TypeExpenses.ReadAsync(It.Is<int>(val => val.Equals(expected))));
            Assert.AreEqual(FakeTypeExpense, actualTypeExpense);
        }

        [TestMethod()]
        public void GetByIdTest_dependency()
        {
            //arrange
            int expected = PositiveRandomNumber;
            MockUnitOfWork.Setup(x => x.TypeExpenses.Read(It.IsAny<int>())).Returns(FakeTypeExpense);

            //act
            TypeExpense actualTypeExpense = TypeExpenseService.GetById(expected);

            //assert
            MockUnitOfWork.Verify(u => u.TypeExpenses.Read(It.Is<int>(val => val.Equals(expected))));
            Assert.AreEqual(FakeTypeExpense, actualTypeExpense);
        }

        [TestMethod()]
        public void GetAllTest_dependency()
        {
            //arrange            
            MockUnitOfWork.Setup(x => x.TypeExpenses.ReadAll()).Returns(FakeTypeExpenses);

            //act
            List<TypeExpense> actualTypeExpenses = TypeExpenseService.GetAll().ToList();

            //assert
            MockUnitOfWork.Verify(u => u.TypeExpenses.ReadAll());
            CollectionAssert.AreEqual(FakeTypeExpenses.ToList(), actualTypeExpenses);
        }

        [TestMethod()]
        public void UpdateTest_dependency()
        {
            //arrange
            TypeExpense typeExpense = new TypeExpense { Name = "abc" };
            MockUnitOfWork.Setup(x => x.TypeExpenses.Update(It.IsAny<TypeExpense>()));

            //act
            TypeExpense actualTypeExpense = TypeExpenseService.Update(FakeTypeExpense, typeExpense);
            FakeTypeExpense.Name = typeExpense.Name;            

            //assert
            MockUnitOfWork.Verify(u => u.TypeExpenses.Update(It.Is<TypeExpense>(val => val.Equals(FakeTypeExpense))));
            MockUnitOfWork.Verify(u => u.SaveAsync());
            Assert.AreEqual(FakeTypeExpense, actualTypeExpense);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException), "Exception was not thrown")]
        public void UpdateTest_first_parametr_null()
        {
            TypeExpenseService.Update(null, FakeTypeExpense);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException), "Exception was not thrown")]
        public void UpdateTest_second_parametr_null()
        {
            TypeExpenseService.Update(FakeTypeExpense, null);
        }
    }
}
