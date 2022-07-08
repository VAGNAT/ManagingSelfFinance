using Bogus;
using Model;

namespace Presentation.FakeData
{
    public class FakeTypeIncomeData
    {
        public FakeTypeIncomeData(int seed)
        {
            Valid = Valid.UseSeed(seed);
        }

        public Faker<TypeIncome> Valid { get; private set; }
            = new Faker<TypeIncome>("en")
                .CustomInstantiator(e => new TypeIncome {Name = e.Lorem.Text() });
    }
}
