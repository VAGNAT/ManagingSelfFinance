using Bogus;
using Infrastructure.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using Model.ViewModel;
using Moq;
using Presentation.Controllers;
using Presentation.FakeData;
using Services;
using Services.Interfaces;

namespace Presentation.UnitTests
{
    public abstract class BaseUnitTest
    {
        const int seed = 1234;
        protected Faker Faker { get; private set; }
        protected Mock<IUnitOfWork> MockUnitOfWork { get; private set; }
        protected Mock<ICRUD<Expense>> MockExpenseService { get; private set; }
        protected Mock<ICRUD<Income>> MockIncomeService { get; private set; }
        protected Mock<ICRUD<TypeExpense>> MockTypeExpenseService { get; private set; }
        protected Mock<ICRUD<TypeIncome>> MockTypeIncomeService { get; private set; }
        protected Mock<IReport> MockReportService { get; private set; }
        protected Mock<ILogger<ExpenseController>> MockLoggerExpenseController { get; private set; }
        protected Mock<ILogger<IncomeController>> MockLoggerIncomeController { get; private set; }
        protected Mock<ILogger<TypeExpenseController>> MockLoggerTypeExpenseController { get; private set; }
        protected Mock<ILogger<TypeIncomeController>> MockLoggerTypeIncomeController { get; private set; }
        protected Mock<ILogger<ReportController>> MockLoggerReportController { get; private set; }
        protected ExpenseController ExpenseController { get; private set; }
        protected IncomeController IncomeController { get; private set; }
        protected TypeExpenseController TypeExpenseController { get; private set; }
        protected TypeIncomeController TypeIncomeController { get; private set; }
        protected ReportController ReportController { get; private set; }
        protected ExpenseService ExpenseService { get; private set; }
        protected IncomeService IncomeService { get; private set; }
        protected TypeExpenseService TypeExpenseService { get; private set; }
        protected TypeIncomeService TypeIncomeService { get; private set; }
        protected ReportService ReportService { get; private set; }
        protected Expense FakeExpense { get; private set; }
        protected Income FakeIncome { get; private set; }
        protected TypeExpense FakeTypeExpense { get; private set; }
        protected TypeIncome FakeTypeIncome { get; private set; }
        protected DailyReport FakeDailyReport { get; private set; }
        protected DatePeriodReport FakeDatePeriodReport { get; private set; }
        protected List<Expense> FakeExpenses { get; private set; }
        protected List<Income> FakeIncomes { get; private set; }
        protected List<TypeExpense> FakeTypeExpenses { get; private set; }
        protected List<TypeIncome> FakeTypeIncomes { get; private set; }
        protected int PositiveRandomNumber => Faker.Random.Number(seed);

        [TestInitialize]
        public void Initialize()
        {
            Randomizer.Seed = new Random(seed);

            Faker = new Faker("en");

            MockUnitOfWork = new Mock<IUnitOfWork>();
            MockExpenseService = new Mock<ICRUD<Expense>>();
            MockIncomeService = new Mock<ICRUD<Income>>();
            MockTypeExpenseService = new Mock<ICRUD<TypeExpense>>();
            MockTypeIncomeService = new Mock<ICRUD<TypeIncome>>();
            MockReportService = new Mock<IReport>();
            MockLoggerExpenseController = new Mock<ILogger<ExpenseController>>();
            MockLoggerIncomeController = new Mock<ILogger<IncomeController>>();
            MockLoggerTypeExpenseController = new Mock<ILogger<TypeExpenseController>>();
            MockLoggerTypeIncomeController = new Mock<ILogger<TypeIncomeController>>();
            MockLoggerReportController = new Mock<ILogger<ReportController>>();

            ExpenseController = new ExpenseController(MockExpenseService.Object, MockTypeExpenseService.Object, MockLoggerExpenseController.Object);
            IncomeController = new IncomeController(MockIncomeService.Object, MockTypeIncomeService.Object, MockLoggerIncomeController.Object);
            TypeExpenseController = new TypeExpenseController(MockTypeExpenseService.Object, MockLoggerTypeExpenseController.Object);
            TypeIncomeController = new TypeIncomeController(MockTypeIncomeService.Object, MockLoggerTypeIncomeController.Object);
            ReportController = new ReportController(MockReportService.Object, MockLoggerReportController.Object);

            ExpenseService = new ExpenseService(MockUnitOfWork.Object);
            IncomeService = new IncomeService(MockUnitOfWork.Object);
            TypeExpenseService = new TypeExpenseService(MockUnitOfWork.Object);
            TypeIncomeService = new TypeIncomeService(MockUnitOfWork.Object);
            ReportService = new ReportService(MockUnitOfWork.Object);

            FakeTypeExpense = new Faker<TypeExpense>().RuleFor(e => e.Id, e => PositiveRandomNumber).RuleFor(e => e.Name, e => e.Lorem.Text());
            FakeTypeIncome = new Faker<TypeIncome>().RuleFor(i => i.Id, i => PositiveRandomNumber).RuleFor(i => i.Name, i => i.Lorem.Text());
            FakeExpense = new Faker<Expense>().RuleFor(e => e.Id, e => PositiveRandomNumber).RuleFor(e => e.Date, e => DateTime.Now)
                .RuleFor(e => e.TypeExpense, e => FakeTypeExpense).RuleFor(e => e.Amount, e => e.Finance.Random.Decimal(seed));
            FakeIncome = new Faker<Income>().RuleFor(i => i.Id, i => PositiveRandomNumber).RuleFor(i => i.Date, i => DateTime.Now)
                .RuleFor(i => i.TypeIncome, i => FakeTypeIncome).RuleFor(i => i.Amount, i => i.Finance.Random.Decimal(seed));
            FakeExpenses = new FakeExpenseData(seed).Valid.Generate(10);
            FakeIncomes = new FakeIncomeData(seed).Valid.Generate(10);
            FakeDailyReport = new DailyReport(DateTime.Now, FakeIncomes, FakeExpenses,
                FakeIncomes.Select(e => e.Amount).Aggregate((x, y) => x + y), FakeExpenses.Select(e => e.Amount).Aggregate((x, y) => x + y));                       
            FakeDatePeriodReport = new DatePeriodReport(DateTime.Now, DateTime.Now, FakeIncomes, FakeExpenses,
                FakeIncomes.Select(e => e.Amount).Aggregate((x, y) => x + y), FakeExpenses.Select(e => e.Amount).Aggregate((x, y) => x + y)); 
            FakeTypeExpenses = new FakeTypeExpenseData(seed).Valid.Generate(10);
            FakeTypeIncomes = new FakeTypeIncomeData(seed).Valid.Generate(10);
        }
    }
}
