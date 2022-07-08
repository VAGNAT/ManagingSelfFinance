using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using Moq;
using Presentation.Controllers;
using Presentation.UnitTests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers.Tests
{
    [TestClass()]
    public class TypeIncomeControllerTests : BaseUnitTest
    {
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException), "Exception was not thrown")]
        public void TypeIncomeController_FirstParametrNull_ConstructorTest()
        {
            new TypeIncomeController(null, MockLoggerTypeIncomeController.Object);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException), "Exception was not thrown")]
        public void TypeIncomeController_SecondParametrNull_ConstructorTest()
        {
            new TypeIncomeController(MockTypeIncomeService.Object, null);
        }

        [TestMethod()]
        public void GetTest_dependency()
        {
            //act
            TypeIncomeController.Get();

            //assert
            MockTypeIncomeService.Verify(s => s.GetAll());
        }

        [TestMethod()]
        public async Task GetTest_With_parametr_dependency()
        {
            //arrange
            int expected = PositiveRandomNumber;

            //act
            await TypeIncomeController.Get(expected);

            //assert
            MockTypeIncomeService.Verify(s => s.GetByIdAsync(It.Is<int>(val => val.Equals(expected))));
        }

        [TestMethod()]
        public async Task PostTest_dependency()
        {
            //arrange
            MockTypeIncomeService.Setup(s => s.GetByIdAsync(It.IsAny<int>())).Returns(Task.FromResult(FakeTypeIncome));

            //act
            await TypeIncomeController.Post(FakeTypeIncome);

            //assert
            MockTypeIncomeService.Verify(s => s.GetByIdAsync(It.Is<int>(val => val.Equals(FakeTypeIncome.Id))));
            MockTypeIncomeService.Verify(s => s.Update(It.Is<TypeIncome>(val => val.Equals(FakeTypeIncome)),
                It.Is<TypeIncome>(val => val.Equals(FakeTypeIncome))));
        }

        [TestMethod()]
        public async Task PostTest_not_exist_value_dependency()
        {
            //act
            await TypeIncomeController.Post(FakeTypeIncome);

            //assert
            MockTypeIncomeService.Verify(s => s.GetByIdAsync(It.Is<int>(val => val.Equals(FakeTypeIncome.Id))));
            MockTypeIncomeService.Verify(s => s.Update(It.IsAny<TypeIncome>(), It.IsAny<TypeIncome>()), Times.Never);
        }

        [TestMethod()]
        public async Task PostTest_parametr_null_dependency()
        {
            //act
            await TypeIncomeController.Post(null);

            //assert
            MockTypeIncomeService.Verify(s => s.GetByIdAsync(It.IsAny<int>()), Times.Never);
            MockTypeIncomeService.Verify(s => s.Update(It.IsAny<TypeIncome>(), It.IsAny<TypeIncome>()), Times.Never);
        }

        [TestMethod()]
        public async Task PutTest_dependency()
        {
            //act
            await TypeIncomeController.Put(FakeTypeIncome);

            //assert
            MockTypeIncomeService.Verify(s => s.AddAsync(It.Is<TypeIncome>(val => val.Equals(FakeTypeIncome))));
        }

        [TestMethod()]
        public async Task PutTest_parametr_null_dependency()
        {
            //act
            await TypeIncomeController.Put(null);

            //assert
            MockTypeIncomeService.Verify(s => s.AddAsync(It.IsAny<TypeIncome>()), Times.Never);
        }

        [TestMethod()]
        public async Task DeleteTest_dependency()
        {
            //arrange
            int expected = PositiveRandomNumber;

            //act
            await TypeIncomeController.Delete(expected);

            //assert
            MockTypeIncomeService.Verify(s => s.Delete(expected));
        }
    }
}