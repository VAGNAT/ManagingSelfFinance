using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using Moq;
using Presentation.UnitTests;
using Services;

namespace FinanceUnitTests.Services
{
    [TestClass()]
    public class TypeIncomeServiceTests : BaseUnitTest
    {
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException), "Exception was not thrown")]
        public void TypeIncomeService_ConstructorTest()
        {
            new TypeIncomeService(null);
        }

        [TestMethod()]
        public async Task AddAsyncTest_dependency()
        {
            //arrange            
            MockUnitOfWork.Setup(x => x.TypeIncomes.CreateAsync(It.IsAny<TypeIncome>()));

            //act
            TypeIncome actual = await TypeIncomeService.AddAsync(FakeTypeIncome);

            //assert
            MockUnitOfWork.Verify(u => u.TypeIncomes.CreateAsync(It.Is<TypeIncome>(val => val.Equals(FakeTypeIncome))));
            MockUnitOfWork.Verify(u => u.SaveAsync());
            Assert.AreEqual(FakeTypeIncome, actual);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException), "Exception was not thrown")]
        public async Task AddAsyncTest_parametr_null()
        {
            //act
            await TypeIncomeService.AddAsync(null);
        }

        [TestMethod()]
        public async Task DeleteTest_dependency()
        {
            //arrange
            int expected = PositiveRandomNumber;
            MockUnitOfWork.Setup(x => x.TypeIncomes.ReadAsync(It.IsAny<int>())).Returns(Task.FromResult(FakeTypeIncome));
            MockUnitOfWork.Setup(x => x.TypeIncomes.Delete(It.IsAny<TypeIncome>()));

            //act
            bool actual = await TypeIncomeService.Delete(expected);

            //assert
            MockUnitOfWork.Verify(u => u.TypeIncomes.ReadAsync(It.Is<int>(val => val.Equals(expected))));
            MockUnitOfWork.Verify(u => u.TypeIncomes.Delete(It.Is<TypeIncome>(val => val.Equals(FakeTypeIncome))));
            MockUnitOfWork.Verify(u => u.SaveAsync());
            Assert.IsTrue(actual);
        }

        [TestMethod()]
        public async Task DeleteTest_not_exist_item()
        {
            //arrange            
            int expected = PositiveRandomNumber;
            MockUnitOfWork.Setup(x => x.TypeIncomes.ReadAsync(It.IsAny<int>()));

            //act
            bool actual = await TypeIncomeService.Delete(expected);

            //assert
            MockUnitOfWork.Verify(u => u.TypeIncomes.ReadAsync(It.Is<int>(val => val.Equals(expected))));
            MockUnitOfWork.Verify(u => u.TypeIncomes.Delete(It.Is<TypeIncome>(val => val.Equals(FakeTypeIncome))), Times.Never);
            MockUnitOfWork.Verify(u => u.SaveAsync(), Times.Never);
            Assert.IsFalse(actual);
        }

        [TestMethod()]
        public async Task GetByIdAsyncTest_dependency()
        {
            //arrange
            int expected = PositiveRandomNumber;
            MockUnitOfWork.Setup(x => x.TypeIncomes.ReadAsync(It.IsAny<int>())).Returns(Task.FromResult(FakeTypeIncome));

            //act
            TypeIncome actualTypeIncome = await TypeIncomeService.GetByIdAsync(expected);

            //assert
            MockUnitOfWork.Verify(u => u.TypeIncomes.ReadAsync(It.Is<int>(val => val.Equals(expected))));
            Assert.AreEqual(FakeTypeIncome, actualTypeIncome);
        }

        [TestMethod()]
        public void GetByIdTest_dependency()
        {
            //arrange
            int expected = PositiveRandomNumber;
            MockUnitOfWork.Setup(x => x.TypeIncomes.Read(It.IsAny<int>())).Returns(FakeTypeIncome);

            //act
            TypeIncome actualTypeIncome = TypeIncomeService.GetById(expected);

            //assert
            MockUnitOfWork.Verify(u => u.TypeIncomes.Read(It.Is<int>(val => val.Equals(expected))));
            Assert.AreEqual(FakeTypeIncome, actualTypeIncome);
        }

        [TestMethod()]
        public void GetAllTest_dependency()
        {
            //arrange            
            MockUnitOfWork.Setup(x => x.TypeIncomes.ReadAll()).Returns(FakeTypeIncomes);

            //act
            List<TypeIncome> actualTypeIncomes = TypeIncomeService.GetAll().ToList();

            //assert
            MockUnitOfWork.Verify(u => u.TypeIncomes.ReadAll());
            CollectionAssert.AreEqual(FakeTypeIncomes, actualTypeIncomes);
        }

        [TestMethod()]
        public void UpdateTest_dependency()
        {
            //arrange
            TypeIncome typeIncome = new TypeIncome { Name = "abc" };
            MockUnitOfWork.Setup(x => x.TypeIncomes.Update(It.IsAny<TypeIncome>()));

            //act
            TypeIncome actualTypeIncome = TypeIncomeService.Update(FakeTypeIncome, typeIncome);
            FakeTypeIncome.Name = typeIncome.Name;

            //assert
            MockUnitOfWork.Verify(u => u.TypeIncomes.Update(It.Is<TypeIncome>(val => val.Equals(FakeTypeIncome))));
            MockUnitOfWork.Verify(u => u.SaveAsync());
            Assert.AreEqual(FakeTypeIncome, actualTypeIncome);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException), "Exception was not thrown")]
        public void UpdateTest_first_parametr_null()
        {
            TypeIncomeService.Update(null, FakeTypeIncome);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException), "Exception was not thrown")]
        public void UpdateTest_second_parametr_null()
        {
            TypeIncomeService.Update(FakeTypeIncome, null);
        }
    }
}