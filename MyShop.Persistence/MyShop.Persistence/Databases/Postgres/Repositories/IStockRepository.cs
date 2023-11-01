using MyShop.Common.Repositories;
using MyShop.Domain.Models;

namespace MyShop.Databases.Postgres.Repositories
{
    public interface IStockRepository : IWriteRepository<Stock>
    {
        public Task<IEnumerable<Stock>> GetStocksByProductIdsAsync(List<int> productIds);
    }
}
