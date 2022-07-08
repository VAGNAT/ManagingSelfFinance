using Microsoft.EntityFrameworkCore;
using Model;

namespace Infrastructure.Data
{
    public class FinanceContext : DbContext
    {
        public DbSet<TypeIncome> TypeIncomes { get; set; }
        public DbSet<TypeExpense> TypeExpenses { get; set; }
        public DbSet<Income> Incomes { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public FinanceContext(DbContextOptions<FinanceContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TypeIncome>().HasData(
                new TypeIncome { Id = 1, Name = "Business" },
                new TypeIncome { Id = 2, Name = "Investments" },
                new TypeIncome { Id = 3, Name = "Rent" },
                new TypeIncome { Id = 4, Name = "Government payments" },
                new TypeIncome { Id = 5, Name = "Freelance" },
                new TypeIncome { Id = 6, Name = "Donat" },
                new TypeIncome { Id = 7, Name = "YouTube" },
                new TypeIncome { Id = 8, Name = "Percentage of the bank deposit" },
                new TypeIncome { Id = 9, Name = "Sale of personal property" },
                new TypeIncome { Id = 10, Name = "Wage" },
                new TypeIncome { Id = 11, Name = "Other" });

            modelBuilder.Entity<TypeExpense>().HasData(
                new TypeExpense { Id = 1, Name = "Products" },
                new TypeExpense { Id = 2, Name = "Apartment" },
                new TypeExpense { Id = 3, Name = "Credit" },
                new TypeExpense { Id = 4, Name = "Transport" },
                new TypeExpense { Id = 5, Name = "Clothes" },
                new TypeExpense { Id = 6, Name = "Entertainment" },
                new TypeExpense { Id = 7, Name = "Other" }
                );

            Random random = new Random();
            for (int i = 0; i < 453; i++)
            {
                modelBuilder.Entity<Income>().HasData(
                            new
                            {
                                Id = i + 1,
                                Date = DateTime.Now.AddDays(random.Next(180) * -1), //last 180 days
                                TypeIncomeId = random.Next(1, 11),
                                Amount = Convert.ToDecimal(random.Next(1, 100))
                            }
               );
            }

            for (int i = 0; i < 453; i++)
            {
                modelBuilder.Entity<Expense>().HasData(
                            new
                            {
                                Id = i + 1,
                                Date = DateTime.Now.AddDays(random.Next(180) * -1), //last 180 days
                                TypeExpenseId = random.Next(1, 7),
                                Amount = Convert.ToDecimal(random.Next(1, 100))
                            }
               );
            }
        }
    }
}
