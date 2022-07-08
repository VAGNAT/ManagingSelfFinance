using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    public class Income
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        public int TypeIncomeId { get; set; }

        [Required(ErrorMessage = "The type field is requered")]
        public TypeIncome TypeIncome { get; set; }

        [Required]
        [Column(TypeName = "decimal(15,2)")]
        public decimal Amount { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is Income income &&
                   Date == income.Date &&
                   EqualityComparer<TypeIncome>.Default.Equals(TypeIncome, income.TypeIncome) &&
                   Amount == income.Amount;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Date, TypeIncome, Amount);
        }
    }
}
