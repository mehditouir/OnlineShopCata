using MyShop.Common.Repositories;
using MyShop.Domain.Models;

namespace MyShop.Databases.Postgres.Repositories
{
    public interface IProductRepository : IWriteRepository<Product>
    {
        public Task<IEnumerable<Product>> GetProductsAsync();
    }
}
