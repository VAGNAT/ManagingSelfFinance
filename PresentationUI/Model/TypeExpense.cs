using System.ComponentModel.DataAnnotations;

namespace PresentationUI.Model
{
    public class TypeExpense
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is TypeExpense expense &&
                   Name == expense.Name;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name);
        }
    }
}
