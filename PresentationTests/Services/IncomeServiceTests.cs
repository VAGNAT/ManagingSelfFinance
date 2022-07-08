using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using Moq;
using Presentation.UnitTests;
using Services;

namespace FinanceUnitTests.Services
{
    [TestClass()]
    public class IncomeServiceTests : BaseUnitTest
    {
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException), "Exception was not thrown")]
        public void IncomeService_ConstructorTest()
        {
            new IncomeService(null);
        }

        [TestMethod()]
        public async Task AddAsyncTest_dependency()
        {
            //arrange            
            MockUnitOfWork.Setup(x => x.Incomes.CreateAsync(It.IsAny<Income>()));

            //act
            Income actual = await IncomeService.AddAsync(FakeIncome);

            //assert
            MockUnitOfWork.Verify(u => u.Incomes.CreateAsync(It.Is<Income>(val => val.Equals(FakeIncome))));
            MockUnitOfWork.Verify(u => u.SaveAsync());
            Assert.AreEqual(FakeIncome, actual);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException), "Exception was not thrown")]
        public async Task AddAsyncTest_parametr_null()
        {
            //act
            await IncomeService.AddAsync(null);
        }

        [TestMethod()]
        public async Task DeleteTest_dependency()
        {
            //arrange
            int expected = PositiveRandomNumber;
            MockUnitOfWork.Setup(x => x.Incomes.ReadAsync(It.IsAny<int>())).Returns(Task.FromResult(FakeIncome));
            MockUnitOfWork.Setup(x => x.Incomes.Delete(It.IsAny<Income>()));

            //act
            bool actual = await IncomeService.Delete(expected);

            //assert
            MockUnitOfWork.Verify(u => u.Incomes.ReadAsync(It.Is<int>(val => val.Equals(expected))));
            MockUnitOfWork.Verify(u => u.Incomes.Delete(It.Is<Income>(val => val.Equals(FakeIncome))));
            MockUnitOfWork.Verify(u => u.SaveAsync());
            Assert.IsTrue(actual);
        }

        [TestMethod()]
        public async Task DeleteTest_not_exist_item()
        {
            //arrange            
            int expected = PositiveRandomNumber;
            MockUnitOfWork.Setup(x => x.Incomes.ReadAsync(It.IsAny<int>()));

            //act
            bool actual = await IncomeService.Delete(expected);

            //assert
            MockUnitOfWork.Verify(u => u.Incomes.ReadAsync(It.Is<int>(val => val.Equals(expected))));
            MockUnitOfWork.Verify(u => u.Incomes.Delete(It.Is<Income>(val => val.Equals(FakeIncome))), Times.Never);
            MockUnitOfWork.Verify(u => u.SaveAsync(), Times.Never);
            Assert.IsFalse(actual);
        }

        [TestMethod()]
        public async Task GetByIdAsyncTest_dependency()
        {
            //arrange
            int expected = PositiveRandomNumber;
            MockUnitOfWork.Setup(x => x.Incomes.ReadAsync(It.IsAny<int>())).Returns(Task.FromResult(FakeIncome));

            //act
            Income actualIncome = await IncomeService.GetByIdAsync(expected);

            //assert
            MockUnitOfWork.Verify(u => u.Incomes.ReadAsync(It.Is<int>(val => val.Equals(expected))));
            Assert.AreEqual(FakeIncome, actualIncome);
        }

        [TestMethod()]
        public void GetByIdTest_dependency()
        {
            //arrange
            int expected = PositiveRandomNumber;
            MockUnitOfWork.Setup(x => x.Incomes.Read(It.IsAny<int>())).Returns(FakeIncome);

            //act
            Income actualIncome = IncomeService.GetById(expected);

            //assert
            MockUnitOfWork.Verify(u => u.Incomes.Read(It.Is<int>(val => val.Equals(expected))));
            Assert.AreEqual(FakeIncome, actualIncome);
        }

        [TestMethod()]
        public void GetAllTest_dependency()
        {
            //arrange            
            MockUnitOfWork.Setup(x => x.Incomes.ReadAll()).Returns(FakeIncomes);

            //act
            List<Income> actualIncomes = IncomeService.GetAll().ToList();

            //assert
            MockUnitOfWork.Verify(u => u.Incomes.ReadAll());
            CollectionAssert.AreEqual(FakeIncomes, actualIncomes);
        }

        [TestMethod()]
        public void UpdateTest_dependency()
        {
            //arrange
            Income income = new Income { Date = DateTime.Now.AddDays(-1), Amount = 1 };
            MockUnitOfWork.Setup(x => x.Incomes.Update(It.IsAny<Income>()));

            //act
            Income actualIncome = IncomeService.Update(FakeIncome, income);
            FakeIncome.Date = income.Date;
            FakeIncome.Amount = income.Amount;

            //assert
            MockUnitOfWork.Verify(u => u.Incomes.Update(It.Is<Income>(val => val.Equals(FakeIncome))));
            MockUnitOfWork.Verify(u => u.SaveAsync());
            Assert.AreEqual(FakeIncome, actualIncome);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException), "Exception was not thrown")]
        public void UpdateTest_first_parametr_null()
        {
            IncomeService.Update(null, FakeIncome);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException), "Exception was not thrown")]
        public void UpdateTest_second_parametr_null()
        {
            IncomeService.Update(FakeIncome, null);
        }
    }
}
