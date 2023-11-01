using Dapper;
using MyShop.Domain.Models;
using Npgsql;

namespace MyShop.Databases.Postgres.Repositories.Implementations
{
    public class PriceRepository : IPriceRepository
    {
        private readonly NpgsqlConnection _dbConnection;

        public PriceRepository(NpgsqlConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public Task<Price> AddAsync(Price item)
        {
            var query = $"INSERT INTO dbo.price (ProductId, Amount) VALUES ({item.ProductId}, {item.Amount}) RETURNING ProductId, Amount";
            var result = _dbConnection.QuerySingleOrDefaultAsync<Price>(query, item);
            return result;
        }

        public Task<IEnumerable<Price>> GetPricesByProductIdsAsync(List<int> productIds)
        {
            var productIdsString = string.Join(',', productIds);
            var query = $"SELECT productid as {nameof(Stock.ProductId)}, Amount as {nameof(Stock.Amount)} FROM dbo.price WHERE ProductId in ({productIdsString})";
            return _dbConnection.QueryAsync<Price>(query);
        }

        public Task<Price> UpdateAsync(Price item)
        {
            var query = $"UPDATE dbo.price SET ProductId = {item.ProductId}, Amount = {item.Amount} WHERE ProductId = {item.ProductId} RETURNING ProductId, Amount";
            return _dbConnection.QuerySingleOrDefaultAsync<Price>(query, item);
        }
    }
}
