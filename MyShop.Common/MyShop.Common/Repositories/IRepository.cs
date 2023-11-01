namespace MyShop.Common.Repositories
{
    public interface IWriteRepository<T>
    {
        Task<T> AddAsync(T item);
        Task<T> UpdateAsync(T item);
    }
}
