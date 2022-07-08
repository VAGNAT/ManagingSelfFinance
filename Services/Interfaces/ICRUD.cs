namespace Services.Interfaces
{
    public interface ICRUD<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        T GetById(int id);
        IEnumerable<T> GetAll();
        Task<T> AddAsync(T item);
        T Update(T item, T value);
        Task<bool> Delete(int id);
    }
}
