namespace Infrastructure.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<T> ReadAsync(int id);
        T Read(int id);
        IEnumerable<T> ReadAll();
        Task CreateAsync(T item);
        void Update(T item);
        void Delete(T item);
        bool Empty(int id);
    }
}
