using Bogus;
using Model;

namespace Presentation.FakeData
{
    public class FakeExpenseData
    {
        public FakeExpenseData(int seed)
        {
            Valid = Valid.UseSeed(seed);
        }

        public Faker<Expense> Valid { get; private set; }
            = new Faker<Expense>("en")
                .CustomInstantiator(e => new Expense { Date = DateTime.Now, TypeExpense = new TypeExpense(), Amount = e.Finance.Random.Decimal()});
    }
}
