using Bogus;
using Model;

namespace Presentation.FakeData
{
    public class FakeIncomeData
    {
        public FakeIncomeData(int seed)
        {
            Valid = Valid.UseSeed(seed);
        }

        public Faker<Income> Valid { get; private set; }
             = new Faker<Income>("en")
                 .CustomInstantiator(e => new Income { Date = DateTime.Now, TypeIncome = new TypeIncome(), Amount = e.Finance.Random.Decimal() });
    }
}
