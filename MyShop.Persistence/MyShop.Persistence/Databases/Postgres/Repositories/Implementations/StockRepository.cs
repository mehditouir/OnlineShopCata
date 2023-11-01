using Dapper;
using MyShop.Domain.Models;
using Npgsql;

namespace MyShop.Databases.Postgres.Repositories.Implementations
{
    public class StockRepository : IStockRepository
    {
        private readonly NpgsqlConnection _dbConnection;

        public StockRepository(NpgsqlConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public Task<Stock> AddAsync(Stock item)
        {
            var query = $"INSERT INTO dbo.stock (ProductId, Amount) VALUES ({item.ProductId}, {item.Amount}) RETURNING ProductId, Amount";
            return _dbConnection.QuerySingleOrDefaultAsync<Stock>(query, item);
        }

        public Task<IEnumerable<Stock>> GetStocksByProductIdsAsync(List<int> productIds)
        {
            var productIdsString = string.Join(',', productIds);
            var query = $"SELECT productid as {nameof(Stock.ProductId)}, Amount as {nameof(Stock.Amount)} FROM dbo.stock WHERE ProductId in ({productIdsString})";
            return _dbConnection.QueryAsync<Stock>(query);
        }

        public Task<Stock> UpdateAsync(Stock item)
        {
            var query = $"UPDATE dbo.stock SET ProductId = {item.ProductId}, Amount = {item.Amount} WHERE ProductId = {item.ProductId} RETURNING ProductId, Amount";
            return _dbConnection.QuerySingleOrDefaultAsync<Stock>(query, item);
        }
    }
}
