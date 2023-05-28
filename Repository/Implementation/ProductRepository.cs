using DatabaseModel.DatabaseEntity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implementation
{
    public class ProductRepository : IProductRepository
    {

        private readonly AssignmentContext _dbContext;
        public ProductRepository(AssignmentContext dbContext)

        {
            _dbContext = dbContext;
        }
        public async Task<bool> CreateProduct(Product product)
        {
            try
            {
                string json = JsonConvert.SerializeObject(product);
                await _dbContext.Database.ExecuteSqlRawAsync("EXEC AddProduct @JSON", new SqlParameter("@JSON", json));
                return true;
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteProduct(int id)
        {
            try
            {
                var result = await _dbContext.Database.ExecuteSqlRawAsync("EXEC DeleteProduct @ProductID", new SqlParameter("@ProductID", id));
                return true;

            }
            catch (SqlException ex) when (ex.Number == 547)
            {
                throw new Exception("Cannot delete product due to existing related records.");
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            try
            {
                string sql = "EXEC GetAllProducts";
                return await _dbContext.Products.FromSqlRaw(sql).ToListAsync();
            }
            catch (SqlException e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<bool> UpdateProduct(Product product)
        {
            try
            {
                string json = JsonConvert.SerializeObject(product);
                await _dbContext.Database.ExecuteSqlRawAsync("EXEC UpdateProduct @JSON", new SqlParameter("@JSON", json));
                return true;
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
