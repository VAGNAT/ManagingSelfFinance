using System.ComponentModel.DataAnnotations;

namespace Model
{
    public class TypeIncome
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is TypeIncome income &&
                   Name == income.Name;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name);
        }
    }
}