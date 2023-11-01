using MyShop.Common.Repositories;
using MyShop.Domain.Models;

namespace MyShop.Databases.Postgres.Repositories
{
    public interface IPriceRepository : IWriteRepository<Price>
    {
        public Task<IEnumerable<Price>> GetPricesByProductIdsAsync(List<int> productIds);
    }
}
