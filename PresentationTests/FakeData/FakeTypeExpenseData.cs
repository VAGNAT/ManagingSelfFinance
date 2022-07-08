using Bogus;
using Model;

namespace Presentation.FakeData
{
    public class FakeTypeExpenseData
    {
        public FakeTypeExpenseData(int seed)
        {
            Valid = Valid.UseSeed(seed);
        }

        public Faker<TypeExpense> Valid { get; private set; }
            = new Faker<TypeExpense>("en")
                .CustomInstantiator(e => new TypeExpense { Name = e.Lorem.Text()});
    }
}
