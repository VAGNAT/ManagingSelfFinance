using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PresentationUI.Model
{
    public class Expense
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        public int TypeExpenseId { get; set; }

        [Required(ErrorMessage = "The type field is requered")]
        public TypeExpense TypeExpense { get; set; }

        [Required]
        [Column(TypeName = "decimal(15,2)")]
        public decimal Amount { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is Expense expense &&
                   Date == expense.Date &&
                   EqualityComparer<TypeExpense>.Default.Equals(TypeExpense, expense.TypeExpense) &&
                   Amount == expense.Amount;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Date, TypeExpense, Amount);
        }
    }
}
