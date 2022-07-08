namespace PresentationUI.Services.Interfaces
{
    public interface ICheck
    {
        Task<bool> PossibleToRemoveTypeExpenseAsync(int id);
        Task<bool> PossibleToRemoveTypeIncomeAsync(int id);
    }
}
