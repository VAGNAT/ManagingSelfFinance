using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using Moq;
using Presentation.UnitTests;

namespace Presentation.Controllers.Tests
{
    [TestClass()]
    public class IncomeControllerTests : BaseUnitTest
    {
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException), "Exception was not thrown")]
        public void IncomeController_ConstructorTest_FirstParametrNull() =>
            new IncomeController(null, MockTypeIncomeService.Object, MockLoggerIncomeController.Object);

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException), "Exception was not thrown")]
        public void IncomeController_ConstructorTest_SecondParametrNull() =>
            new IncomeController(MockIncomeService.Object, null, MockLoggerIncomeController.Object);

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException), "Exception was not thrown")]
        public void IncomeController_ConstructorTest_ThirdParametrNull() =>
            new IncomeController(MockIncomeService.Object, MockTypeIncomeService.Object, null);

        [TestMethod()]
        public void GetTest_dependency()
        {
            // act
            IncomeController.Get();

            // assert            
            MockIncomeService.Verify(x => x.GetAll());
        }

        [TestMethod()]
        public void GetTest_WithParametr_dependency()
        {
            //arrange
            int expected = PositiveRandomNumber;

            // act
            IncomeController.Get(expected);

            // assert            
            MockIncomeService.Verify(x => x.GetById(It.Is<int>(val => val.Equals(expected))));
        }

        [TestMethod()]
        public async Task PostTest_dependency()
        {
            //arrange
            MockIncomeService.Setup(e => e.GetByIdAsync(It.IsAny<int>())).Returns(Task.FromResult(FakeIncome));
            MockTypeIncomeService.Setup(e => e.GetByIdAsync(It.IsAny<int>())).Returns(Task.FromResult(FakeTypeIncome));

            // act
            await IncomeController.Post(FakeIncome);

            //assert
            MockIncomeService.Verify(x => x.GetByIdAsync(It.Is<int>(val => val.Equals(FakeIncome.Id))));
            MockTypeIncomeService.Verify(x => x.GetByIdAsync(It.Is<int>(val => val.Equals(FakeIncome.TypeIncome.Id))));
            MockIncomeService.Verify(x => x.Update(It.Is<Income>(val => val.Equals(FakeIncome)), It.Is<Income>(val => val.Equals(FakeIncome))));
        }

        [TestMethod()]
        public async Task PostTest_type_not_exist_dependency()
        {
            //arrange
            MockIncomeService.Setup(e => e.GetByIdAsync(It.IsAny<int>())).Returns(Task.FromResult(FakeIncome));

            // act
            await IncomeController.Post(FakeIncome);

            //assert
            MockIncomeService.Verify(x => x.GetByIdAsync(It.Is<int>(val => val.Equals(FakeIncome.Id))));
            MockTypeIncomeService.Verify(x => x.GetByIdAsync(It.Is<int>(val => val.Equals(FakeIncome.TypeIncome.Id))));
            MockIncomeService.Verify(x => x.Update(It.IsAny<Income>(), It.IsAny<Income>()), Times.Never);
        }

        [TestMethod()]
        public async Task PostTest_not_exist_value_dependency()
        {
            // act
            await IncomeController.Post(FakeIncome);

            //assert
            MockIncomeService.Verify(x => x.GetByIdAsync(It.Is<int>(val => val.Equals(FakeIncome.Id))));
            MockTypeIncomeService.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Never);
            MockIncomeService.Verify(x => x.Update(It.IsAny<Income>(), It.IsAny<Income>()), Times.Never);
        }

        [TestMethod()]
        public async Task PostTest_parametr_null_dependency()
        {
            // act
            await IncomeController.Post(null);

            //assert
            MockIncomeService.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Never);
            MockTypeIncomeService.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Never);
            MockIncomeService.Verify(x => x.Update(It.IsAny<Income>(), It.IsAny<Income>()), Times.Never);
        }

        [TestMethod()]
        public async Task PutTest_dependency()
        {
            //arrange            
            MockTypeIncomeService.Setup(e => e.GetByIdAsync(It.IsAny<int>())).Returns(Task.FromResult(FakeTypeIncome));

            // act
            await IncomeController.Put(FakeIncome);

            //assert            
            MockTypeIncomeService.Verify(x => x.GetByIdAsync(It.Is<int>(val => val.Equals(FakeIncome.TypeIncome.Id))));
            MockIncomeService.Verify(x => x.AddAsync(It.Is<Income>(val => val.Equals(FakeIncome))));
        }

        [TestMethod()]
        public async Task PutTest_type_not_exist_dependency()
        {
            // act
            await IncomeController.Put(FakeIncome);

            //assert            
            MockTypeIncomeService.Verify(x => x.GetByIdAsync(It.Is<int>(val => val.Equals(FakeIncome.TypeIncome.Id))));
            MockIncomeService.Verify(x => x.AddAsync(It.IsAny<Income>()), Times.Never);
        }

        [TestMethod()]
        public async Task PutTest_parametr_null_dependency()
        {
            // act
            await IncomeController.Put(null);

            //assert            
            MockTypeIncomeService.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Never);
            MockIncomeService.Verify(x => x.AddAsync(It.IsAny<Income>()), Times.Never);
        }

        [TestMethod()]
        public async Task DeleteTest_dependency()
        {
            //arrange
            int expected = PositiveRandomNumber;

            // act
            await IncomeController.Delete(expected);

            //assert                        
            MockIncomeService.Verify(x => x.Delete(It.Is<int>(val => val.Equals(expected))));
        }
    }
}