namespace PresentationUI.Services.Interfaces
{
    public interface ICRUD<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task UpdateAsync(T item);
        Task CreateAsync(T item);     
        Task DeleteByIdAsync(int id);
    }
}
