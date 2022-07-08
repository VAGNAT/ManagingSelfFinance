using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationUI.Helpers
{
    public class ItemReport
    {
        public TypeForReport Type { get; set; }
        public DateOnly Date { get; set; }
        public string Name { get; set; }
        public decimal Amount { get; set; }
    }

    public enum TypeForReport
    {
        Income,
        Expense
    }
}
