using Dapper;
using MyShop.Domain.Models;
using Npgsql;

namespace MyShop.Databases.Postgres.Repositories.Implementations
{
    public class ProductRepository : IProductRepository
    {
        private readonly NpgsqlConnection _dbConnection;

        public ProductRepository(NpgsqlConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public Task<Product> AddAsync(Product item)
        {
            var nextId = GetNextId();
            var query = $"INSERT INTO dbo.product (Id, Name, Brand, Size) VALUES ({nextId}, '{item.Name}', '{item.Brand}', '{item.Size}') RETURNING Id, Name, Brand, Size";
            return _dbConnection.QuerySingleOrDefaultAsync<Product>(query, item);
        }

        public Task<IEnumerable<Product>> GetProductsAsync()
        {
            var query = $"SELECT Id as {nameof(Product.Id)}, Name as {nameof(Product.Name)}, Brand as {nameof(Product.Brand)}, Size as {nameof(Product.Size)} FROM dbo.product";
            return _dbConnection.QueryAsync<Product>(query);
        }

        public Task<Product> UpdateAsync(Product item)
        {
            var query = $"UPDATE dbo.product SET Name = '{item.Name}', Brand = '{item.Brand}', Size = '{item.Size}' WHERE Id = {item.Id} RETURNING Id, Name, Brand, Size";
            return _dbConnection.QuerySingleOrDefaultAsync<Product>(query, item);
        }

        private int GetNextId()
        {
            var query = "select max(id) from dbo.product";
            return _dbConnection.ExecuteScalar<int>(query) + 1;
        }
    }
}
